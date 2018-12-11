using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class ChannelIOSAPI{
    [DllImport("__Internal")]
    private static extern void InitIAPManager();//初始化

    [DllImport("__Internal")]
    private static extern bool IsProductAvailable();//判断是否可以购买

    [DllImport("__Internal")]
    private static extern void RequstProductInfo(string s);//获取商品信息

    [DllImport("__Internal")]
    private static extern void BuyProduct(int id);//购买商品

    void Awake()
    {
        Init();
        //StartCoroutine(InitProductInfo());
        //DontDestroyOnLoad(this.gameObject);
    }

    private IEnumerator InitProductInfo()
    {
        yield return new WaitForSeconds(5f);
        RequstALLProductInfo();
    }


    public static void Init()
    {
        Debug.Log("初始化InitIAPManager");
#if UNITY_IOS && !UNITY_EDITOR
        //InitIAPManager();
        RequstALLProductInfo();
#endif

    }

    public static  void BuyProductById(int mOrderId)
    {
        Debug.LogError("@@mOrderId: " + mOrderId);
        BuyProduct(mOrderId);
    }


    public static void RequstALLProductInfo()
    {
        if (IsProductAvailable())
        {
            RequstProductInfo("tsoft_protecter_1001");
            RequstProductInfo("tsoft_protecter_1002");
            RequstProductInfo("tsoft_protecter_1003");

            RequstProductInfo("tsoft_protecter_1004");
            RequstProductInfo("tsoft_protecter_1005");
            RequstProductInfo("tsoft_protecter_1006");

        }

    }
}
