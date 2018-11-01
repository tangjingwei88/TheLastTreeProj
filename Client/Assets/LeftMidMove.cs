using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMidMove : MonoBehaviour {

    public float dis = 0;

    public void Apply(float dis)
    {
        this.dis = dis;
    }
	// Update is called once per frame
	void Update () {
        if (this.dis > 0)
        {
            Vector3 curPos = transform.localPosition;
            Vector3 endPos = new Vector3(transform.localPosition.x + dis, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(curPos, endPos, 0.05f);
        }
        else if (this.dis < 0)
        {
            Vector3 curPos = transform.localPosition;
            Vector3 endPos = new Vector3(transform.localPosition.x - dis, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.Lerp(curPos, endPos, 5);
        }
	}
}
