using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForce : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200)) ;
	}
	
}
