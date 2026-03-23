using UnityEngine;

/* 
    Creer le void avec un Box Collider 2D (is trigger actif), sans tag.
    Mettre le script dans le Void
*/

public class VoidKill : MonoBehaviour
{

    private GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        PlayerRespawn respawn = collision.GetComponentInParent<PlayerRespawn>(); // appeler 
        CPlayerLife playerLife = collision.GetComponentInParent<CPlayerLife>();

        // si Player tombe dans le void il perd un coeur et est teletransporté au derniere chekcpoint
        if (collision.CompareTag("Player"))
        {    
            
            if (playerLife.currentLives == 0)
            {
                respawn.MainRespawn();
            }
            else
                respawn.Respawn();

            playerLife.Damage();
        }
    }
}
