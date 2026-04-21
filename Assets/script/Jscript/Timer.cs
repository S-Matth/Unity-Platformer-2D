using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterDeath : MonoBehaviour
{
    public float maxTimeInWater = 30f;
    public float speedThreshold = 0.1f; 
    public float noMaskDeathDelay = 0.1f; // Délai avant la mort pour les joueurs sans masque de nage

    public Tilemap waterTilemap;

    private float timer = 0f;
    private bool pendingDeath = false;

    private Rigidbody2D playerRb;
    private PlayerRespawn respawn;
    private CPlayerLife playerLife;
    private PlayerMask mask;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        respawn = GetComponent<PlayerRespawn>();
        playerLife = GetComponent<CPlayerLife>();
        mask = GetComponent<PlayerMask>();

        if (waterTilemap == null)
            waterTilemap = GameObject.Find("WaterTilemap").GetComponent<Tilemap>();
    }

    private void Update()
    {
        bool isInWater = IsPlayerOnWater();

        if (!isInWater)
        {
            timer = 0f;
            pendingDeath = false;
            return;
        }
        Debug.Log("etat du mask: " + mask);

        // Cas 1 où le joueur n'a pas de masque de l'eau : mort instantanée après un court délai
        if (!mask.hasWaterMask)
        {
            if(!pendingDeath)
            {
                pendingDeath = true;
                Invoke(nameof(InstantKill), noMaskDeathDelay);
            }
            return;
        }

        // Cas 2 où le joueur a un masque de l'eau -> système normal
        if (playerRb.linearVelocity.magnitude < speedThreshold)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer eau: " + timer);
        }
        else
        {
            timer = 0f;
        }

        if (timer >= maxTimeInWater)
        {
            KillPlayer();

            timer = 0f;
        }
    }

    private bool IsPlayerOnWater()
    {
        if(waterTilemap == null) 
            return false;
        
        Vector3Int cellPosition = waterTilemap.WorldToCell(transform.position);
        return waterTilemap.HasTile(cellPosition);
    }

    private void InstantKill()
    {
        if(pendingDeath)
            KillPlayer();
    }

    private void KillPlayer()
    {
        Debug.Log("Mort par noyade");

        if (playerLife.currentLives == 0)
            respawn.MainRespawn();
        else
            respawn.Respawn();

        playerLife.Damage();
    }
}