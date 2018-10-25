using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JGBRotate : MonoBehaviour {


    void Update()
    {
        //this.transform.Rotate(new Vector3(0, 0, -speed), 3);
        this.transform.Rotate(new Vector3(0, 0, -GameData.Instance.JGBRotateSpeed), GameData.Instance.JGBRotateSpeed);
    }
}
