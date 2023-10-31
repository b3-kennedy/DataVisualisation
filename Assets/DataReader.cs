using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

//[System.Serializable]
public class Data
{
    public Data(string ti,string mag, string dat, string cd, string mm, string al, string ts, string si, string ne, string ns,
        string dm, string ga, string ma, string de, string la, string lo, string loc, string con, string cou)
    {
        title = ti;
        magnitude = mag;
        date = dat;
        cdi = cd;
        mmi = mm;
        alert = al;
        tsunami = ts;
        sig = si;
        net = ne;
        nst = ns;
        dmin = dm;
        gap = ga;
        magType = ma;
        depth = de;
        latitude = la;
        longitude = lo;
        location = loc;
        continent = con;
        country = cou;

    }

    //public Data(string ti, string mag, string dat, string cd, string mm, string al, string ts, string si, string ne, string ns,
    //string dm, string ga, string ma, string de, string la, string lo, string loc, string con, string cou)
    //{
    //    title = ti;
    //    magnitude = ConvertToFloat(mag);
    //    date = dat;
    //    cdi = ConvertToInt(cd);
    //    mmi = ConvertToInt(mm);
    //    alert = al;
    //    tsunami = ts;
    //    sig = ConvertToInt(si);
    //    net = ne;
    //    nst = ConvertToInt(ns);
    //    dmin = ConvertToFloat(dm);
    //    gap = ConvertToInt(ga);
    //    magType = ma;
    //    depth = ConvertToFloat(de);
    //    latitude = ConvertToFloat(la);
    //    longitude = ConvertToFloat(lo);
    //    location = loc;
    //    continent = con;
    //    country = cou;

    //}


    public string title;
    public string magnitude;
    public string date;
    public string cdi;
    public string mmi;
    public string alert;
    public string tsunami;
    public string sig;
    public string net;
    public string nst;
    public string dmin;
    public string gap;
    public string magType;
    public string depth;
    public string latitude;
    public string longitude;
    public string location;
    public string continent;
    public string country;
}



public class DataReader : MonoBehaviour
{
    string path = "Assets/Resources/Earthquake_data.csv";
    string[] lines;

    [Range(1, 782)]
    public int maxData;

    public Data[] data;

    public GameObject sphere;
    public GameObject text;

    float lastMag = 0;

    public int width;

    float y;
    float x;

    public Transform earth;
    public Transform marker;

    Vector3 lastPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        

        if (File.Exists(path))
        {
            lines = System.IO.File.ReadAllLines(path);
        }

        data = new Data[maxData];

        


        for (int i = 0; i < maxData; i++)
        {
            if(i > 0)
            {
                var splitLines = lines[i].Split(',');


                data[i - 1] = new Data(splitLines[0], splitLines[1], splitLines[2], splitLines[3], splitLines[4], splitLines[5],
                    splitLines[6], splitLines[7], splitLines[8], splitLines[9], splitLines[10], splitLines[11],
                    splitLines[12], splitLines[13], splitLines[14], splitLines[15], splitLines[16], splitLines[17], splitLines[18]);
            }

        }


        for (int i = 0;i < maxData-1;i++) 
        {


            if (i % width == 0)
            {
                y -= 15;
                lastPos = Vector3.zero;
            }


            GameObject sp = Instantiate(sphere, new Vector3(lastPos.x + 32, y, 0), Quaternion.identity);
            sp.transform.localScale = new Vector3(ConvertToFloat(data[i].magnitude), ConvertToFloat(data[i].magnitude), ConvertToFloat(data[i].magnitude));
            lastMag = ConvertToFloat(data[i].magnitude);
            lastPos = sp.transform.position;

            GameObject txt = Instantiate(text, new Vector3(sp.transform.position.x, (sp.transform.position.y + ConvertToFloat(data[i].magnitude) / 2) + 3, sp.transform.position.z), Quaternion.identity);
            TextMeshPro t = txt.GetComponent<TextMeshPro>();
            t.text = data[i].location + "\n" + "Magnitude: " + data[i].magnitude;

            if (data[i].alert == "green")
            {
                sp.GetComponent<Renderer>().material.color = Color.green;
            }
            else if (data[i].alert == "yellow")
            {
                sp.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (data[i].alert == "orange")
            {
                sp.GetComponent<Renderer>().material.color = Color.yellow + Color.red;
            }
            else if (data[i].alert == "red")
            {
                sp.GetComponent<Renderer>().material.color = Color.red;
            }

            Vector3 position = CalculatePositionOnShpere(ConvertToFloat(data[i].latitude), ConvertToFloat(data[i].longitude), earth.localScale.x / 2);
            Instantiate(marker, position, Quaternion.identity);

        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 CalculatePositionOnShpere(float lat, float lon, double radius)
    {
        float rad = (float)radius;
        float phi = (float)((90 - lat) * (Math.PI / 180));
        float theta = (float)((lon + 180) * (Math.PI / 180));
        float x = -((rad) * Mathf.Sin(phi) * Mathf.Cos(theta));
        float z = ((rad) * Mathf.Sin(phi) * Mathf.Sin(theta));
        float y = ((rad) * Mathf.Cos(phi));

        return earth.transform.position + new Vector3(x,y,z);
    }

    int ConvertToInt(string str)
    {
        return int.Parse(str);
    }

    float ConvertToFloat(string str)
    {
        return float.Parse(str);
    }
}
