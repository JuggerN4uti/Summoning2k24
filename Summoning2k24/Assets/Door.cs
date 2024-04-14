using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public SpriteRenderer DoorSprite;
    public Collider2D DoorCollider;
    public ButtonScript[] Buttons;
    public bool viable;

    public Sprite OpenedDoorSprite, ClosedDoorSprite;

    void Update()
    {
        if (AllPressed())
        {
            DoorSprite.sprite = OpenedDoorSprite;
            DoorCollider.enabled = true;
        }
        else
        {
            DoorSprite.sprite = ClosedDoorSprite;
            DoorCollider.enabled = false;
        }
    }

    bool AllPressed()
    {
        viable = true;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (!Buttons[i].pressed)
                viable = false;
        }
        return viable;
    }
}
