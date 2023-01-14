using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    float startPosX;
    float startPosY;
    bool isBeingHeld = false;



    private Vector3 position;
    private float width;
    private float height;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        // Position used for the cube.
      //  position = new Vector3(0.0f, 0.0f, 0.0f);
    }



    // Update is called once per frame
    void Update()
    {

#if !UNITY_EDITOR
        Touch touch = Input.GetTouch(0);


        // Move the cube if the screen has the finger moving.
        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 pos = touch.position;
            pos.x = (pos.x - width) / width;
            pos.y = (pos.y - height) / height;
            position = new Vector3(pos.x, transform.position.y, 0.0f);

            // Position the cube.
            transform.position = position;





            //if (isBeingHeld)
            //{
            //    Vector3 mousePos;
            //    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);



            //    this.gameObject.transform.localPosition = new Vector3(mousePos.x, 0f, 0f);




            //    Vector3 viewPos = Camera.main.WorldToViewportPoint(this.transform.position);
            //    viewPos.x = Mathf.Clamp01(viewPos.x);
            //    this.transform.position = Camera.main.ViewportToWorldPoint(viewPos);

            //}

          
            // HERE
         //   Vector3 viewPos = Camera.main.WorldToViewportPoint(this.transform.position);
         //   viewPos.x = Mathf.Clamp01(viewPos.x);
          //  this.transform.position = Camera.main.ViewportToWorldPoint(viewPos);
       
        }
#endif
    }


    
}
