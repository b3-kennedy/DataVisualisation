using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EasyUI.Dialogs;
using UnityEngine.Rendering;
using reversegeocoding;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;


public class DataHolder : MonoBehaviour
{
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

    DataReader reader;
    int year;
    float mag;

    string popUpTitle;
    string popUpMessage;
    // public PopUpController popUp;

    private void Start()
    {
        reader = DataReader.Instance;
        year = reader.ConvertToInt(reader.GetYearFromDate(date));
        mag = reader.ConvertToFloat(magnitude);
        
        RootObject rootObject=getAddress(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
        string convertedLocation= rootObject.city +","+ rootObject.country;
        Debug.Log(rootObject.city);
        // popUpTitle = convertedLocation;
        popUpTitle = country +" - Earthquake in "+ year;
        popUpMessage = "⦿ Magnitude : "+mag+ System.Environment.NewLine +
         "⦿ Alert : "+ (!string.IsNullOrEmpty(alert) ? alert : "N/A") +  System.Environment.NewLine +
          "⦿ Location : "+ location +  System.Environment.NewLine +
           "⦿ Depth : "+ depth+  System.Environment.NewLine +
            "⦿ Tsunami : "+ (tsunami=="1"? "yes" : "no");
        
        
    }
    private void OnMouseDown() {
   
        PopUp.Instance.SetTitle(popUpTitle).SetMessage(popUpMessage).Show();
         
    }
    
    
    public void CheckFilter()
    {
        gameObject.SetActive(CheckYearFilter() && CheckMagnitudeFilter() && CheckAlertFilter() && CheckTsunamiFilter());
    }
    

    bool CheckYearFilter()
    {
        if (year >= reader.minYear && year <= reader.maxYear)
        {
            return true;
        }
        return false;
    }

    bool CheckMagnitudeFilter()
    {
        if(mag >= reader.minMagnitude && mag <= reader.maxMagnitude)
        {
            return true;
        }
        return false;
    }


    bool CheckTsunamiFilter()
    {
        if(reader.tsunami == DataReader.Tsunami.YES && tsunami == "1")
        {
            return true;
        }
        else if(reader.tsunami == DataReader.Tsunami.ALL)
        {
            return true;
        }
        else if(reader.tsunami == DataReader.Tsunami.NO && tsunami == "0")
        {
            return true;
        }
        return false;
    }

    bool CheckAlertFilter()
    {
        switch (reader.alertType)
        {
            case DataReader.AlertType.ALL:
                return true;
            case DataReader.AlertType.GREEN:
                if(alert == "green")
                {
                    return true;
                }
                break;
            case DataReader.AlertType.YELLOW:
                if(alert == "yellow")
                {
                    return true;
                }
                break;
            case DataReader.AlertType.ORANGE:
                if (alert == "orange")
                {
                    return true;
                }
                break;
            case DataReader.AlertType.RED:
                if (alert == "red")
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }
    RootObject getAddress(double lat,double lon)
    {
        WebClient webClient = new WebClient();
        webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        webClient.Headers.Add("Referer", "http://www.microsoft.com");
        var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat="+lat+"&lon="+lon);
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
        RootObject rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));
        return rootObject;
    }
}
