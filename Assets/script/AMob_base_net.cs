using UnityEngine;


public class Mob_base_net: MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
    private bool movingRight = true;

    [Header("Detection")]
    public float detectionRange = 5f;
    public Transform player;

    [Header("Attack")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireRate = 1.5f;
    private float nextFireTime;

    private Rigidbody2D rb;
    private bool playerDetected = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        rb.linearVelocity = new Vector2((movingRight ? 1 : -1) * speed, rb.linearVelocity.y);

        // Détection du vide (pour pas tomber)
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        if (!groundInfo.collider)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void AttackPlayer()
    {
        rb.linearVelocity = Vector2.zero;

        // Regarder le joueur
        if (player.position.x > transform.position.x && !movingRight)
            Flip();
        else if (player.position.x < transform.position.x && movingRight)
            Flip();

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
    }
}
