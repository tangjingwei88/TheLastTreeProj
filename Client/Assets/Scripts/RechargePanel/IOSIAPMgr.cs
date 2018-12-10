﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class IOSIAPMgr : MonoBehaviour
{
	
	public List<string> productInfo = new List<string>();
	
    private static IOSIAPMgr _instance;
	public static IOSIAPMgr Instance{
		get{
            if(_instance == null)
            {
                _instance = new IOSIAPMgr();

            }
			return _instance;
		}
	}
	//private string _goodsID = "";
	void Awake()
	{
		//_instance = this;
		Init ();
		StartCoroutine (InitProductInfo ());
		DontDestroyOnLoad (this.gameObject);
		
	}
	private IEnumerator InitProductInfo()
	{
		yield return new WaitForSeconds(5f);
		IOSIAPMgr.Instance.RequstALLProductInfo();
	}
	
	
	[DllImport("__Internal")]
	private static extern void InitIAPManager();//初始化
	
	[DllImport("__Internal")]
	private static extern bool IsProductAvailable();//判断是否可以购买
	
	[DllImport("__Internal")]
	private static extern void RequstProductInfo(string s);//获取商品信息
	
	[DllImport("__Internal")]
	private static extern void BuyProduct(int id);//购买商品


	public void BuyProductById(int mOrderId)
	{
		Debug.LogError("@@mOrderId: "+mOrderId);
		BuyProduct(mOrderId);
	}
	//测试从xcode接收到的字符串
	void IOSToU(string s)
	{
		Debug.Log("[MsgFrom ios]"+s);
	}
	
	//获取product列表
	void ShowProductList(string s){
		productInfo.Add(s);
        Debug.LogError("@@ShowProductListk : " + s);
    }
	
	//获取商品回执
	void ProvideContent(string s)
	{
		Debug.Log("[MsgFrom ios]proivideContent : "+s);
	}
    public void Init()
    {
        Debug.Log("初始化InitIAPManager");
#if UNITY_IOS && !UNITY_EDITOR
        InitIAPManager();
#endif

    }
	
	public bool IsProductVailable()
	{
		return IsProductAvailable();
	}
	
	public void RequstALLProductInfo()
	{
		if(IsProductAvailable())
		{
			RequstProductInfo("tsoft_protecter_1001");
			RequstProductInfo("tsoft_protecter_1002");
			RequstProductInfo("tsoft_protecter_1003");
			
			RequstProductInfo("tsoft_protecter_1004");
			RequstProductInfo("tsoft_protecter_1005");
			RequstProductInfo("tsoft_protecter_1006");

		}
		
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