/*using System.Collections;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.Tilemaps;*/
using UnityEngine;

public class MonsterF : MonoBehaviour
{
    // Mouvement
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public float speed;
    public float jumpForce;

    // Attaque
    public GameObject fireBallPreFab;
    public Transform firePoint;

    private float timer;
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject Player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentPoint = pointB.transform;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        // verifie a chaque frame si le Player est dans sa zone
        float distancePlayer = Vector2.Distance(transform.position, Player.transform.position);
        Move();
        if (distancePlayer < 2f) // s'il le Player est dans sa zone il va s'arreter, continue a sauter et attack (declenche le timer)
        {
            stopMoving();

            lookPos();

            attack();
        }
        else // si non il bouge de gauche a droite et le timer se met a 0
        {
            Move();

            timer = 0;
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.Play("sol", -1, 0f);
         
            Jump();
        }
    }
    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // on modifie pas le x que le y

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // multiplie le saut selon le jumpForce
    }

    void Shoot()
    {
        Instantiate(fireBallPreFab, firePoint.position, Quaternion.identity);
    }

    void attack()  // attaque le player avec un timer
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            timer = 0;
            Shoot();
        }
    }
    private void Move()
    {
        // move de gauche a droite selon le currentPoint
        float direction = (currentPoint.position.x > transform.position.x) ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y); // on modifie que le x => direction (gauche ou droite) * speed

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f) // que la distance jusque au currentPoint est inferieur a 0.5f
        {
            Flip();
            currentPoint = (currentPoint == pointB.transform) ? pointA.transform : pointB.transform; // si il a atteint son currentPoint on lui donne l'autre coté comme currentPoint
        }
    }
    private void stopMoving()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // on block le x a 0
    }

    // position selon le player
    private void lookPos()
    {
        // si le player est a gauche 
        if (Player.transform.position.x < transform.position.x )
        {
            // on flip et on change son currentPoint
            transform.localScale = new Vector3(-1, 1, 1);
            currentPoint = pointA.transform; 
        }
        else
        {
            // on flip et on change son currentPoint
            transform.localScale = new Vector3(1, 1, 1);
            currentPoint = pointB.transform;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.2f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.2f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }*/
}
