using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private GameObject Monster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster = GameObject.FindGameObjectWithTag("Monster");

        // si tag touche le monstrer il mort
        if (collision.CompareTag("Monster"))
        {
            Destroy(Monster);
        }
    }
}
