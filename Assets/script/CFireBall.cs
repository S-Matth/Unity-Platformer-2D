using UnityEngine;

public class FireBall : MonoBehaviour
{

    private GameObject Player;

    public float speed = 10f;
    private Rigidbody2D rb;

    private bool directionFixe = false;
    private float InitFireBallSpeed = 2f;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        Player = GameObject.FindGameObjectWithTag("Player");

        if ( Player != null)
        {
            Vector2 direction = Player.transform.position - transform.position;// determine la direction de la FB selon la position du Player
            rb.linearVelocity = direction * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // au cas ou cela touche un objet ou joueur
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
       {
            Destroy(gameObject);
       }
    }
}   

