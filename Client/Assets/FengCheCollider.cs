using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FengCheCollider : MonoBehaviour {

    Vector3 collidePos;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            Vector3 borderPos = transform.localPosition;
            Vector3 collPos = collision.gameObject.GetComponent<RectTransform>().localPosition;

            Vector3 direct = collPos - borderPos;
            //transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(direct.x*10,direct.y*10));
            collision.gameObject.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(direct.x * GameData.Instance.protectPower / 5, direct.y * GameData.Instance.protectPower / 5));

        }

    }
}
