using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour {

    public GameOverPanel theGameOverPanel;

    public GameObject protectBtn;
    public GameObject tree;

    public GameObject beginPos;  //物体生成位置

    int num = 0;

    private static GamePanel instance;
    public static GamePanel Instance {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        Init();
    }

    public void Init()
    {
        protectBtn.transform.localPosition = new Vector3(0,-150,0);
        CreatePubbleItem();
        List<string> templateList = new List<string>();
        templateList.Add("CarTemplate");
        templateList.Add("JingGuBangTemplate");
        templateList.Add("SquadGroupTemplate");
        templateList.Add("AntGroupTemplate");
        templateList.Add("LuDengTemplate");
        templateList.Add("buppleGroupTemplate");
        templateList.Add("AnimDog3Template");
        templateList.Add("JingGuBangTemplate");
        templateList.Add("AnimDog4Template");
        templateList.Add("CarTemplate");
        templateList.Add("AnimDogTemplate");
        templateList.Add("AntGroupTemplate");
        templateList.Add("LuDengTemplate");
        templateList.Add("CarTemplate");
        templateList.Add("JingGuBangTemplate");
        templateList.Add("AntTemplate");
        templateList.Add("buppleGroupTemplate");
        templateList.Add("CarTemplate");
        templateList.Add("CarTemplate");
        templateList.Add("SquadGroupTemplate");
        Apply(templateList);
    }


    void Update()
    {
        if (GameData.Instance.isLost)
        {
            theGameOverPanel.Apply();
            Clear();
            GameData.Instance.isLost = false;
            StopAllCoroutines();
        }
    }


    public void Apply(List<string> templateList)
    {
        StopAllCoroutines();
        StartCoroutine(CreateColliderItem(templateList));
    }


    IEnumerator CreateColliderItem(List<string> templateList)
    {
        int timer = 0;
        num = 0;
        for (int i = 0; i < templateList.Count; i++)
        {
            num++;
            timer++;
            Object obj;
            if (timer % 10 == 0)
            {
                CreateBoom();
            }

            obj = Resources.Load(GameDefine.ItemPrefabPath + templateList[i]);
          //  Debug.LogError(GameDefine.ItemPrefabPath + templateList[i]);
            GameObject item = Instantiate((GameObject)obj);
            item.SetActive(true);
            item.transform.parent = beginPos.transform;
            item.transform.localPosition = beginPos.transform.localPosition;
            item.transform.localScale = Vector3.one;

            if (item.name.Contains("Group"))
            {
                foreach (Transform child in item.transform)
                {
                    num++;
                    //                    Debug.LogError("##" + child.name);
                    child.gameObject.name += num;
                    GameData.Instance.colliderList.Add(child.gameObject);
                }
                GameData.Instance.colliderList.Add(item);
            }
            else
            {
                item.name += num;
                GameData.Instance.colliderList.Add(item);
            }


            if (i == templateList.Count -1) i = 0;


            yield return new WaitForSeconds(5);
        }
    }

    //生成气球
    public void CreatePubbleItem()
    {
        num++;
        Object obj;
        obj = Resources.Load(GameDefine.UIPrefabPath + "PubbleItem");

        GameObject item = Instantiate((GameObject)obj);
        item.name += num;
        item.SetActive(true);
        item.transform.parent = protectBtn.transform.parent;
        item.transform.localPosition = new Vector3(protectBtn.GetComponent<RectTransform>().localPosition.x, protectBtn.GetComponent<RectTransform>().localPosition.y -200, 0);
        item.transform.localScale = Vector3.one;
    }

    //生成炸弹
    public void CreateBoom()
    {
        num++;
        Object obj;
        obj = Resources.Load(GameDefine.ItemPrefabPath + "BoomTemplate");

        GameObject item = Instantiate((GameObject)obj);
        item.name += num;
        GameData.Instance.colliderList.Add(item);

        item.SetActive(true);
        item.transform.parent = beginPos.transform;
        item.transform.localPosition = beginPos.transform.localPosition;
        item.transform.localScale = Vector3.one;
    }

    public void Clear()
    {
        if (GameData.Instance.colliderList != null)
        {
            for (int i = 0; i < GameData.Instance.colliderList.Count; i++)
            {
                Destroy(GameData.Instance.colliderList[i].gameObject);
            }
            GameData.Instance.colliderList.Clear();
        }
    }

    public void BoomClear()
    {
        if (GameData.Instance.colliderList != null)
        {
           StartCoroutine(ItemBoomClear());
           // GameData.Instance.colliderList.Clear();
        }
    }


    IEnumerator ItemBoomClear()
    {
        for (int i = 0; i < GameData.Instance.colliderList.Count; i++)
        {
            if (GameData.Instance.colliderList[i] != null && 
                !GameData.Instance.colliderList[i].gameObject.name.Contains("Group")
                )
            {
                GameData.Instance.colliderList[i].transform.Find("Image").gameObject.SetActive(false);
                Debug.LogError("##"+ GameData.Instance.colliderList[i].name);
                GameData.Instance.colliderList[i].transform.Find("CollideImg").gameObject.SetActive(false);
                GameData.Instance.colliderList[i].transform.Find("BoomImg").gameObject.SetActive(true);
                yield return new WaitForSeconds(0.02f);
                Destroy(GameData.Instance.colliderList[i]);
            }
        }
        
        Clear();
    }
}
