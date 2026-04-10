using UnityEngine;

public class PortalReturn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // appelle la fonction qui desactive la subScene actuelle et qui envoie le player a l'endroit d'apparition dans la Main Scene
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SceneLoader>().UnloadCurrentSubScene();
        }
    }
}
