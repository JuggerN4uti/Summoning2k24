using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    public Fade Fading;

    [Header("Movement")]
    public Transform PlayerSprite;
    public float horizontal, speed, jumpStrength;
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Summon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Summon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Summon = 2;
        if (Input.GetKeyDown(KeyCode.R))
            Die();

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
            Vector3 localScale = PlayerSprite.localScale;
            localScale.x *= -1f;
            PlayerSprite.localScale = localScale;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.25f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Death" || other.transform.tag == "Barrier")
            Die();
        if (other.transform.tag == "Doors")
            Enter();
    }

    void Die()
    {
        Fading.FadeOut();
        Invoke("Restart", 0.5f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    void Enter()
    {
        if (PlayerPrefs.GetInt("Unlocked") < SceneManager.GetActiveScene().buildIndex + 1)
            PlayerPrefs.SetInt("Unlocked", SceneManager.GetActiveScene().buildIndex + 1);
        Fading.FadeOut();
        Invoke("Proceed", 0.6f);
    }

    void Proceed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Summoning
    void SummonElemental()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursosPosition.position = new Vector2(mousePos[0], mousePos[1]);

        if (SummonsAviable[Summon] > 0 && Vector3.Distance(transform.position, new Vector2(mousePos[0], mousePos[1])) <= 2.6f)
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
