using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {

    #region 引用
    public GameOverPanel theGameOverPanel;

    public GameObject protectBtn;
    public GameObject tree;
    public Text stageLabel;
    public Text stageNumLabel;

    public Transform LeftTopPos;
    public Transform LeftMidPos;
    public Transform LeftBottomPos;
    public Transform LeftAllPos;

    public Transform rightTopPos;
    public Transform rightMidPos;
    public Transform rightBottomPos;
    public Transform rightAllPos;

    public Transform bottomLeftPos;
    public Transform bottomRightPos;
    public Transform bottomAllPos;
    public Transform bottomMidPos;
    public Transform crazyBottomAllPos;

    public Transform FCRMovePos;
    public Transform FCSMovePos;
    public Transform FCRSmallLeftMovePos;
    public Transform FCRSmallRightMovePos;
    public Transform FCSSmallLeftMovePos;
    public Transform FCSSmallRightMovePos;

    public Transform bottomBollPos;

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
    public GameObject bottomMidMoveStock;
    public GameObject CrazyBottomAllMoveStock;

    public GameObject FCRMoveStock;
    public GameObject FCSMoveStock;
    public GameObject FCRSmallLeftMoveStock;
    public GameObject FCRSmallRightMoveStock;
    public GameObject FCSSmallLeftMoveStock;
    public GameObject FCSSmallRightMoveStock;


    public GameObject bottomBollMoveStock;

    public GameObject beginPos;  //物体生成位置
    public GameObject stageLabelPos; //关卡名称生成位置
    private GameObject LabelObj;

    #endregion

    #region 变量
    int num = 0;
    float timer = 0;
    int stageNum;
    #endregion

    #region 单例
    private static GamePanel instance;
    public static GamePanel Instance {
        get { return instance; }
    }

    #endregion

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
        Apply();
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


    public void Apply()
    {
        StopAllCoroutines();
        StartCoroutine(CreateColliderItem());
    }


    IEnumerator CreateColliderItem()
    {
        timer += Time.deltaTime;
        num = 0;
        if (PlayerPrefs.GetInt("StageRecord") == -1)
        {
            PlayerPrefs.SetInt("StageRecord", 1);
            stageNum = 1;
        }
        else
        {
            stageNum = PlayerPrefs.GetInt("StageRecord");
            stageNumLabel.text = stageNum.ToString();
        }
        
        for (int stage = 1; stage <= StageConfigManager.stageConfigList.Count; stage++)
        {
            if (stage > stageNum) {
                stageNum = stage;
                PlayerPrefs.SetInt("StageRecord", stageNum);
                stageNumLabel.text = stageNum.ToString();

            }
            StopAllAnimation();
            StageConfigManager.StageConfig cf;
            if (StartGame.isRandMode)
            {
                //随机生成关卡
                int stageRandom = Random.Range(1, StageConfigManager.stageConfigList.Count);
                cf = StageConfigManager.GetStageConfig(stageRandom);
            }
            else {
                cf = StageConfigManager.GetStageConfig(stage);
            }
            
            if (LabelObj != null)
                Destroy(LabelObj);

            Object stageLabelObj;
            stageLabelObj = Resources.Load(GameDefine.UIPrefabPath + "stageLabel");
            LabelObj = Instantiate((GameObject)stageLabelObj);
            LabelObj.SetActive(true);
            LabelObj.transform.parent = beginPos.transform;
            LabelObj.transform.localPosition = stageLabelPos.transform.localPosition;
            LabelObj.transform.localScale = Vector3.one;
            LabelObj.GetComponent<Text>().text = stage.ToString();
            stageLabel.text = stage.ToString();

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
                else if (cf.ItemTemplateList[i] == "LeftAllMove")
                {
                    LeftAllMove(200);
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
                else if (cf.ItemTemplateList[i] == "RightAllMove")
                {
                    RightAllMove(200);
                }
                else if (cf.ItemTemplateList[i] == "BottomLeftMove")
                {
                    BottomLeftMove(200);
                }
                else if (cf.ItemTemplateList[i] == "BottomRightMove")
                {
                    BottomRightMove(200);
                }
                else if (cf.ItemTemplateList[i] == "BottomAllMove")
                {
                    BottomAllMove(200);
                }
                else if (cf.ItemTemplateList[i] == "BottomMidMove")
                {
                    BottomMidMove(200);
                }
                else if (cf.ItemTemplateList[i] == "CrazyBottomAllMove")
                {
                    CrazyBottomAllMove(200);
                }
                else if (cf.ItemTemplateList[i] == "BottomBollMove")
                {
                    BottomBollMove(200);
                }
                else if (cf.ItemTemplateList[i] == "FCRMove")
                {
                    FCRMove(200);
                }
                else if (cf.ItemTemplateList[i] == "FCSMove")
                {
                    FCSMove(200);
                }
                else if (cf.ItemTemplateList[i] == "FCRSmallLeftMove")
                {
                    FCRSmallLeftMove(200);
                }
                else if (cf.ItemTemplateList[i] == "FCRSmallRightMove")
                {
                    FCRSmallRightMove(200);
                }
                else if (cf.ItemTemplateList[i] == "FCSSmallLeftMove")
                {
                    FCSSmallLeftMove(200);
                }
                else if (cf.ItemTemplateList[i] == "FCSSmallRightMove")
                {
                    FCSSmallRightMove(200);
                }
                else {
                    num++;
                    Object obj;
                    obj = Resources.Load(GameDefine.ItemPrefabPath + cf.ItemTemplateList[i]);
                    //  Debug.LogError(GameDefine.ItemPrefabPath + cf.ItemTemplateList[i]);
                    if (obj != null)
                    {
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
    }


    #region 播放四周动画

    public void LeftTopMove(float dis)
    {
        leftTopMoveStock.GetComponent<Animation>().Play("LeftTopMoveAnimation");
    }


    public void StopLeftTopMove()
    {
        if (leftTopMoveStock.GetComponent<Animation>() != null)
            leftTopMoveStock.GetComponent<Animation>().Stop();
            leftTopMoveStock.transform.localPosition = LeftTopPos.localPosition;
    }


    public void LeftMidMove(float dis)
    {
        leftMidMoveStock.GetComponent<Animation>().Play("LeftMidMoveAnimation");
    }

    public void StopLeftMidMove()
    {
        if (leftMidMoveStock.GetComponent<Animation>() != null)
            leftMidMoveStock.GetComponent<Animation>().Stop();
            leftMidMoveStock.transform.localPosition = LeftMidPos.localPosition;
    }


    public void LeftAllMove(float dis)
    {
        leftAllMoveStock.GetComponent<Animation>().Play("LeftAllMoveAnimation");
    }

    public void StopLeftAllMove()
    {
        if (leftAllMoveStock.GetComponent<Animation>() != null)
            leftAllMoveStock.GetComponent<Animation>().Stop();
        leftAllMoveStock.transform.localPosition = LeftAllPos.localPosition;
    }


    public void LeftBottomMove(float dis)
    {
        leftBottomMoveStock.GetComponent<Animation>().Play("LeftBottomMoveAnimation");
    }

    public void StopLeftBottomMove()
    {
        if(leftBottomMoveStock.GetComponent<Animation>() != null)
            leftBottomMoveStock.GetComponent<Animation>().Stop();
            leftBottomMoveStock.transform.localPosition = LeftBottomPos.localPosition;
    }

    public void RightTopMove(float dis)
    {
        rightTopMoveStock.GetComponent<Animation>().Play("RightTopMoveAnimation");
    }

    public void StopRightTopMove()
    {
        if (rightTopMoveStock.GetComponent<Animation>() != null)
            rightTopMoveStock.GetComponent<Animation>().Stop();
            rightTopMoveStock.transform.localPosition = rightTopPos.localPosition;
    }


    public void RightMidMove(float dis)
    {
        rightMidMoveStock.GetComponent<Animation>().Play("RightMidMoveAnimation");
    }

    public void StopRightMidMove()
    {
        if (rightMidMoveStock.GetComponent<Animation>() != null)
            rightMidMoveStock.GetComponent<Animation>().Stop();
            rightMidMoveStock.transform.localPosition = rightMidPos.localPosition;
    }


    public void RightBottomMove(float dis)
    {
        rightBottomMoveStock.GetComponent<Animation>().Play("RightBottomMoveAnimation");
    }

    public void StopRightBottomMove()
    {
        if (rightBottomMoveStock.GetComponent<Animation>() != null)
            rightBottomMoveStock.GetComponent<Animation>().Stop();
            rightBottomMoveStock.transform.localPosition = rightBottomPos.localPosition;
    }

    public void RightAllMove(float dis)
    {
        rightAllMoveStock.GetComponent<Animation>().Play("RightAllMoveAnimation");
    }

    public void StopAllRightMove()
    {
        if (rightAllMoveStock.GetComponent<Animation>() != null)
            rightAllMoveStock.GetComponent<Animation>().Stop();
        rightAllMoveStock.transform.localPosition = rightAllPos.localPosition;
    }

    public void BottomLeftMove(float dis)
    {
        bottomLeftMoveStock.GetComponent<Animation>().Play("BottomLeftMoveAnimation");
    }

    public void StopBottomLeftMove()
    {
        if (bottomLeftMoveStock.GetComponent<Animation>() != null)
            bottomLeftMoveStock.GetComponent<Animation>().Stop();
            bottomLeftMoveStock.transform.localPosition = bottomLeftPos.localPosition;
    }

    public void BottomRightMove(float dis)
    {
        bottomRightMoveStock.GetComponent<Animation>().Play("BottomRightMoveAnimation");
    }

    public void StopBottomRightMove()
    {
        if (bottomRightMoveStock.GetComponent<Animation>() != null)
            bottomRightMoveStock.GetComponent<Animation>().Stop();
            bottomRightMoveStock.transform.localPosition = bottomRightPos.localPosition;
    }

    public void BottomMidMove(float dis)
    {
        bottomMidMoveStock.GetComponent<Animation>().Play("BottomMidMoveAnimation");
    }

    public void StopBottomMidMove()
    {
        if (bottomMidMoveStock.GetComponent<Animation>() != null)
            bottomMidMoveStock.GetComponent<Animation>().Stop();
        bottomMidMoveStock.transform.localPosition = bottomMidPos.localPosition;
    }

    public void BottomAllMove(float dis)
    {
        bottomAllMoveStock.GetComponent<Animation>().Play("BottomAllMoveAnimation");
    }

    public void StopAllBottomMove()
    {
        if (bottomAllMoveStock.GetComponent<Animation>() != null)
            bottomAllMoveStock.GetComponent<Animation>().Stop();
        bottomAllMoveStock.transform.localPosition = bottomAllPos.localPosition;
    }

    public void CrazyBottomAllMove(float dis)
    {
        CrazyBottomAllMoveStock.GetComponent<Animation>().Play("CrazyBottomAllMoveAnimation");
    }

    public void StopCrazyBottomAllMove()
    {
        if (CrazyBottomAllMoveStock.GetComponent<Animation>() != null)
            CrazyBottomAllMoveStock.GetComponent<Animation>().Stop();
        CrazyBottomAllMoveStock.transform.localPosition = crazyBottomAllPos.localPosition;
    }

    public void BottomBollMove(float dis)
    {
        bottomBollMoveStock.GetComponent<Animation>().Play("BottomBollMoveAnimation");
    }

    public void StopBottomBollMove()
    {
        if (bottomBollMoveStock.GetComponent<Animation>() != null)
            bottomBollMoveStock.GetComponent<Animation>().Stop();
        bottomBollMoveStock.transform.localPosition = bottomBollPos.localPosition;
    }

    public void FCRMove(float dis)
    {
        FCRMoveStock.SetActive(true);
        FCRMoveStock.GetComponent<Animation>().Play("FCRMoveAnimation");
    }

    public void StopFCRMove()
    {
        if (FCRMoveStock.GetComponent<Animation>() != null)
            FCRMoveStock.GetComponent<Animation>().Stop();
        FCRMoveStock.SetActive(false);
    }


    public void FCSMove(float dis)
    {
        FCSMoveStock.SetActive(true);
        FCSMoveStock.GetComponent<Animation>().Play("FCSMoveAnimation");
    }

    public void StopFCSMove()
    {
        if (FCSMoveStock.GetComponent<Animation>() != null)
            FCSMoveStock.GetComponent<Animation>().Stop();
        FCSMoveStock.SetActive(false);
    }


    public void FCRSmallLeftMove(float dis)
    {
        FCRSmallLeftMoveStock.SetActive(true);
        FCRSmallLeftMoveStock.GetComponent<Animation>().Play("FCRMoveAnimation");
    }

    public void StopFCRSmallLeftMove()
    {
        if (FCRSmallLeftMoveStock.GetComponent<Animation>() != null)
            FCRSmallLeftMoveStock.GetComponent<Animation>().Stop();
        FCRSmallLeftMoveStock.SetActive(false);
    }

    public void FCRSmallRightMove(float dis)
    {
        FCRSmallRightMoveStock.SetActive(true);
        FCRSmallRightMoveStock.GetComponent<Animation>().Play("FCRMoveAnimation");
    }

    public void StopFCRSmallRightMove()
    {
        if (FCRSmallRightMoveStock.GetComponent<Animation>() != null)
            FCRSmallRightMoveStock.GetComponent<Animation>().Stop();
        FCRSmallRightMoveStock.SetActive(false);
    }

    public void FCSSmallRightMove(float dis)
    {
        FCSSmallRightMoveStock.SetActive(true);
        FCSSmallRightMoveStock.GetComponent<Animation>().Play("FCSMoveAnimation");
    }

    public void StopFCSSmallRightMove()
    {
        if (FCSSmallRightMoveStock.GetComponent<Animation>() != null)
            FCSSmallRightMoveStock.GetComponent<Animation>().Stop();
        FCSSmallRightMoveStock.SetActive(false);
    }

    public void FCSSmallLeftMove(float dis)
    {
        FCSSmallLeftMoveStock.SetActive(true);
        FCSSmallLeftMoveStock.GetComponent<Animation>().Play("FCSMoveAnimation");
    }

    public void StopFCSSmallLeftMove()
    {
        if (FCSSmallLeftMoveStock.GetComponent<Animation>() != null)
            FCSSmallLeftMoveStock.GetComponent<Animation>().Stop();
        FCSSmallLeftMoveStock.SetActive(false);
    }
    #endregion


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
 //               Debug.LogError("##"+ GameData.Instance.colliderList[i].name);
                GameData.Instance.colliderList[i].transform.Find("CollideImg").gameObject.SetActive(false);
                GameData.Instance.colliderList[i].transform.Find("BoomImg").gameObject.SetActive(true);
                yield return new WaitForSeconds(0.02f);
                Destroy(GameData.Instance.colliderList[i]);
            }
        }
        
        Clear();
    }


    public void StopAllAnimation()
    {
        StopBottomLeftMove();
        StopBottomRightMove();
        StopAllBottomMove();
        StopBottomMidMove();
        StopCrazyBottomAllMove();

        StopLeftBottomMove();
        StopLeftMidMove();
        StopLeftTopMove();
        StopLeftAllMove();

        StopRightBottomMove();
        StopRightMidMove();
        StopRightTopMove();
        StopAllRightMove();

        StopFCRMove();
        StopFCRSmallLeftMove();
        StopFCRSmallRightMove();
        StopFCSMove();
        StopFCSSmallLeftMove();
        StopFCSSmallRightMove();

        StopBottomBollMove();
    }
}
