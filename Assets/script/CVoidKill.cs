using UnityEngine;

public class VoidKill : MonoBehaviour
{
    private GameObject Player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if (collision.CompareTag("checkDown"))
        {
            Destroy(Player);
        }
    }

}
