using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {
    public Text diamText;

    public void Apply(float stage)
    {
        GameData.Instance.diamonds = PlayerPrefs.GetInt("Diamonds");
        this.gameObject.SetActive(true);
        if (GameData.Instance.curStageState == StageState.OrderState)
        {
            int diam = (GameData.Instance.passedStageNum - 1) / 2;
            if (diam >= 1)
            {
                GameData.Instance.diamonds += diam * 2;
                diamText.text = " + " + diam * 2;
            }
            else
            {
                GameData.Instance.diamonds -= 1;
                diamText.text = " - " + 1;
            }

        }
        else if (GameData.Instance.curStageState == StageState.RandomState)
        {
            int diam = (GameData.Instance.passedStageNum-1) / 1;
            if(diam >= 1)
            {
                GameData.Instance.diamonds += diam * 5;
                diamText.text = "+ " + diam * 5;
            }else{
                GameData.Instance.diamonds -= 3;
                diamText.text = "- " + 3;
            }
        }
        else if (GameData.Instance.curStageState == StageState.GhostState)
        {
            int diam = (GameData.Instance.passedStageNum - 1) / 1;
            if (diam >= 1)
            {
                GameData.Instance.diamonds += diam * 10;
                diamText.text = "+ " + diam * 10;
            }
            else
            {
                GameData.Instance.diamonds -= 5;
                diamText.text = "- " + 5;
            }
        }
        else if (GameData.Instance.curStageState == StageState.SkeletonState)
        {
            int diam = (GameData.Instance.passedStageNum - 1) / 1;
            if (diam >= 1)
            {
                GameData.Instance.diamonds += diam * 20;
                diamText.text = "+ " + diam * 20;
            }
            else
            {
                GameData.Instance.diamonds -= 10;
                diamText.text = "- " + 10;
            }
        }

        PlayerPrefs.SetInt("Diamonds", GameData.Instance.diamonds);
        StopAllCoroutines();
    }


    public void OkBtnClick()
    {
        GameData.Instance.passedStageNum = 0;
        this.gameObject.SetActive(false);
        StopAllCoroutines();
        if (GameData.Instance.curStageState == StageState.OrderState)
        {
            if(GameData.Instance.diamonds >= 1)
                GamePanel.Instance.Init();
            else
                HomeBtnClick();       //钻石不够直接返回主界面
        }
        else if (GameData.Instance.curStageState == StageState.RandomState)
        {
            if (GameData.Instance.diamonds >= 3)
                GamePanel.Instance.Init();
            else
                HomeBtnClick();
        }
        else if (GameData.Instance.curStageState == StageState.GhostState)
        {
            if (GameData.Instance.diamonds >= 5)
                GamePanel.Instance.Init();
            else
                HomeBtnClick();
        }
        else if (GameData.Instance.curStageState == StageState.SkeletonState)
        {
            if (GameData.Instance.diamonds >= 10)
                GamePanel.Instance.Init();
            else
                HomeBtnClick();
        }

    }

    public void HomeBtnClick()
    {
        GameData.Instance.passedStageNum = 0;
        this.gameObject.SetActive(false);
        StopAllCoroutines();
        SceneManager.LoadScene("StartScene");
    }
}
