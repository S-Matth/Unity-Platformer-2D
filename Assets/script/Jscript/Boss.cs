using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public int health = 3;
    //public Transform player;
    public float speed = 2f;
    public float jumpForce = 6f;
    public float aggroRange = 5f;// Distance de détection
    private GameObject Player;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isPatternRunning = false;
    private bool canTakeDamage = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (Player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);

        //  Le boss ne réagit que si le joueur est proche
        if (distanceToPlayer <= aggroRange)
        {
            Flip();

            if (!isPatternRunning)
            {
                StartCoroutine(SimplePattern());
            }
        }

        UpdateAnimations();
    }

    void Flip()
    {
        if (Player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void JumpToPlayer()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, jumpForce);
    }

    IEnumerator SimplePattern()
    {
        isPatternRunning = true;

        for (int i = 0; i < 3; i++)
        {
            JumpToPlayer();
            yield return new WaitForSeconds(1.0f);
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

    //  Visualiser la zone d’aggro dans Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

}