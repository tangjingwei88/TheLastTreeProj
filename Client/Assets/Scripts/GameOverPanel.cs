using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour {

    public void Apply(float stage)
    {
        this.gameObject.SetActive(true);
        StopAllCoroutines();
    }


    public void OkBtnClick()
    {
        this.gameObject.SetActive(false);
        StopAllCoroutines();
        GamePanel.Instance.Init();
    }
}
