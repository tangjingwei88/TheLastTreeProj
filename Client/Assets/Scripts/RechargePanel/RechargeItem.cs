﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeItem : MonoBehaviour {

    public void RechargeItem6Click()
    {
        Debug.LogError("RechargeItem6Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(59);
#endif
    }

    public void RechargeItem12Click()
    {
        Debug.LogError("RechargeItem12Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(149);
#endif
    }

    public void RechargeItem24Click()
    {
        Debug.LogError("RechargeItem24Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(399);
#endif
    }

    public void RechargeItem49Click()
    {
        Debug.LogError("RechargeItem49Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(999);
#endif
    }

    public void RechargeItem99Click()
    {
        Debug.LogError("RechargeItem99Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(1999);
#endif
    }

    public void RechargeItem168Click()
    {
        Debug.LogError("RechargeItem168Click");
        //IOSIAPMgr.BuyProductById("tsoft_y_6");
#if UNITY_EDITOR || UNITY_ANDROID
        IOSIAPMgr.Instance.AddPurchaseDiamonds(4999);
#endif
    }
}
