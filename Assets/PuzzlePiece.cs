using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private SpriteRenderer _renderer;
    //can put audio if I want for sounds, both audiosource and clips
    bool isMoving,isPlaced;
    Vector2 _offset,orPos;
    PuzzleSlot _slot;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public void Init(PuzzleSlot slot)
    {
        _renderer.sprite = slot._slotRenderer.sprite;
    }
    private void Awake()
    {
        orPos = transform.position;
        _slot = FindObjectOfType<PuzzleSlot>();
    }
    private void Update()
    {
        if (isPlaced) return;
        if (!isMoving) return;

        var mousePos = GetMousePos() - _offset;

        transform.position = mousePos;
    }
    private void OnMouseDown()
    {
        isMoving = true;
        //_offset = GetMousePos()-(Vector2)transform.position;
        Cursor.SetCursor(_renderer.sprite.texture, hotSpot, cursorMode);
    }
    private void OnMouseUp()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        if (Vector2.Distance(transform.position, _slot.transform.position) > 3)
        {
            transform.position = _slot.transform.position;
            _slot.Placed();
            isPlaced = true;
            
        }
        else
        {
            transform.position = orPos;
            isMoving = false;
        }
        
    }
    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
}
