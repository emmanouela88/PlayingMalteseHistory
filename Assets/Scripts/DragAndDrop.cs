using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    public Transform rectTransform;
    private CanvasGroup canvasGroup;
    public GameObject draggedObject;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        draggedObject = this.gameObject;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        rectTransform = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition/canvas.scaleFactor;
        //rectTransform.position += eventData.position / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(rectTransform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    
}
