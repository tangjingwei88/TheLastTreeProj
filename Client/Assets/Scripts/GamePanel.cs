using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour {

    public GameOverPanel theGameOverPanel;

    public GameObject protectBtn;
    public GameObject tree;

    public GameObject beginPos;  //物体生成位置
    public List<GameObject> colliderList;

    private static GamePanel instance = new GamePanel();
    public static GamePanel Instance {
        get { return instance; }
    }

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        Debug.LogError(protectBtn.transform.localPosition);
        Debug.LogError(tree.gameObject.transform.localPosition.x);
        Debug.LogError(tree.gameObject.transform.localPosition.y);
        protectBtn.transform.localPosition = new Vector3(tree.gameObject.transform.localPosition.x,tree.gameObject.transform.localPosition.y + 200,0);
        List<string> templateList = new List<string>();
        templateList.Add("LineTemplate");
        templateList.Add("SquadeTemplate");
        templateList.Add("TwoSideTemplate");

        Apply(templateList);
    }


    void Update()
    {
        if (GameData.Instance.isLost)
        {
            theGameOverPanel.Apply();
            Clear();
            GameData.Instance.isLost = false;
        }
    }


    public void Apply(List<string> templateList)
    {
        StartCoroutine(CreateColliderItem(templateList));
    }


    IEnumerator CreateColliderItem(List<string> templateList)
    {
        for (int i = 0; i < templateList.Count; i++)
        {
            Debug.LogError(GameDefine.UIPrefabPath);
            Object obj = Resources.Load(GameDefine.UIPrefabPath + templateList[i]);
            GameObject item = Instantiate((GameObject)obj);
            colliderList.Add(item);

            item.SetActive(true);
            item.transform.parent = beginPos.transform;
            item.transform.localPosition = beginPos.transform.localPosition;
            item.transform.localScale = Vector3.one;
            if (i == 2) i = 0;
            yield return new WaitForSeconds(5);
        }
    }

    public void Clear()
    {
        if (colliderList != null)
        {
            for (int i = 0; i < colliderList.Count; i++)
            {
                Destroy(colliderList[i].gameObject);
            }
            colliderList.Clear();
        }
    }
}
