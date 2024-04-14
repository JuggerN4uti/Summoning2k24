using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public SpriteRenderer ButtonSprite;
    public bool pressed;

    public Sprite UnpressedButtonSprite, PressedButtonSprite;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Golem")
        {
            pressed = true;
            ButtonSprite.sprite = PressedButtonSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Golem")
        {
            pressed = false;
            ButtonSprite.sprite = UnpressedButtonSprite;
        }
    }
}
