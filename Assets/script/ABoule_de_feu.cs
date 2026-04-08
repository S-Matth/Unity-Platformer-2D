using UnityEngine;


public class Boule_de_feu : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public float lifetime = 5f;

    private Transform player;

    void Start()
    {
        // Trouver le joueur automatiquement
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Détruire après un certain temps
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (player == null) return;

        // Direction vers le joueur
        Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
        direction.Normalize();

        // Rotation vers le joueur
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        // Avancer
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit !");
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
