using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;

//[System.Serializable]


public class DataReader : MonoBehaviour
{

    public static DataReader Instance;

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


    public Transform dataParent;

    Vector3 lastPos = Vector3.zero;

    public float earthRadius = 41;

    [Header("Filters")]
    public int minYear;
    public int maxYear;

    public float minMag;
    public float maxMag;

    public enum AlertType {GREEN, YELLOW, ORANGE, RED, ALL};
    public AlertType alertType = AlertType.ALL;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;


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


            //if (i % width == 0)
            //{
            //    y -= 15;
            //    lastPos = Vector3.zero;
            //}


            //GameObject sp = Instantiate(sphere, new Vector3(lastPos.x + 32, y, 0), Quaternion.identity);
            //sp.transform.localScale = new Vector3(ConvertToFloat(data[i].magnitude), ConvertToFloat(data[i].magnitude), ConvertToFloat(data[i].magnitude));
            //lastMag = ConvertToFloat(data[i].magnitude);
            //lastPos = sp.transform.position;

            //GameObject txt = Instantiate(text, new Vector3(sp.transform.position.x, (sp.transform.position.y + ConvertToFloat(data[i].magnitude) / 2) + 3, sp.transform.position.z), Quaternion.identity);
            //TextMeshPro t = txt.GetComponent<TextMeshPro>();
            //t.text = data[i].location + "\n" + "Magnitude: " + data[i].magnitude;

            //if (data[i].alert == "green")
            //{
            //    sp.GetComponent<Renderer>().material.color = Color.green;
            //}
            //else if (data[i].alert == "yellow")
            //{
            //    sp.GetComponent<Renderer>().material.color = Color.yellow;
            //}
            //else if (data[i].alert == "orange")
            //{
            //    sp.GetComponent<Renderer>().material.color = Color.yellow + Color.red;
            //}
            //else if (data[i].alert == "red")
            //{
            //    sp.GetComponent<Renderer>().material.color = Color.red;
            //}


            PositionMarkers(i);
        }

        Debug.Log(GetYearFromDate(data[0].date));

    }

    // Update is called once per frame
    void Update()
    {
        Filters();


    }

    void Filters()
    {
        for (int i = 0; i < dataParent.childCount; i++)
        {
            dataParent.GetChild(i).GetComponent<DataHolder>().CheckFilter();
        }
    }

    void PositionMarkers(int index)
    {

        Vector3 position = CalculatePositionOnShpere(ConvertToFloat(data[index].latitude), ConvertToFloat(data[index].longitude));
        Transform m = Instantiate(marker, position, Quaternion.identity);

        m.SetParent(dataParent);

        var markerData = m.GetComponent<DataHolder>();
        markerData.title = data[index].title;
        markerData.magnitude = data[index].magnitude;
        markerData.date = data[index].date;
        markerData.cdi = data[index].cdi;
        markerData.mmi = data[index].mmi;
        markerData.alert = data[index].alert;
        markerData.tsunami = data[index].tsunami;
        markerData.sig = data[index].sig;
        markerData.net = data[index].net;
        markerData.nst = data[index].nst;
        markerData.dmin = data[index].dmin;
        markerData.gap = data[index].gap;
        markerData.magType = data[index].magType;
        markerData.depth = data[index].depth;
        markerData.latitude = data[index].latitude;
        markerData.longitude = data[index].longitude;
        markerData.location = data[index].location;
        markerData.continent = data[index].continent;
        markerData.country = data[index].country;

    }

    Vector3 CalculatePositionOnShpere(float latitude, float longitude)
    {
        double latitude_rad = latitude * Math.PI / 180;
        double longitude_rad = longitude * Math.PI / 180;

        double zPos = earthRadius * Math.Cos(latitude_rad) * Math.Cos(longitude_rad);
        double xPos = -earthRadius * Math.Cos(latitude_rad) * Math.Sin(longitude_rad);
        double yPos = earthRadius * Math.Sin(latitude_rad);


        Vector3 markerPos;

        markerPos.x = (float)xPos;
        markerPos.y = (float)yPos;
        markerPos.z = (float)zPos;



        return earth.transform.position + markerPos;
    }

    public string GetYearFromDate(string date)
    {
        var dateSplit = date.Split('-');
        var yearSplit = dateSplit[dateSplit.Length-1].Split(" ");

        return yearSplit[0];
    }

    public int ConvertToInt(string str)
    {
        return int.Parse(str);
    }

    public float ConvertToFloat(string str)
    {
        return float.Parse(str);
    }
}
