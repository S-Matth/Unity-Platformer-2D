using UnityEngine;

/*
   script que pour les mainCheckPoints de Matthias
*/

public class CmainCheckMatthias : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("checkpointX", transform.position.x);
            PlayerPrefs.SetFloat("checkpointY", transform.position.y);
            PlayerPrefs.SetInt("hasCheckpoint", 1);
        }
    }
}
