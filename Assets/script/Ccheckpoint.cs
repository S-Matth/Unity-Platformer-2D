using UnityEngine;

/*
 Ajouter ce script dans tout les CheckPoints   
 pour les CheckPoints utiliser le tag "Respawn" avec un collider (is trigger actif)
 pour le checkPoint pricipal, celui que le player commence, utiliser le tag "MainRespawn" (is trigger actif)
 placer le player au dessus du MainRespawn, au momment qu'il tombe il enregistre comme le mainRespawn
*/


public class checkpoint : MonoBehaviour
{

    // quand le Player rentre en contact avec le CheckPoint il est sauvegardé
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerRespawn>().SetCheckPoint(transform.position);
        }
    }
}
