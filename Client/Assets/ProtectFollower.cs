using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectFollower : MonoBehaviour {
    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
//        if (timer > 0.1f)
//        {
            GameObject protect = GameObject.FindGameObjectWithTag("protect");
            Vector3 protectPos = protect.transform.localPosition;
            transform.localPosition = new Vector3(protectPos.x, protectPos.y - 200, 0);
           
    
            //transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, protectPos.x,0.1f), Mathf.Lerp(transform.localPosition.y, protectPos.y - 100,0.1f), 0);
            timer = 0;
//        }

    }

}
