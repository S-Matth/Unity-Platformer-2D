using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    public float maxTimeInWater = 30f;

    private float timer = 0f;
    private bool isInWater = false;

    private PlayerRespawn respawn;
    private CPlayerLife playerLife;

    private void Update()
    {
        if (!isInWater) return;

        timer += Time.deltaTime;

        Debug.Log("Timer eau: " + timer);

        if (timer >= maxTimeInWater)
        {
            Debug.Log("Mort par noyade");

            if (playerLife.currentLives == 0)
                respawn.MainRespawn();
            else
                respawn.Respawn();

            playerLife.Damage();

            // reset pour éviter boucle infinie
            timer = 0f;
            isInWater = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Entré dans l'eau");

        isInWater = true;
        timer = 0f;

        respawn = collision.GetComponentInParent<PlayerRespawn>();
        playerLife = collision.GetComponentInParent<CPlayerLife>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Sorti de l'eau");

        isInWater = false;
        timer = 0f;
    }
}