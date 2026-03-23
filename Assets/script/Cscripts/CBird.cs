using UnityEngine;

public class CBird : MonoBehaviour
{
    public GameObject pointA1;
    public GameObject pointB1;
    private Transform currentPoint;
    public float speed;

    public Transform FirePoint;
    public GameObject EggPrep;

    private Rigidbody2D rb;
    private GameObject Player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPoint = pointB1.transform;
        rb = GetComponent<Rigidbody2D>();

        Player = GameObject.FindGameObjectWithTag("Player");
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // move de gauche a droite selon le currentPoint
        float direction = (currentPoint.position.x > transform.position.x) ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y); // on modifie que le x => direction (gauche ou droite) * speed

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f) // que la distance jusque au currentPoint est inferieur a 0.5f
        {
            Flip();
            currentPoint = (currentPoint == pointB1.transform) ? pointA1.transform : pointB1.transform; // si il a atteint son currentPoint on lui donne l'autre coté comme currentPoint
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(
        -transform.localScale.x,
        transform.localScale.y,
        transform.localScale.z
    );
    }

    public void shoot()
    {
        Instantiate(EggPrep, FirePoint.position, Quaternion.identity);
    }

}
