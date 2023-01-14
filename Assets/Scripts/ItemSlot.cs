using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject correctText;
    [SerializeField]
    private DragAndDrop dd;
    public void OnDrop(PointerEventData eventData)
    {
        
       if(eventData.pointerDrag != null)
        {
            /*eventData.pointerDrag.GetComponent<RectTransform>().gameObject.transform.position = GetComponent<RectTransform>().transform.position;
            Debug.Log("Pos: "+eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition);*/
            if(dd.draggedObject == correctText)
            {
                GameObject dropped = eventData.pointerDrag;
                DragAndDrop dragObj = dropped.GetComponent<DragAndDrop>();
                dragObj.rectTransform = transform;
            }
            
        }
    }
}
