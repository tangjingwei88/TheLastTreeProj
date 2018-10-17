using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour {

    public GameObject beginPos;
    public GameObject obj;
    public List<string> templateList;

    void Start()
    {
        templateList.Add("LineTemplate");
        templateList.Add("SquadeTemplate");
        templateList.Add("TwoSideTemplate");

        Apply(templateList);
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

            item.SetActive(true);
            item.transform.parent = beginPos.transform;
            item.transform.localPosition = beginPos.transform.localPosition;
            item.transform.localScale = Vector3.one;
            if (i == 2) i = 0;
            yield return new WaitForSeconds(5);
        }
    }
}
