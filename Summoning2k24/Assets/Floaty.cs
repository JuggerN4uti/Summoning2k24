using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaty : MonoBehaviour
{
    [Header("SerializeFields")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform topCheck;
    [SerializeField] private LayerMask heavyLayer;

    void Update()
    {
        if (IsPressed())
        {
            rb.velocity = new Vector2(rb.velocity.x, -1.5f);
        }
        else rb.velocity = new Vector2(rb.velocity.x, 0f);
}

    bool IsPressed()
    {
        return Physics2D.OverlapCircle(topCheck.position, 0.35f, heavyLayer);
    }
}
