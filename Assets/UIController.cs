using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Slider minYearSlider;
    public Slider maxYearSlider;
    public Slider minMagnitudeSlider;
    public Slider maxMagnitudeSlider;
    public Dropdown alertTypeDropdown;
    public DataReader dataReader;
    public Button tsunami;
    public TextMeshProUGUI minMag;
    public TextMeshProUGUI maxMag;
    public TextMeshProUGUI minYear;
    public TextMeshProUGUI maxYear;




    private void Start()
    {
        Debug.Log(dataReader);

        Debug.Log(minYearSlider);

        dataReader.minYear = (int)minYearSlider.value;
        dataReader.maxYear = (int)maxYearSlider.value;
        dataReader.minMagnitude = minMagnitudeSlider.value;
        dataReader.maxMagnitude = maxMagnitudeSlider.value;
        SetAlertType();
    }

    public void OnMinYearValueChanged(float value)
    {
        dataReader.minYear = (int)value;
        if (dataReader.minYear > dataReader.maxYear)
        {
            dataReader.maxYear = (int)value;
            maxYearSlider.value = (int)value;

        }
    }

    public void OnMaxYearValueChanged(float value)
    {
        dataReader.maxYear = (int)value;
        if (dataReader.maxYear < dataReader.minYear)
        {
            dataReader.minYear = (int)value;
            minYearSlider.value = (int)value;

        }
    }

    public void OnMinMagnitudeValueChanged(float value)
    {
        dataReader.minMagnitude = value;
        if (dataReader.minMagnitude > dataReader.maxMagnitude)
        {
            dataReader.maxMagnitude = value;
            maxMagnitudeSlider.value = value;
        }
    }

    public void OnMaxMagnitudeValueChanged(float value)
    {
        dataReader.maxMagnitude = value;
        if (dataReader.maxMagnitude < dataReader.minMagnitude)
        {
            dataReader.minMagnitude = value;
            minMagnitudeSlider.value = value;
            
        }
    }

    private void Update()
    {
        maxMag.text = "Max Magnitude: " + Mathf.Round(dataReader.maxMagnitude * 10)/10;
        minMag.text = "Min Magnitude: " + Mathf.Round(dataReader.minMagnitude * 10)/10;

        minYear.text = "Min Year: " + dataReader.minYear;
        maxYear.text = "Max Year: " + dataReader.maxYear;
    }

    public void ShowOnlyTsunamiEarthquakes()
    {
        //dataReader.alertType = DataReader.AlertType.;
        dataReader.Filters();
    }

    public void OnAlertTypeDropdownValueChanged()
    {
        SetAlertType();
    }

    private void SetAlertType()
    {
        if(alertTypeDropdown != null)
        {
            switch (alertTypeDropdown.value)
            {
                case 0:
                    dataReader.alertType = DataReader.AlertType.GREEN;
                    break;
                case 1:
                    dataReader.alertType = DataReader.AlertType.YELLOW;
                    break;
                case 2:
                    dataReader.alertType = DataReader.AlertType.ORANGE;
                    break;
                case 3:
                    dataReader.alertType = DataReader.AlertType.RED;
                    break;
                default:
                    dataReader.alertType = DataReader.AlertType.ALL;
                    break;
            }
        }

    }

    public void ApplyFilters()
    {
        dataReader.Filters();
    }
}
