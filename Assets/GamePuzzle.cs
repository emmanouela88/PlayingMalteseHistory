using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GamePuzzle : MonoBehaviour
{
    [Header("Image Pieces")]
    public GameObject imgPrefabPiece;
    GameObject[,] imgPrefabPieceList;
    public RectTransform imgParent;
    public Sprite[] imgList;

    [Space]
    [Header("Image Pieces")]
    public GameObject btnPrefabPiece;
    GameObject[,] btnPrefabList;
    public RectTransform btnParent;

    
    // Start is called before the first frame update
    void Start()
    {
        imgPrefabPieceList = new GameObject[5, 3];
        btnPrefabList = new GameObject[5, 3];
        CreateImagePieces();
        CreateButtonPieces();
    }

    void CreateImagePieces()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                imgPrefabPieceList[j, i] = Instantiate(imgPrefabPiece);
                if (i >= 1)
                {
                    int temp = 0;
                    temp = i* 4;
                    imgPrefabPieceList[j, i].GetComponent<Image>().sprite = imgList[(j + i)+temp];
                    
                }
                else
                {
                    imgPrefabPieceList[j, i].GetComponent<Image>().sprite = imgList[j + i];
                   
                }
                
                imgPrefabPieceList[j, i].transform.parent = imgParent;
                imgPrefabPieceList[j, i].transform.localPosition = new Vector2(j * 200, i * -200);
            }

        }
    }
    //Random random = new Random();
    void CreateButtonPieces()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                btnPrefabList[j, i] = Instantiate(btnPrefabPiece);
                if (i >= 1)
                {
                    int temp = 0;//to change correctly on each y axis the pictures
                    int rnd = Random.Range(0,4);//to use it when loading the button images to appear randomly
                    temp = i*4;
                    btnPrefabList[j, i].GetComponent<Image>().sprite = imgList[(j + i) + temp];
                    
                }
                else
                {
                    btnPrefabList[j, i].GetComponent<Image>().sprite = imgList[j + i];
                    
                }


                btnPrefabList[j, i].transform.parent = btnParent;                
                btnPrefabList[j, i].transform.localPosition = new Vector2(j * 200, i * -200);
                //btnPrefabList[i, j].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            }

        }
    }
}
