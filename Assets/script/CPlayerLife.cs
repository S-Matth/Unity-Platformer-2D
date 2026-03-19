using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.U2D;

public class CPlayerLife : MonoBehaviour
{
    
    public int maxLives = 5;
    public int currentLives;

    private GameObject Player;

    //private Vector3 CheckPointPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLives = maxLives;
    }
    // - un coeur a chaque fois que la fonction est appeléé
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
    }
}
