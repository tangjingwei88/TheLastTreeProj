using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeItem : MonoBehaviour {

    public void RechargeItem6Click()
    {
        Debug.LogError("RechargeItem6Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
      IOSIAPMgr.Instance.AddPurchaseDiamonds(59);
#else
        ChannelIOSAPI.BuyProductById(1001);
#endif
    }

    public void RechargeItem12Click()
    {
        Debug.LogError("RechargeItem12Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(149);
#else
        ChannelIOSAPI.BuyProductById(1002);
#endif
    }

    public void RechargeItem24Click()
    {
        Debug.LogError("RechargeItem24Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(399);
#else
        ChannelIOSAPI.BuyProductById(1003);
#endif
    }

    public void RechargeItem49Click()
    {
        Debug.LogError("RechargeItem49Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(999);
#else
        ChannelIOSAPI.BuyProductById(1004);
#endif
    }

    public void RechargeItem99Click()
    {
        Debug.LogError("RechargeItem99Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(1999);
#else
        ChannelIOSAPI.BuyProductById(1005);
#endif
    }

    public void RechargeItem168Click()
    {
        Debug.LogError("RechargeItem168Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(4999);
#else
        ChannelIOSAPI.BuyProductById(1006);
#endif
    }
}
