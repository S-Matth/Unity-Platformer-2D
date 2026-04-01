using UnityEngine;

public class FireBall : MonoBehaviour
{

    private GameObject Player;

    public float speed = 10f;
    private Rigidbody2D rb;

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

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CPlayerLife playerLife = collision.GetComponentInParent<CPlayerLife>();

        // si la boule touche le player, la boule est detruite et il pert un coeur
        if (collision.CompareTag("Player"))
        {
            playerLife.Damage();

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground")) // si la boule touche le sol, elle esrt detruite
        {
            Destroy(gameObject);
        }
    }
}   

