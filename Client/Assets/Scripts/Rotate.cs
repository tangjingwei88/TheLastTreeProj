using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public int speed = 100;
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0,Time.deltaTime * speed,0),0.5f);
	}
}
