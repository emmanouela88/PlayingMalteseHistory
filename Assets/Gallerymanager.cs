using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Gallerymanager : MonoBehaviour
{
    #region VARIABLES[SerializeField] private Button btnPrev;  
    [SerializeField] private Image imgShow;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnPrev;
    [Space(10)]
    [SerializeField] private List<Sprite> imagesForShow;
    private int indexImagesForShow;
    #endregion
    #region METHODS_MONOBEHAVIOUR  
    private void Start()
    {
        imgShow.sprite = imagesForShow[0];
        btnPrev.onClick.AddListener(OnButtonPrevClicked);
        btnNext.onClick.AddListener(OnButtonNextClicked);
    }
    #endregion
    #region METHODS_PRIVATE  
    private void OnButtonPrevClicked()
    {
        indexImagesForShow--;
        if (indexImagesForShow < 0)
        {
            
            indexImagesForShow = imagesForShow.Count - 1;
            
        }
        
        imgShow.sprite = imagesForShow[indexImagesForShow];
    }
    private void OnButtonNextClicked()
    {
        indexImagesForShow++;
        if (indexImagesForShow > imagesForShow.Count - 1)
        {
            indexImagesForShow = 0;
            
        }
        
        imgShow.sprite = imagesForShow[indexImagesForShow];
    }
    #endregion
}  
 

