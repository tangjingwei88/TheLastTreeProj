﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefine : MonoBehaviour {

    public const string UIPrefabPath = "UIPrefab/";


    private static GameDefine instance;
    public static GameDefine Instance {
        get {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }
}
