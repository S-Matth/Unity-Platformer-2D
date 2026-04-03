using UnityEngine;

public class CmainCheckPoint : MonoBehaviour
{
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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerRespawn>().SetCheckPoint(transform.position);
            collision.GetComponent<PlayerRespawn>().MainRespawn(transform.position);
        }
    }

}
