using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {

    public GameOverPanel theGameOverPanel;

    public GameObject protectBtn;
    public GameObject tree;
    public Text stageLabel;

    public GameObject leftAllMoveStock;
    public GameObject leftTopMoveStock;
    public GameObject leftMidMoveStock;
    public GameObject leftBottomMoveStock;

    public GameObject rightAllMoveStock;
    public GameObject rightTopMoveStock;
    public GameObject rightMidMoveStock;
    public GameObject rightBottomMoveStock;

    public GameObject bottomAllMoveStock;
    public GameObject bottomLeftMoveStock;
    public GameObject bottomRightMoveStock;

    public GameObject beginPos;  //物体生成位置
    public GameObject stageLabelPos; //关卡名称生成位置
    private GameObject LabelObj;
    int num = 0;
    float timer = 0;

    private static GamePanel instance;
    public static GamePanel Instance {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        StageConfigManager.Init();
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
            theGameOverPanel.Apply(timer);
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
        timer += Time.deltaTime;
        num = 0;
        for (int stage = 1; stage <= StageConfigManager.stageConfigList.Count; stage++)
        {
            StageConfigManager.StageConfig cf = StageConfigManager.GetStageConfig(stage);
            
            if (LabelObj != null)
                Destroy(LabelObj);

            Object stageLabelObj;
            stageLabelObj = Resources.Load(GameDefine.UIPrefabPath + "stageLabel");
            LabelObj = Instantiate((GameObject)stageLabelObj);
            LabelObj.SetActive(true);
            LabelObj.transform.parent = beginPos.transform;
            LabelObj.transform.localPosition = stageLabelPos.transform.localPosition;
            LabelObj.transform.localScale = Vector3.one;
            LabelObj.GetComponent<Text>().text = cf.ID.ToString();
            stageLabel.text = cf.ID.ToString();

            GameData.Instance.protectPower = cf.ProtectPower;
            GameData.Instance.protectRotateSpeed = cf.ProtectRotateSpeed;
            GameData.Instance.protectRotateInnerSpeed = cf.ProtectRotateInnerSpeed;
            GameData.Instance.JGBRotateSpeed = cf.JGBRotateSpeed;
            GameData.Instance.JGBPower = cf.JGBPower;

            yield return new WaitForSeconds(1);
            Destroy(LabelObj);

            for (int i = 0; i < cf.ItemTemplateList.Count; i++)
            {
                if (cf.ItemTemplateList[i] == "LeftTopMove") {
                    LeftTopMove(200);
                }
                else if (cf.ItemTemplateList[i] == "LeftMidMove")
                {
                    LeftMidMove(200);
                }
                else if (cf.ItemTemplateList[i] == "LeftBottomMove")
                {
                    LeftBottomMove(200);
                }
                else if (cf.ItemTemplateList[i] == "RightTopMove")
                {
                    RightTopMove(200);
                }
                else if (cf.ItemTemplateList[i] == "RightMidMove")
                {
                    RightMidMove(200);
                }
                else if (cf.ItemTemplateList[i] == "RightBottomMove")
                {
                    RightBottomMove(200);
                }
                else if (cf.ItemTemplateList[i] == "BottomLeftMove")
                {
                    BottomLeftMove(200);
                }
                else if (cf.ItemTemplateList[i] == "BottomRightMove")
                {
                    BottomRightMove(200);
                }
                else {
                    num++;
                    Object obj;
                    obj = Resources.Load(GameDefine.ItemPrefabPath + cf.ItemTemplateList[i]);
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

                    yield return new WaitForSeconds(5);

                }
            }
        }
    }


    public void LeftTopMove(float dis)
    {
        leftTopMoveStock.GetComponent<Animation>().Play("LeftTopMoveAnimation");
    }



    public void LeftMidMove(float dis)
    {
        leftMidMoveStock.GetComponent<Animation>().Play("LeftMidMoveAnimation");
    }



    public void LeftBottomMove(float dis)
    {
        leftBottomMoveStock.GetComponent<Animation>().Play("LeftBottomMoveAnimation");
    }



    public void RightTopMove(float dis)
    {
        rightTopMoveStock.GetComponent<Animation>().Play("RightTopMoveAnimation");
    }



    public void RightMidMove(float dis)
    {
        rightMidMoveStock.GetComponent<Animation>().Play("RightMidMoveAnimation");
    }



    public void RightBottomMove(float dis)
    {
        rightBottomMoveStock.GetComponent<Animation>().Play("RightBottomMoveAnimation");
    }



    public void BottomLeftMove(float dis)
    {
        bottomLeftMoveStock.GetComponent<Animation>().Play("BottomLeftMoveAnimation");
    }



    public void BottomRightMove(float dis)
    {
        bottomRightMoveStock.GetComponent<Animation>().Play("BottomRightMoveAnimation");
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
            //    AudioClip collideClip = (AudioClip)Resources.Load(GameDefine.AudioPath + "boom");
            //    AudioSource.PlayClipAtPoint(collideClip, transform.position);
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
