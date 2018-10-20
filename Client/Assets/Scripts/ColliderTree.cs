using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTree : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "enemy")
        {
            GameData.Instance.isLost = true;
            StartCoroutine(DestroySelf());

            //GamePanel.Instance.theGameOverPanel.Apply();
        }
    }

    IEnumerator DestroySelf()
    {
        transform.gameObject.SetActive(false);
        GameObject boom = transform.Find("BoomImage").gameObject;
        boom.SetActive(true);
        yield return new WaitForSeconds(1);
        boom.SetActive(false);
        
    }
}
