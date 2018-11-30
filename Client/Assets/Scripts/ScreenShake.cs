﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    private float shakeTime = 0.0f;
    private float fps = 20.0f;
    private float frameTime = 0.0f;
    private float shakeDelta = 0.005f;
    //public Camera cam;
    public static bool isshakeCamera = false;
    // Use this for initialization
    void Start()
    {
        shakeTime = 2.0f;
        fps = 20.0f;
        frameTime = 0.03f;
        shakeDelta = 0.005f;
        //isshakeCamera=true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
               // Debug.LogError("shakeTime:" + shakeTime);
                if (shakeTime <= 0)
                {
                    isshakeCamera = false;
                    Camera.main.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                  //  Debug.LogError("222" + shakeTime);
                  //  Debug.LogError("isshakeCamera" + isshakeCamera);
                    if(!Camera.main.orthographic)
                    {


                    }
                    shakeTime = 2.0f;
                    fps = 20.0f;
                    frameTime = 0.03f;
                    shakeDelta = 0.005f;
                }
                else
                {
                    frameTime += Time.deltaTime;
                   // Debug.LogError("@@@");
                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        Camera.main.rect = new Rect(shakeDelta * (-1.0f + 4.0f * Random.value), shakeDelta * (-1.0f + 4.0f * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }
}
