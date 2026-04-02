using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    //  Vie du boss
    public int health = 3;

    //  Joueur
    public Transform player;

    //  Vitesse horizontale
    public float speed = 2f;

    //  Force de saut
    public float jumpForce = 8f;

    //  Composants
    private Rigidbody2D rb;
    private Animator anim;

    //  Pattern
    private bool isPatternRunning = false;

    //  Invincibilité après Hit
    private bool canTakeDamage = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // Flip pour regarder le joueur
        Flip();

        // Lancer le pattern si pas en cours
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
        rb.linearVelocity = new Vector2(direction.x * 5f, jumpForce);
    }

    IEnumerator SimplePattern()
    {
        isPatternRunning = true;

        for (int i = 0; i < 3; i++)
        {
            JumpToPlayer();
            yield return new WaitForSeconds(1f);
        }

        isPatternRunning = false;
    }

    void UpdateAnimations()
    {
        anim.SetBool("isMoving", Mathf.Abs(rb.linearVelocity.x) > 0.1f);
        anim.SetBool("isJumping", rb.linearVelocity.y > 0.1f);
    }

    // 💥 Prendre des dégâts
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
        yield return new WaitForSeconds(0.5f); // temps d'invincibilité
        canTakeDamage = true;
    }

    void Die()
    {
        anim.SetBool("Dead", true);
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject, 1.5f);
    }
}