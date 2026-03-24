using UnityEngine;

public class MCanSwim : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CPlayerLife life = collision.GetComponent<CPlayerLife>();

            // le joueur se noie s'il NE PEUT PAS nager
            if (!life.canSwim) life.Die();
        }
    }
}
