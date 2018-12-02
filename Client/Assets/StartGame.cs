using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    public GameObject theRechargePanel;
    public GameObject theTipsPanel;
    public Text diamondText;

    public static bool isRandMode = false;

    private static StartGame _instance;
    public static StartGame Instance{
        get
        {
            if (_instance == null)
            {
                _instance = new StartGame();
                return _instance;
            }
            return _instance;
        }

    }
    void Awake()
    {
        Debug.LogError("@@StartGame.awake");
#if !UNITY_EDITOR && UNITY_IOS
        IOSIAPMgr.Instance.Init();
#endif
        if (PlayerPrefs.GetString("NewPlayer") == null)
        {
            PlayerPrefs.SetInt("Diamonds", 20);
            PlayerPrefs.SetString("NewPlayer", "false");

            GameData.Instance.diamonds = PlayerPrefs.GetInt("Diamonds");
            diamondText.text = GameData.Instance.diamonds.ToString();
        }
        else
        {
            GameData.Instance.diamonds = PlayerPrefs.GetInt("Diamonds");
            if (GameData.Instance.diamonds >= 0)
            {
                diamondText.text = GameData.Instance.diamonds.ToString();
            }else{
                PlayerPrefs.SetInt("Diamonds",0);
                diamondText.text = "0";
            }
        }
    }

    void Update()
    {
        GameData.Instance.diamonds = PlayerPrefs.GetInt("Diamonds");
        diamondText.text = GameData.Instance.diamonds.ToString();
    }

    public void RandomModeStartClick()
    {
        GameData.Instance.curStageState = StageState.RandomState;
        int nowDiamonds = PlayerPrefs.GetInt("Diamonds");
        if (nowDiamonds >= 3)
        {
            StartLoadMainScenes();
            isRandMode = true;
        }
        else{
            theTipsPanel.SetActive(true);
            theTipsPanel.GetComponent<TipsPanel>().Apply(nowDiamonds,3);
        }
    }


    public void OrderModeStartClick()
    {
        GameData.Instance.curStageState = StageState.OrderState;
        int nowDiamonds = PlayerPrefs.GetInt("Diamonds");
        if (nowDiamonds >= 1)
        {
            StartLoadMainScenes();
            isRandMode = false;
        }
        else
        {
            theTipsPanel.SetActive(true);
            theTipsPanel.GetComponent<TipsPanel>().Apply(nowDiamonds, 1);
        }
    }


    public void StartLoadMainScenes()
    {
        SceneManager.LoadScene("MainScene");
    }


    public void RechargeBtnClick()
    {
        theRechargePanel.gameObject.SetActive(true);
    }


    public void CleanDiamonds()
    {
        PlayerPrefs.SetInt("Diamonds",0);
    }

}
