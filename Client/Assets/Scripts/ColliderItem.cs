using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderItem : MonoBehaviour {

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "bottom")
        {
            Destroy(this.gameObject);
        }
        //碰撞有穿透问题，目前引擎没有方法解决，给碰撞体添加相反的力模拟碰撞
        else if (coll.transform.tag == "protect")
        {
            Vector3 treePos = new Vector3(0,-510,0);
            Vector3 collPos = coll.gameObject.GetComponent<RectTransform>().localPosition;
            //Debug.LogError("collPos" + collPos);

            Vector3 direct = collPos - treePos;
            //this.gameObject.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.localPosition.x *10, -transform.localPosition.y * 10));
            this.gameObject.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(direct.x*10,direct.y*10));

        }
    }
}
