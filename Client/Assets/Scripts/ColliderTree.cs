using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTree : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "enemy")
        {
            GameData.Instance.isLost = true;
            StopAllCoroutines();
            StartCoroutine(DestroyTree());

        }
    }

    IEnumerator DestroyTree()
    {
        this.gameObject.transform.Find("Image").gameObject.SetActive(false);
        GameObject boom = transform.Find("BoomImage").gameObject;
        boom.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        boom.SetActive(false);
        Destroy(this.gameObject);
        
    }
}
