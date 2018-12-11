using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject theRechargePanel;
    public GameObject theTipsPanel;

    public Image ZDCloseSprite;

    public Image orderGraySprite;
    public Image orderShowSprite;
    public Image randomGraySprite;
    public Image randomShowSprite;
    public Image ghostGraySprite;
    public Image ghostShowSprite;
    public Image skeletonGraySprite;
    public Image skeletonShowSprite;

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
        Debug.LogError("@@@Newr:" + PlayerPrefs.GetString("Newr"));
        Debug.LogError("@@StartGame.awake");
#if !UNITY_EDITOR && UNITY_IOS
        IOSIAPMgr.Instance.Init();
#endif
        if (PlayerPrefs.GetString("NewPlayer") != "false")
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

        Init();
    }

    public void Init()
    {
        RefreshModeShow();
        if (GameData.Instance.isShake)
        {
            ZDCloseSprite.gameObject.SetActive(true);
        }
        else 
        {
            ZDCloseSprite.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        GameData.Instance.diamonds = PlayerPrefs.GetInt("Diamonds");
        diamondText.text = GameData.Instance.diamonds.ToString();
        RefreshModeShow();
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



    public void GhostModeStartClick()
    {
        GameData.Instance.curStageState = StageState.GhostState;
        int nowDiamonds = PlayerPrefs.GetInt("Diamonds");
        if (nowDiamonds >= 5)
        {
            StartLoadMainScenes();
        }
        else
        {
            theTipsPanel.SetActive(true);
            theTipsPanel.GetComponent<TipsPanel>().Apply(nowDiamonds, 5);
        }
    }


    public void SkeletonModeStartClick()
    {
        GameData.Instance.curStageState = StageState.SkeletonState;
        int nowDiamonds = PlayerPrefs.GetInt("Diamonds");
        if (nowDiamonds >= 10)
        {
            StartLoadMainScenes();
        }
        else
        {
            theTipsPanel.SetActive(true);
            theTipsPanel.GetComponent<TipsPanel>().Apply(nowDiamonds, 10);
        }
    }

    public void OpenCloseShake()
    {
        if (GameData.Instance.isShake)
        {//设为不震动
            GameData.Instance.isShake = false;
            ZDCloseSprite.gameObject.SetActive(false);
            PlayerPrefs.SetString("Shake", "false");
        }
        else {//设为震动
            GameData.Instance.isShake = true;
            ZDCloseSprite.gameObject.SetActive(true);
            PlayerPrefs.SetString("Shake", "true");
        }
    }

    //刷新模式选择显示
    public void RefreshModeShow()
    {
        int sumDiamonds = PlayerPrefs.GetInt("Diamonds");
        if (sumDiamonds >= 10)
        {
            orderGraySprite.gameObject.SetActive(false);
            randomGraySprite.gameObject.SetActive(false);
            ghostGraySprite.gameObject.SetActive(false);
            skeletonGraySprite.gameObject.SetActive(false);

            orderShowSprite.gameObject.SetActive(true);
            randomShowSprite.gameObject.SetActive(true);
            ghostShowSprite.gameObject.SetActive(true);
            skeletonShowSprite.gameObject.SetActive(true);
        }
        else if (sumDiamonds >= 5 && sumDiamonds < 10)
        {
            orderGraySprite.gameObject.SetActive(false);
            randomGraySprite.gameObject.SetActive(false);
            ghostGraySprite.gameObject.SetActive(false);
            skeletonGraySprite.gameObject.SetActive(true);

            orderShowSprite.gameObject.SetActive(true);
            randomShowSprite.gameObject.SetActive(true);
            ghostShowSprite.gameObject.SetActive(true);
            skeletonShowSprite.gameObject.SetActive(false);
        }
        else if (sumDiamonds >= 3 && sumDiamonds < 5)
        {
            orderGraySprite.gameObject.SetActive(false);
            randomGraySprite.gameObject.SetActive(false);
            ghostGraySprite.gameObject.SetActive(true);
            skeletonGraySprite.gameObject.SetActive(true);

            orderShowSprite.gameObject.SetActive(true);
            randomShowSprite.gameObject.SetActive(true);
            ghostShowSprite.gameObject.SetActive(false);
            skeletonShowSprite.gameObject.SetActive(false);
        }
        else if (sumDiamonds >= 1 && sumDiamonds < 3)
        {
            orderGraySprite.gameObject.SetActive(false);
            randomGraySprite.gameObject.SetActive(true);
            ghostGraySprite.gameObject.SetActive(true);
            skeletonGraySprite.gameObject.SetActive(true);

            orderShowSprite.gameObject.SetActive(true);
            randomShowSprite.gameObject.SetActive(false);
            ghostShowSprite.gameObject.SetActive(false);
            skeletonShowSprite.gameObject.SetActive(false);
        }
        else {
            orderGraySprite.gameObject.SetActive(true);
            randomGraySprite.gameObject.SetActive(true);
            ghostGraySprite.gameObject.SetActive(true);
            skeletonGraySprite.gameObject.SetActive(true);

            orderShowSprite.gameObject.SetActive(false);
            randomShowSprite.gameObject.SetActive(false);
            ghostShowSprite.gameObject.SetActive(false);
            skeletonShowSprite.gameObject.SetActive(false);
        }

    }


    public void StartLoadMainScenes()
    {
        SceneManager.LoadScene("MainScene");
    }


    public void RechargeBtnClick()
    {
        theRechargePanel.gameObject.SetActive(true);
        ChannelIOSAPI.RequstALLProductInfo();
    }


    public void CleanDiamonds()
    {
        PlayerPrefs.SetInt("Diamonds",0);
    }

}
