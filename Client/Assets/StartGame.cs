using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public RechargePanel theRechargePanel;
    public GameObject theTipsPanel;

    public Image ZDCloseSprite;
    public Image MusicCloseSprite;

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
#if !UNITY_EDITOR && UNITY_IOS
        IOSIAPMgr.Instance.Init();
#endif
        if (PlayerPrefs.GetString("NewPlayer") != "false")
        {
            PlayerPrefs.SetInt("Diamonds", 29);
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
        ChannelIOSAPI.RequstALLProductInfo();
    }

    public void Init()
    {
        //ReadText();
        RefreshModeShow();
        if (PlayerPrefs.GetString("Shake") == "true")
        {
            ZDCloseSprite.gameObject.SetActive(false);
            GameData.Instance.isShake = true;
        }
        else 
        {
            ZDCloseSprite.gameObject.SetActive(true);
            GameData.Instance.isShake = false;
        }

        if (PlayerPrefs.GetString("Music") == "true")
        {
            MusicCloseSprite.gameObject.SetActive(false);
            GameData.Instance.isMusic = true;
        }
        else
        {
            MusicCloseSprite.gameObject.SetActive(true);
            GameData.Instance.isMusic = false;
        }
    }

    void Update()
    {
        GameData.Instance.diamonds = PlayerPrefs.GetInt("Diamonds");
       // WriteText(GameData.Instance.diamonds.ToString());
        diamondText.text = GameData.Instance.diamonds.ToString();
        RefreshModeShow();
    }

    public void ReadText()
    {
        string path = Application.dataPath + "/portecter/pro.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        else
        {
            //文件读写流
            StreamReader sr = new StreamReader(path);
            //读取内容
            string result = sr.ReadToEnd();

            PlayerPrefs.SetInt("Diamonds", int.Parse(result));
        }
    }

    public void WriteText(string str)
    {
        string path = Application.dataPath + "/portecter/pro.txt";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        //文件流
        FileStream fs = File.OpenWrite(path);

        byte[] map = Encoding.UTF8.GetBytes(str);
        fs.Write(map, 0, map.Length);

        fs.Close();
        fs.Dispose();
    }


    public string CreateFolder(string path, string FolderName)
    {
        string FolderPath = path + FolderName;
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }
        return FolderPath;
    }


    public void OrderModeStartClick()
    {
        PlayMusic("click");
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
        PlayMusic("click");
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
        PlayMusic("click");
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
        PlayMusic("click");
    
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
        PlayMusic("click");
        if (GameData.Instance.isShake)
        {//设为不震动
            GameData.Instance.isShake = false;
            ZDCloseSprite.gameObject.SetActive(true);
            PlayerPrefs.SetString("Shake", "false");
        }
        else {//设为震动
            GameData.Instance.isShake = true;
            ZDCloseSprite.gameObject.SetActive(false);
            PlayerPrefs.SetString("Shake", "true");
        }
    }


    public void OpenCloseMusic()
    {
        PlayMusic("click");
        if (GameData.Instance.isMusic)
        {//关闭音乐
            GameData.Instance.isMusic = false;
            MusicCloseSprite.gameObject.SetActive(true);
            PlayerPrefs.SetString("Music", "false");
        }
        else
        {//开启音乐
            GameData.Instance.isMusic = true;
            MusicCloseSprite.gameObject.SetActive(false);
            PlayerPrefs.SetString("Music", "true");
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
        PlayMusic("click");
        ChannelIOSAPI.RequstALLProductInfo();
        theRechargePanel.gameObject.SetActive(true);
        theRechargePanel.gameObject.GetComponent<RechargePanel>().ApplyInfo();
    }

    public void PlayMusic(string name)
    {
        if (GameData.Instance.isMusic)
        {
            string audioStr = GameDefine.AudioPath + name;
           // Debug.LogError("audioStr: " + audioStr);
            AudioClip collideClip = Resources.Load(audioStr) as AudioClip;
            if (collideClip != null)
            {
              //  Debug.LogError("audio");
                AudioSource.PlayClipAtPoint(collideClip, Camera.main.transform.position);
            }
        }
    }

    public void SetRechargePrice(string info)
    {
       // Debug.LogError("@@#:" + theRechargePanel.gameObject.name);
        theRechargePanel.gameObject.GetComponent<RechargePanel>().ApplyInfo();
    }

    public void CleanDiamonds()
    {
        PlayerPrefs.SetInt("Diamonds",0);
    }

}
