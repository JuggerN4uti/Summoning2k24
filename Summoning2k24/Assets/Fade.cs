using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image BlackFade;
    public float transparencyValue;
    public bool begin;

    void Start()
    {
        transparencyValue = 1f;
        begin = true;
    }

    public void FadeOut()
    {
        transparencyValue = 0f;
        begin = false;
    }

    void Update()
    {
        if (begin && transparencyValue > 0f)
        {
            transparencyValue -= 2f * Time.deltaTime;
            BlackFade.color = new Color(0f, 0f, 0f, transparencyValue);
        }
        else if (!begin && transparencyValue < 1f)
        {
            transparencyValue += 1.6f * Time.deltaTime;
            BlackFade.color = new Color(0f, 0f, 0f, transparencyValue);
        }
    }
}
