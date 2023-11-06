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

    public int width;

    public Transform earth;
    public Transform marker;


    public Transform dataParent;

    Vector3 lastPos = Vector3.zero;

    public float earthRadius = 41;

    [Header("Filters")]
    public int minYear;
    public int maxYear;

    public float minMagnitude;
    public float maxMagnitude;

    public enum Tsunami {YES, NO, ALL};
    public Tsunami tsunami = Tsunami.ALL;

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
            PositionMarkers(i);
        }

        Debug.Log(GetYearFromDate(data[0].date));

    }

    // Update is called once per frame
    void Update()
    {
        Filters();


    }

    public void Filters()
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
