using UnityEngine;

public class CEggDrop : MonoBehaviour
{
    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        else if (collision.CompareTag("Void"))
        {
            Destroy(gameObject);
        }
    }
}
