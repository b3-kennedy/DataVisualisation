using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        reader = DataReader.Instance;
        year = reader.ConvertToInt(reader.GetYearFromDate(date));
        mag = reader.ConvertToFloat(magnitude);
    }

    public void CheckFilter()
    {
        gameObject.SetActive(CheckYearFilter() && CheckMagnitudeFilter() && CheckAlertFilter());
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
}
