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
    public bool facingRight, grounded;

    [Header("Summon")]
    public Vector3 mousePos;
    public Transform CursosPosition;
    public GameObject[] SelectedElemental;
    public TMPro.TextMeshProUGUI[] ElementalCount;

    [Header("Elementals")]
    public int Summon;
    public int[] SummonsAviable;
    bool destroyCooldown;
    public GameObject[] ElementalPrefabs;
    public GameObject Destroyer;

    [Header("SerializeFields")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck, groundCheck2;
    [SerializeField] private LayerMask groundLayer;

    [Header("Audio")]
    [SerializeField] private AudioSource SFXSource;
    public AudioClip[] ElementalSound;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetMouseButtonDown(0))
            SummonElemental();
        if (Input.GetMouseButtonDown(1))
            DestroyElemental();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectElemental(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectElemental(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectElemental(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectElemental(3);
        if (Input.GetKeyDown(KeyCode.R))
            Die();
        if (Input.GetKeyDown(KeyCode.Escape))
            Return();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        for (int i = 0; i < 4; i++)
        {
            ElementalCount[i].text = SummonsAviable[i].ToString("0");
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
        grounded = false;
        if (Physics2D.OverlapCircle(groundCheck.position, 0.16f, groundLayer))
            grounded = true;
        if (Physics2D.OverlapCircle(groundCheck2.position, 0.16f, groundLayer))
            grounded = true;
        return grounded;
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

    void Return()
    {
        Fading.FadeOut();
        Invoke("Exit", 0.5f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    void Exit()
    {
        SceneManager.LoadScene(0);
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

    void SelectElemental(int which)
    {
        SelectedElemental[Summon].SetActive(false);
        Summon = which;
        SelectedElemental[Summon].SetActive(true);
    }

    // Summoning
    void SummonElemental()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursosPosition.position = new Vector2(mousePos[0], mousePos[1]);

        if (SummonsAviable[Summon] > 0 && Vector3.Distance(transform.position, new Vector2(mousePos[0], mousePos[1])) <= 2.6f)
        {
            Instantiate(ElementalPrefabs[Summon], CursosPosition.position, CursosPosition.rotation);
            SFXSource.clip = ElementalSound[Summon];
            SFXSource.Play();
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
