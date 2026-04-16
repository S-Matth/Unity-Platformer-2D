using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    public float maxTimeInWater = 30f;

    private float timer = 0f;
    private bool isPlayerInside = false;

    private PlayerRespawn respawn;
    private CPlayerLife playerLife;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Entré dans l'eau");

        isPlayerInside = true;
        timer = 0f;

        respawn = collision.GetComponentInParent<PlayerRespawn>();
        playerLife = collision.GetComponentInParent<CPlayerLife>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isPlayerInside) return;
        if (!collision.CompareTag("Player")) return;

        timer += Time.deltaTime;

        if (timer >= maxTimeInWater)
        {
            Debug.Log("Mort par noyade");

            if (playerLife.currentLives == 0)
            {
                respawn.MainRespawn();
            }
            else
            {
                respawn.Respawn();
            }

            playerLife.Damage();

            // reset pour éviter boucle
            timer = 0f;
            isPlayerInside = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Sorti de l'eau");

        isPlayerInside = false;
        timer = 0f;
    }
}