using UnityEngine;

public class VoidKill : MonoBehaviour
{

    private GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerRespawn respawn = collision.GetComponentInParent<PlayerRespawn>(); // appeler 
        CPlayerLife playerLife = collision.GetComponentInParent<CPlayerLife>();
        
        // si Player tombe dans le void il perd un coeur et est teletransporté au derniere chekcpoint
        if (collision.CompareTag("Check"))
        {    
            playerLife.Damage(); 
            respawn.Respawn();
        }

        

    }
}
