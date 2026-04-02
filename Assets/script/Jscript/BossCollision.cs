using UnityEngine;

public class BossCollision : MonoBehaviour
{
    private Boss boss;

    void Start()
    {
        boss = GetComponent<Boss>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifie que c'est bien le joueur
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        // Vérifie si le joueur est au-dessus du boss (attaque sur la tête)
        if (collision.transform.position.y > transform.position.y + 0.5f)
        {
            boss.TakeDamage();

            // Rebond du joueur
            if (playerRb != null)
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 10f);
        }
        else
        {
            // Le joueur touche sur le côté ou en dessous → il meurt
            Destroy(collision.gameObject);
            Debug.Log("Le joueur est mort !");
        }
    }
}