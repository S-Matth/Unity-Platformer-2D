using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // quand le Player rentre en contact avec le CheckPoint il est sauvegardé
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            collision.GetComponent<PlayerRespawn>().SetCheckPoint(transform.position);
        }
    }
}
