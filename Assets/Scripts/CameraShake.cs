using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    float shakeIntensity;
    public Camera cam;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        Debug.Log(cam);
    }

    public void Shake(float intensity, float length)
    {

        shakeIntensity = intensity;
        InvokeRepeating("BeginShake", 0, 0.01f);    //repeatedly invoke BeginShake() after 0 seconds at 0.01 second intervals
        Invoke("EndShake", length);         //invoke EndShake() after length seconds
    }

    void BeginShake()
    {
        if (shakeIntensity > 0)     //if there should be any shaking
        {
            //create temporary variable equal to position of the camera
            Vector3 camPos = cam.transform.position;

            //creating offset amount
            float shakeAmtx = Random.value * shakeIntensity * 2 - shakeIntensity;   //apparently this equation is optimal?
            float shakeAmty = Random.value * shakeIntensity * 2 - shakeIntensity;

            //assigning offset amounts to temporary variable
            camPos.x += shakeAmtx;
            camPos.y += shakeAmty;

            //assigning temporary variable into camera transform at the end of calculation
            cam.transform.position = camPos;
            
        }

    }

    void EndShake()
    {
        CancelInvoke("BeginShake");
        cam.transform.localPosition = Vector3.zero;
    }
}
