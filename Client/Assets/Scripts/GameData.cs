using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public bool isLost = false;

    public float protectPower = 0;
    public float protectRotateSpeed = 0;
    public float protectRotateInnerSpeed = 0;
    public float JGBRotateSpeed = 0;
    public float JGBPower = 0;

    public int diamonds = 0;
    //通过的关卡数
    public int passedStageNum = 0;

    public StageState curStageState = StageState.OrderState;

    public Vector3 protectPos;
    public List<GameObject> colliderList = new List<GameObject>();

    private static GameData instance = new GameData();
    public static GameData Instance {
        get {
            return instance;
        }
    }




}


public enum StageState
{
    OrderState,
    RandomState,

}