using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject hitBoxAttack;
    public float jumpForce = 10f;
    public int health = 7;
    private bool isActing;
    private bool isGrounded = true;
    public List<Sprite> weaponSprites;
    public SpriteRenderer weaponRenderer;
    public AudioClip audioJump;
    public AudioClip audioDamage;
    public AudioClip audioSlash;
    public AudioClip audioDodge;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        int equippedId = SaveManager.GetEquippedWeapon();
        weaponRenderer.sprite = weaponSprites[equippedId];
        health += SaveManager.GetExtraLives(); // 1 vida base + extras
        Debug.Log($"Total de vidas: {health}");
    }

    void Start()
    {
        hitBoxAttack = GameObject.FindGameObjectWithTag("hitbox");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
        {
            anim.Play("idle");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Jump();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            StartCoroutine(Attack());
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            StartCoroutine(Dodge());
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (ButtonsController.instance.canvaMenu)
            {
                ButtonsController.instance.CloseMenu();
                Time.timeScale = 1f;
            }
            else
            {
                ButtonsController.instance.OpenMenu();
                Time.timeScale = 0f;
            }
        }
    }

    public void OnAttackBtn()
    {
        StartCoroutine(Attack());
    }
    public void OnDodgeBtn()
    {
        StartCoroutine(Dodge());
    }

    public void Jump()
    {
        if (isGrounded && !isActing)
        {
            isGrounded = false;
            audioSource.clip = audioJump;
            audioSource.Play();
            anim.SetBool("jump", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            /*rb.AddForce(Vector2.up * jumpForce);*/
        }
    }

    public IEnumerator Attack()
    {
        if (isActing)
            yield break;
        isActing = true;
        audioSource.clip = audioSlash;
        audioSource.Play();
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(0.5f);
        isActing = false;
    }

    public IEnumerator Dodge()
    {
        if (isActing)
            yield break;
        isActing = true;
        audioSource.clip = audioDodge;
        audioSource.Play();
        anim.SetTrigger("dodge");
        yield return new WaitForSeconds(1.5f);
        isActing = false;
    }

    IEnumerator TakeDamage()
    {
        audioSource.clip = audioDamage;
        audioSource.Play();
        anim.SetTrigger("hurt");
        gameObject.tag = "PlayerHurt";
        health -= 1;

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }

        rb.gravityScale = 5;
        FlashRed(1f);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 3;
        gameObject.tag = "Player";
    }

    public void FlashRed(float duration)
    {
        StartCoroutine(FlashColorCoroutine(Color.red, duration));
    }

    private IEnumerator FlashColorCoroutine(Color targetColor, float duration)
    {
        var renderers = GetComponentsInChildren<SpriteRenderer>();

        // Salva as cores originais
        Color[] originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalColors[i] = renderers[i].color;

        // Aplica a cor alvo
        foreach (var r in renderers)
            r.color = targetColor;

        yield return new WaitForSeconds(duration);

        // Restaura
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].color = originalColors[i];
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            anim.SetBool("jump", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("obstacle") || collider.CompareTag("breakableObstacle"))
        {
            StartCoroutine(TakeDamage());
            Destroy(collider.gameObject);
        }
    }
}
