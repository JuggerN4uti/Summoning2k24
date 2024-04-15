using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public Elemental elemental;
    [SerializeField] private LayerMask fireLayer;

    void Update()
    {
        if (IsHeated())
            elemental.Perish();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Snow")
            elemental.duration += 4f;
    }

    bool IsHeated()
    {
        return Physics2D.OverlapCircle(transform.position, 1.9f, fireLayer);
    }
}
