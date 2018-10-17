using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour {

    public void Apply()
    {
        this.gameObject.SetActive(true);
        StopAllCoroutines();
    }


    public void OkBtnClick()
    {
        this.gameObject.SetActive(false);
        GamePanel.Instance.Init();
    }
}
