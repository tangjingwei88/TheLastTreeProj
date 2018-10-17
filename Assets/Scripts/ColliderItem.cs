using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderItem : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "bottom")
        {
            Destroy(this.gameObject);
        }
    }
}
