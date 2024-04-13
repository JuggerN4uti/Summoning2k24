using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float horizontal;
    public float speed, jumpStrength;
    public bool facingRight;

    [Header("Summon")]
    public Vector3 mousePos;
    public Transform CursosPosition;

    [Header("Elementals")]
    public int Summon;
    public int[] SummonsAviable;
    bool destroyCooldown;
    public GameObject[] ElementalPrefabs;
    public GameObject Destroyer;

    [Header("SerializeFields")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetMouseButtonDown(0))
            SummonElemental();
        if (Input.GetMouseButtonDown(1))
            DestroyElemental();

        if (Input.GetKeyUp(KeyCode.Alpha1))
            Summon = 0;
        if (Input.GetKeyUp(KeyCode.Alpha2))
            Summon = 1;

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void Flip()
    {
        if (facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Death")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    // Summoning
    void SummonElemental()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursosPosition.position = new Vector2(mousePos[0], mousePos[1]);

        if (SummonsAviable[Summon] > 0)
        {
            Instantiate(ElementalPrefabs[Summon], CursosPosition.position, CursosPosition.rotation);
            SummonsAviable[Summon]--;
        }
    }

    void DestroyElemental()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursosPosition.position = new Vector2(mousePos[0], mousePos[1]);

        if (!destroyCooldown)
        {
            Instantiate(Destroyer, CursosPosition.position, CursosPosition.rotation);
            destroyCooldown = true;
            Invoke("OffCooldown", 0.5f);
        }
    }

    void OffCooldown()
    {
        destroyCooldown = false;
    }
}
