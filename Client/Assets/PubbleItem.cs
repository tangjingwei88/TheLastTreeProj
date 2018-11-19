using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubbleItem : MonoBehaviour {

    float timer = 0;
	void Update () {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            this.transform.localEulerAngles = Vector3.zero;
            timer = 0;
        }
	}
}
