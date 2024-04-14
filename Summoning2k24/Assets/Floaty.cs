using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floaty : MonoBehaviour
{
    public Vector2 BoxSize;
    public bool pushed;

    [Header("SerializeFields")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform topCheck;
    [SerializeField] private LayerMask heavyLayer;

    void Update()
    {
        if (pushed)
            rb.velocity = new Vector2(rb.velocity.x, 2.5f);
        else if (IsPressed())
            rb.velocity = new Vector2(rb.velocity.x, -1.5f);
        else rb.velocity = new Vector2(rb.velocity.x, 0f);
    }

    bool IsPressed()
    {
        return Physics2D.OverlapCircle(topCheck.position, 1f, heavyLayer);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Wind")
            pushed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "Wind")
            pushed = false;
    }
}
