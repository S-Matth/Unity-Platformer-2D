using UnityEngine;

public class WaterZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(" Collision eau avec : " + collision.name);

        if (!collision.CompareTag("Player"))
        {
            Debug.Log(" Pas le player");
            return;
        }

        PlayerMaskTest player = collision.GetComponentInParent<PlayerMaskTest>();

        if (player == null)
        {
            Debug.Log(" Script PlayerMaskTest introuvable");
            return;
        }

        if (player.hasMaskWater)
        {
            Debug.Log(" MASQUE DETECTÉ = NAGE");

            Rigidbody2D rb = collision.GetComponentInParent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0.5f;
                Debug.Log(" Gravité réduite");
            }
            else
            {
                Debug.Log(" Rigidbody introuvable");
            }
        }
        else
        {
            Debug.Log(" PAS DE MASQUE = VOID");
        }
    }
}