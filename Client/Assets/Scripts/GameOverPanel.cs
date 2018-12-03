using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {
    public Text diamText;

    public void Apply(float stage)
    {
        this.gameObject.SetActive(true);
        if(StartGame.isRandMode)
        {
            int diam = (GameData.Instance.passedStageNum-1) / 2;
            if(diam >= 1)
            {
                GameData.Instance.diamonds += diam * 5;
                diamText.text = "+ " + diam * 5;
            }else{
                GameData.Instance.diamonds -= 3;
                diamText.text = "- " + 3;
            }
        }
        else if(!StartGame.isRandMode)
        {
            int diam = (GameData.Instance.passedStageNum -1 ) / 3;
            if (diam >= 1)
            {
                GameData.Instance.diamonds += diam * 3;
                diamText.text = " + " + diam * 3;
            }
            else
            {
                GameData.Instance.diamonds -= 1;
                diamText.text = " - " + 1;
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

    }

    public void HomeBtnClick()
    {
        GameData.Instance.passedStageNum = 0;
        this.gameObject.SetActive(false);
        StopAllCoroutines();
        SceneManager.LoadScene("StartScene");
    }
}
