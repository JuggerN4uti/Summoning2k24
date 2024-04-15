using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public SpriteRenderer BarrierSprite;
    public Collider2D BarrierCollider;
    public ButtonScript[] Buttons;
    public bool viable;

    void Update()
    {
        if (AllPressed())
        {
            BarrierSprite.color = new Color(0.278f, 0f, 0.898f, 0.2f);
            BarrierCollider.enabled = false;
        }
        else
        {
            BarrierSprite.color = new Color(0.278f, 0f, 0.898f, 1f);
            BarrierCollider.enabled = true;
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
