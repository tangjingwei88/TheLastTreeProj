using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour {

    private static LoadingManager instance;
    public static LoadingManager Instance {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        InitConfig();
        IOSIAPMgr.Instance.Init();
    }


    private void InitConfig()
    {
        StageConfigManager.Init();
    }


}
