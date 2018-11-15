using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {



    public void RandomModeStartClick()
    {
        StartLoadMainScenes();
    }


    public void OrderModeStartClick()
    {
        StartLoadMainScenes();
    }


    public void StartLoadMainScenes()
    {
        SceneManager.LoadScene("MainScene");
    }
}
