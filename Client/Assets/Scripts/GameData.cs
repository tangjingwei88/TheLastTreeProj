using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public bool isLost = false;
    public Vector3 protectPos;
    public List<GameObject> colliderList = new List<GameObject>();

    private static GameData instance = new GameData();
    public static GameData Instance {
        get {
            return instance;
        }
    }



}
