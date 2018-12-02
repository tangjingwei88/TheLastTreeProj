using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargePanel : MonoBehaviour {

    public GameObject CloseBtn;



    public void CloseBtnClick()
    {
        this.gameObject.SetActive(false);
    }
}
