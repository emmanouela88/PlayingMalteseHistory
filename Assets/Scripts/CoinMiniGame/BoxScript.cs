using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour // Used to move the plate on device
{
    float startPosX;
    float startPosY;

    private Vector3 position;
    private float width;
    private float height;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }

    void Update()
    {

#if !UNITY_EDITOR

        if (CreateObjects.m_playing)
        {
            Touch touch = Input.GetTouch(0);

            // Move the plate if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                pos.x = (pos.x - width) / width;
                pos.y = (pos.y - height) / height;
                position = new Vector3(pos.x, transform.position.y, 0.0f);

                // Position the plate
                transform.position = position;
            }
        }
#endif
    }
}
