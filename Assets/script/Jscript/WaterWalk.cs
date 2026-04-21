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

        PlayerMask mask = other.GetComponent<PlayerMask>();
        Timer waterDeath = other.GetComponent<Timer>();
        Collider2D playerCollider = other.GetComponent<Collider2D>();

        if (mask == null || waterDeath == null || playerCollider == null) return;

        // CAS 1 : pas de masque => tombe dans l'eau
        if (!mask.hasWaterMask)
        {
            Physics2D.IgnoreCollision(playerCollider, waterCollider, true);
            waterDeath.SetInWater(true);
            return;
        }

        // CAS 2 : masque + E => traverse, donc dans l'eau
        if (mask.isPressingE)
        {
            Physics2D.IgnoreCollision(playerCollider, waterCollider, true);
            waterDeath.SetInWater(true);
        }
        else
        {
            // CAS 3 : masque sans E => marche SUR l'eau, donc pas "dans" l'eau
            Physics2D.IgnoreCollision(playerCollider, waterCollider, false);
            waterDeath.SetInWater(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Collider2D playerCollider = other.GetComponent<Collider2D>();
        Timer waterDeath = other.GetComponent<Timer>();

        if (playerCollider != null)
            Physics2D.IgnoreCollision(playerCollider, waterCollider, false);

        if (waterDeath != null)
            waterDeath.SetInWater(false);
    }
}