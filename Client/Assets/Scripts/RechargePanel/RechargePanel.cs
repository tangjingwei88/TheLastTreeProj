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
        ApplyInfo();
    }

    public void ApplyInfo()
    {
        string[] priceList = GameData.Instance.allPirceStr.Split(';');
        Debug.LogError("@@priceList.Length: " + priceList.Length);
        //如果从appstore上没有拿到所有的商品价格信息，就直接隐藏价格显示
        if (priceList.Length-1 < 6)
        {
            //HidePriceText(true);
#if UNITY_IOS
            ChannelIOSAPI.RequstALLProductInfo();
#endif
        }
        else {
            //HidePriceText(false);
        }

        for (int i = 0; i < priceList.Length; i++)
        {
            if (priceList[i] != null)
            {
                //Debug.LogError("@@pirceList[i]: " + priceList[i]);
                ApplyPriceInfo(priceList[i]);
            }
        }
    }

    public void ApplyPriceInfo(string info)
    {
#if UNITY_IOS && !UNITY_EDITOR
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


    public void HidePriceText(bool hide)
    {
        if (hide)
        {
            price01Text.gameObject.SetActive(false);
            price02Text.gameObject.SetActive(false);
            price03Text.gameObject.SetActive(false);
            price04Text.gameObject.SetActive(false);
            price05Text.gameObject.SetActive(false);
            price06Text.gameObject.SetActive(false);
        }
        else {
            price01Text.gameObject.SetActive(true);
            price02Text.gameObject.SetActive(true);
            price03Text.gameObject.SetActive(true);
            price04Text.gameObject.SetActive(true);
            price05Text.gameObject.SetActive(true);
            price06Text.gameObject.SetActive(true);
        }
    }

    public void CloseBtnClick()
    {
        PlayMusic("click");
        this.gameObject.SetActive(false);
    }

    public void PlayMusic(string name)
    {
        if (GameData.Instance.isMusic)
        {
            string audioStr = GameDefine.AudioPath + name;
            Debug.LogError("audioStr: " + audioStr);
            AudioClip collideClip = Resources.Load(audioStr) as AudioClip;
            if (collideClip != null)
            {
                Debug.LogError("audio");
                AudioSource.PlayClipAtPoint(collideClip, Camera.main.transform.position);
            }
        }
    }
}
