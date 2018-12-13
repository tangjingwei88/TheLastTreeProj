using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RechargePanel : MonoBehaviour
{

    public GameObject CloseBtn;
    public Text price01Text;
    public Text price02Text;
    public Text price03Text;
    public Text price04Text;
    public Text price05Text;
    public Text price06Text;

    void Awake()
    {
        Debug.LogError("@@RechargePanel.Awake!!");
        ApplyInfo();
    }

    public void ApplyInfo()
    {
        string[] priceList = GameData.Instance.allPirceStr.Split(';');
        for (int i = 0; i < priceList.Length; i++)
        {
            if (priceList[i] != null)
            {
                Debug.LogError("@@pirceList[i]: " + priceList[i]);
                ApplyPriceInfo(priceList[i]);
            }
        }
    }

    public void ApplyPriceInfo(string info)
    {
#if UNITY_IOS
        string[] strList = info.Split('|');
        string mProductId = strList[0];
        string mPrice = strList[1];

        Debug.LogError("@@mProductId: " + mProductId);
        Debug.LogError("@@mPrice: " + mProductId);

        if (mProductId == "tsoft_protecter_1001")
        {
            price01Text.text = mPrice.ToString();
        }
        else if (mProductId == "tsoft_protecter_1002")
        {
            price02Text.text = mPrice.ToString();
        }
        else if (mProductId == "tsoft_protecter_1003")
        {
            price03Text.text = mPrice.ToString();
        }
        else if (mProductId == "tsoft_protecter_1004")
        {
            price04Text.text = mPrice.ToString();
        }
        else if (mProductId == "tsoft_protecter_1005")
        {
            price05Text.text = mPrice.ToString();
        }
        else if (mProductId == "tsoft_protecter_1006")
        {
            price06Text.text = mPrice.ToString();
        }
#endif
    }


    public void CloseBtnClick()
    {
        this.gameObject.SetActive(false);
    }
}
