using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelIOSReceiver : MonoBehaviour {

    //获取product列表
    public void ShowProductList(string s)
    {
        Debug.LogError("@@ShowProductListk : " + s);
    }


    //获取商品回执
    void ProvideContent(string s)
    {
        Debug.Log("[MsgFrom ios]proivideContent : " + s);
    }


    /// <summary>
    /// 购买商品成功的回调
    /// </summary>
    /// <param name="str">String.</param>
    public void BuyProcuctSucessCallBack(string str)
    {
        Debug.LogError("@@BuyProcuctSucessCallBack : " + str);
        if (str == "")
        {
            AddPurchaseDiamonds(59);
        }
        else if (str == "")
        {
            AddPurchaseDiamonds(149);
        }
        else if (str == "")
        {
            AddPurchaseDiamonds(399);
        }
        else if (str == "")
        {
            AddPurchaseDiamonds(149);
        }
        else if (str == "")
        {
            AddPurchaseDiamonds(149);
        }
        else if (str == "")
        {
            AddPurchaseDiamonds(149);
        }


    }
    /// <summary>
    /// 购买商品失败调回调
    /// </summary>
    /// <param name="str">String.</param>
    public void BuyProcuctFailedCallBack(string str)
    {
        Debug.LogError("@@BuyProcuctFailedCallBack : " + str);
    }


    public void AddPurchaseDiamonds(int diamondNum)
    {
        GameData.Instance.diamonds += diamondNum;
        PlayerPrefs.SetInt("Diamonds", GameData.Instance.diamonds);

    }

}
