using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool pressed;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Golem")
            pressed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "Golem")
            pressed = false;
    }
}
