using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LYBRotate : MonoBehaviour {

    void Update()
    {
        //this.transform.Rotate(new Vector3(0, 0, -speed), 3);
        this.transform.Rotate(new Vector3(0, 0, -10), GameData.Instance.JGBRotateSpeed);
    }
}
