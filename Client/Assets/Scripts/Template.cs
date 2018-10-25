using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour {

    void Update()
    {
        if (this.gameObject.transform.GetChildCount() == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
