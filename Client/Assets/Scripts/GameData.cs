using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public bool isLost = false;

    private static GameData instance = new GameData();
    public static GameData Instance {
        get {
            return instance;
        }
    }



}
