using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protectInnerRotate : MonoBehaviour {

    public int speed = 3;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, -speed), 3);
    }
}
