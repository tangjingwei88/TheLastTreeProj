using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanel : MonoBehaviour {

    public GameObject theRechargePanel;
    public Text nowDiamondText;
    public Text needDiamondText;

    public void Apply(int nowDiamond,int needDiamond)
    {
        nowDiamondText.text = nowDiamond.ToString();
        needDiamondText.text = needDiamond.ToString();
    }
	
    public void CloseBtnClick()
    {
        this.gameObject.SetActive(false);
    }

    public void RechargeBtn()
    {
        this.gameObject.SetActive(false);
        theRechargePanel.gameObject.SetActive(true);
    }
}
