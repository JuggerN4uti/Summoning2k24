using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elemental : MonoBehaviour
{
    public PlayerController Player;
    public int elementalID;
    public float duration;
    float maxDuration;
    public Image DurationImage;

    void Start()
    {
        maxDuration = duration;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent(typeof(PlayerController)) as PlayerController;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        DurationImage.fillAmount = duration / maxDuration;
        if (duration <= 0)
            Perish();
    }

    public void Perish()
    {
        Player.SummonsAviable[elementalID]++;
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Death")
            Perish();
        if (other.transform.tag == "Perish")
            Perish();
    }
}
