using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRightForce : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(1,5),ForceMode2D.Force);
	}
	
	
}
