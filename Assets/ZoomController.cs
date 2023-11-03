using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomController : MonoBehaviour
{
    public Slider zoomSlider;
    public Camera mainCamera;
    public float zoomSpeed = 5f;

    void Update()
    {
        float zoomValue = 1-zoomSlider.value;

        // Kamera yakınlaştırma ve uzaklaştırma işlemleri
        mainCamera.fieldOfView = Mathf.Lerp(20, 60, zoomValue);
    }

}