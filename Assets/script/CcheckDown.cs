using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private GameObject Monster;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster = GameObject.FindGameObjectWithTag("Monster");

        if (collision.CompareTag("Monster"))
        {
            Destroy(Monster);
        }
    }
}
