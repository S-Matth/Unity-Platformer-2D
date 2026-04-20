using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterDeath : MonoBehaviour
{
    public float maxTimeInWater = 30f;
    public float speedThreshold = 0.1f;

    public Tilemap waterTilemap;

    private float timer = 0f;

    private Rigidbody2D playerRb;
    private PlayerRespawn respawn;
    private CPlayerLife playerLife;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        respawn = GetComponent<PlayerRespawn>();
        playerLife = GetComponent<CPlayerLife>();
    }

    private void Update()
    {
        bool isInWater = IsPlayerOnWater();

        if (!isInWater)
        {
            timer = 0f;
            return;
        }

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
            Debug.Log("Mort par noyade");

            if (playerLife.currentLives == 0)
                respawn.MainRespawn();
            else
                respawn.Respawn();

            playerLife.Damage();

            timer = 0f;
        }
    }

    private bool IsPlayerOnWater()
    {
        Vector3Int cellPosition = waterTilemap.WorldToCell(transform.position);
        return waterTilemap.HasTile(cellPosition);
    }
}