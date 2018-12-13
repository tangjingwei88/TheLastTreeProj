using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderItem : MonoBehaviour {

    private AudioSource audio;
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "topDestory")
        {
            Destroy(this.gameObject);
        }
        
        else if (coll.transform.tag == "protect")
        {
            //带"boom"碰到太极圈自动爆炸
            if (transform.gameObject.name.Contains("boom"))
            {
                //机器震动效果
                if (GameData.Instance.isShake)
                {
                    Handheld.Vibrate();
                }
                StartCoroutine(BoomSelf(this.gameObject));
            }
            //碰撞有穿透问题，目前引擎没有方法解决，给碰撞体添加相反的力模拟碰撞
            else
            {
                if (GameData.Instance.isMusic)
                {
                    AudioClip collideClip = (AudioClip)Resources.Load(GameDefine.AudioPath + GetAudioName(transform.gameObject.name));
                    if (collideClip != null)
                    {
                        AudioSource.PlayClipAtPoint(collideClip, transform.position);
                    }
                }
                //根据tag找到气球，获取位置
                GameObject tree = GameObject.FindGameObjectWithTag("tree");
                Vector3 treePos = tree.transform.localPosition;
                Vector3 collPos = coll.gameObject.GetComponent<RectTransform>().localPosition;
               // Debug.LogError("##treePos:" + treePos);
               // Debug.LogError("##collPos:" + collPos);
                Vector3 direct = collPos - treePos;
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(direct.x * GameData.Instance.protectPower, direct.y * GameData.Instance.protectPower));
                transform.Find("CollideImg").gameObject.SetActive(true);
            }
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


    IEnumerator BoomSelf(GameObject obj)
    {
        obj.transform.Find("Image").gameObject.SetActive(false);
        obj.transform.Find("CollideImg").gameObject.SetActive(false);
        obj.transform.Find("BoomImg").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Destroy(obj);
    }

    //根据prefab名字拿audio资源
    public string GetAudioName(string str)
    {
        return str.Substring(0,2);
    }
}
