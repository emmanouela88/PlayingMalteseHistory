using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
       if(eventData.pointerDrag != null)
        {
            /*eventData.pointerDrag.GetComponent<RectTransform>().gameObject.transform.position = GetComponent<RectTransform>().transform.position;
            Debug.Log("Pos: "+eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition);*/
            GameObject dropped = eventData.pointerDrag;
            DragAndDrop dragObj = dropped.GetComponent<DragAndDrop>();
            dragObj.rectTransform = transform;
        }
    }
}
