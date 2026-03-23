using UnityEngine;

public class CEggCheck : MonoBehaviour
{
    private CBird bird;

    void Start()
    {
        // récupère le CBird sur ce GameObject ou un parent
        bird = GetComponentInParent<CBird>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        // si le joueur entre dans le trigger
        if (collision.CompareTag("Player"))
        {
            bird.shoot();
        }
    }
}
