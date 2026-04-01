using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private GameObject Monster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // si tag touche le monstrer il mort
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(collision.gameObject);
        }
    }
}
