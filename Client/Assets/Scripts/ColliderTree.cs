using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTree : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "enemy")
        {
            GameData.Instance.isLost = true;
            //GamePanel.Instance.theGameOverPanel.Apply();
        }
    }
}
