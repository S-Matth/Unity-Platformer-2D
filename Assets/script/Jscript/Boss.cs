using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public int health = 3;               // Vie du boss
    public Transform player;             // Joueur à suivre
    public float speed = 2f;             // Vitesse horizontale réduite
    public float jumpForce = 6f;         // Force du saut un peu plus douce

    private Rigidbody2D rb;
    private Animator anim;
    private bool isPatternRunning = false;
    private bool canTakeDamage = true;   // Invincibilité temporaire

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        Flip(); // Tourne vers le joueur

        if (!isPatternRunning)
        {
            StartCoroutine(SimplePattern());
        }

        UpdateAnimations();
    }

    void Flip()
    {
        if (player.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void JumpToPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, jumpForce);
    }

    IEnumerator SimplePattern()
    {
        isPatternRunning = true;

        for (int i = 0; i < 3; i++)
        {
            JumpToPlayer();
            yield return new WaitForSeconds(1.0f); // délai entre sauts
        }

        isPatternRunning = false;
    }

    void UpdateAnimations()
    {
        anim.SetBool("isMoving", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        anim.SetBool("isJumping", rb.linearVelocity.y > 0.1f);
    }

    public void TakeDamage()
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;
        health--;
        anim.SetTrigger("Hit");

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(ResetDamage());
        }
    }

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    void Die()
    {
        anim.SetBool("Dead", true);
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject, 1.5f);
    }
}