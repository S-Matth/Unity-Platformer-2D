using UnityEngine;

public class WaterWalk : MonoBehaviour
{
    private Collider2D waterCollider;

    private void Awake()
    {
        waterCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerInventory inv = other.GetComponent<PlayerInventory>();
        if (inv == null) return;

        Collider2D playerCollider = other.GetComponent<Collider2D>();

        //  CAS 1 : PAS de masque = on tombe
        if (!inv.hasMask)
        {
            Physics2D.IgnoreCollision(playerCollider, waterCollider, true);
            return;
        }

        //  CAS 2 : Masque + touche E =on traverse
        if (inv.isPressingE)
        {
            Physics2D.IgnoreCollision(playerCollider, waterCollider, true);
        }
        else
        {
            //  CAS 3 : Masque sans E = on marche sur l'eau
            Physics2D.IgnoreCollision(playerCollider, waterCollider, false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Collider2D playerCollider = other.GetComponent<Collider2D>();

        // Reset sécurité
        Physics2D.IgnoreCollision(playerCollider, waterCollider, false);
    }
}