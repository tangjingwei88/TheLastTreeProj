using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour,IDragHandler
{
    private Vector3 nextPos;
    void Update()
    {
        Vector3 oriPos = transform.position;//记录原来的位置
        //transform.Translate(Vector3.forward * speed * Time.deltaTime); //移动
        float length = (nextPos - oriPos).magnitude;//射线的长度
        Vector3 direction = nextPos - oriPos;//方向
        RaycastHit hitinfo;
        bool isCollider = Physics.Raycast(oriPos, direction, out hitinfo, length);//在两个位置之间发起一条射线，然后通过这条射线去检测有没有发生碰撞
        if (isCollider)
        {
            //中间有碰撞，不移动，回到原来位置
            transform.position = oriPos;
		}
    }


    public void OnDrag(PointerEventData eventData)
    {
        //拖拽移动图片
        SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        var rt = gameObject.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            nextPos = rt.position;
            rt.position = globalMousePos;
           // Debug.LogError("rt.localPosition" + rt.localPosition);
            GameData.Instance.protectPos = rt.localPosition;
        }
    }

}
