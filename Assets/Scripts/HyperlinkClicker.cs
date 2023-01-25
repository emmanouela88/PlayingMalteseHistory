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
        Debug.Log(linkIndex);
        if (linkIndex > -1)
        {
            Application.OpenURL(txtMainInfo.textInfo.linkInfo[linkIndex].GetLinkID());
            Debug.Log(txtMainInfo.textInfo.linkInfo[linkIndex].GetLinkID());
        }
    }
}
