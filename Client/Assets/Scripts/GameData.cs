using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public bool isLost = false;
    public Vector3 protectPos;

    private static GameData instance = new GameData();
    public static GameData Instance {
        get {
            return instance;
        }
    }



}
