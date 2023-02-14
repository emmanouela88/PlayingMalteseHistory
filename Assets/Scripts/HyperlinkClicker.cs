using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using TMPro;

public class HyperlinkClicker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI txtMainInfo;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(txtMainInfo, Input.mousePosition, null);
        //Debug.Log(linkIndex);
        if (linkIndex > -1)
        {
            string linkID = txtMainInfo.textInfo.linkInfo[linkIndex].GetLinkID();
            //Debug.Log(linkID);

            GameObject linkedObject = GameObject.Find(linkID);

            if (linkedObject != null)
			{
                linkedObject.transform.GetChild(0).gameObject.SetActive(true);
			}
			else
			{
                Application.OpenURL(linkID);
            }
        }
    }
}
