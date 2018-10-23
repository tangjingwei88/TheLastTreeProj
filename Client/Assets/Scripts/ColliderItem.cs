using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderItem : MonoBehaviour {

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "topDestory")
        {
            Destroy(this.gameObject);
        }
        //碰撞有穿透问题，目前引擎没有方法解决，给碰撞体添加相反的力模拟碰撞
        else if (coll.transform.tag == "protect")
        {
            //根据tag找到气球，获取位置
            GameObject tree = GameObject.FindGameObjectWithTag("tree");
            Vector3 treePos = tree.transform.localPosition;
            Vector3 collPos = coll.gameObject.GetComponent<RectTransform>().localPosition;

            Vector3 direct = collPos - treePos;
            //transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(direct.x*10,direct.y*10));
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(direct.x * GameData.Instance.protectPower, direct.y * GameData.Instance.protectPower));

            //            Debug.LogError("@@" + transform.gameObject.name);
            transform.Find("CollideImg").gameObject.SetActive(true);
        }

        //给转动的棍子添加反作用力
        else if (coll.gameObject.name.Contains("JingGuBang"))
        {
            GameObject tree = GameObject.FindGameObjectWithTag("tree");
            Vector3 treePos = tree.transform.localPosition;
            Vector3 collPos = coll.gameObject.GetComponent<RectTransform>().localPosition;

            Vector3 direct = collPos - treePos;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(direct.x * GameData.Instance.JGBPower, -direct.y * GameData.Instance.JGBPower));
        }
    }
}
