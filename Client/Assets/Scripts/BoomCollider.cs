using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomCollider : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "protect")
        {
            StartCoroutine(DestorySelf());
           
        }
    }


    IEnumerator DestorySelf()
    {
        this.gameObject.transform.Find("Image").gameObject.SetActive(false);
        this.gameObject.transform.Find("light").gameObject.SetActive(false);
        this.gameObject.transform.Find("BoomImg").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
        GamePanel.Instance.BoomClear();
    }
}
