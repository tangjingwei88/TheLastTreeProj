using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public static bool isRandMode = false;

    public void RandomModeStartClick()
    {
        StartLoadMainScenes();
        isRandMode = true;
    }


    public void OrderModeStartClick()
    {
        StartLoadMainScenes();
        isRandMode = false;
    }


    public void StartLoadMainScenes()
    {
        SceneManager.LoadScene("MainScene");
    }
}
