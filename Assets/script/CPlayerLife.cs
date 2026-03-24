using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.U2D;

/* 
 Ajouter ce script dans Player, il va controler les degats
*/

public class CPlayerLife : MonoBehaviour
{
    // ajouté par matthias, pour savoir si le player peut nager ou pas, et ainsi faire en sorte que s'il tombe dans l'eau, il meurt ou pas
    [SerializeField] public bool canSwim;

    public int maxLives = 5;
    public int currentLives;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLives = maxLives;
    }
    // - un coeur a chaque fois que la fonction est appelée
    public void Damage()
    {
        currentLives--;
        Debug.Log("-1");

        // s'il n'a plus de vie, il "meurt"
        if (currentLives == 0)
        {
            Die();
        }
    }

    // quand il est dead, il va teleporter au debut et on retablie ses coeurs
    public void Die()
    {
        Debug.Log("Game Over");
        PlayerRespawn respawn = GetComponentInParent<PlayerRespawn>(); // appele la fonction d'un autre script

        //Destroy(GameObject.FindGameObjectWithTag("Player"));

        respawn.MainRespawn();
        currentLives = maxLives;
        //Debug.Log("5 lives");
    }
}
