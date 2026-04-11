using UnityEngine;

public class WaterWalk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.GetComponentInParent<PlayerInventory>();

            if (inventory != null && inventory.hasMask)
            {
                // Le joueur a le masque = il peut marcher sur l'eau
                Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>(), true);
            }
            else
            {
                // Pas de masque = comportement normal 
                Debug.Log("Le joueur tombe dans l'eau !");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Réactive la collision quand il sort de l'eau
            Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>(), false);
        }
    }
}