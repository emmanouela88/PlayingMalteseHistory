using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveScript : MonoBehaviour // Move Coin
{
    public float speed = 3f;
  //  public int m_coinScore = 0f; // TODO

    void Update()
    {
        MoveDown();
    }

    void MoveDown()
    {
        if (CreateObjects.m_playing)
        {
            gameObject.transform.position -= new Vector3(0f, speed * 0.05f, 0f);
        }
    }
}
