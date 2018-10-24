using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDefine : MonoBehaviour {

    public const string UIPrefabPath = "UIPrefab/";
    public const string ItemPrefabPath = "UIPrefab/ItemPrefab/";
    public const string ProtoPath = "Config/ClientProto/";
    public const string AudioPath = "Audio/";

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
