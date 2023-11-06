using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderTextControl : MonoBehaviour
{
    private TextMeshProUGUI sliderText = null;
    private float sliderAmount = 100f;


    public void sliderChange(float value)
    {
        float localValue = value * sliderAmount;
        sliderText.text = sliderAmount.ToString("0");

    }
}