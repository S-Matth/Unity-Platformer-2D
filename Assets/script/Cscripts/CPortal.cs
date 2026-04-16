using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnId;
    [SerializeField] private string returnToMainSpawnId;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoader loader = FindObjectOfType<SceneLoader>();
            if (loader != null)
            {
                loader.LoadSubSceneAtSpawn(
                    targetScene,
                    targetSpawnId,
                    returnToMainSpawnId
                );
            }
            else
            {
                Debug.LogError("SceneLoader introuvable !");
            }
        }
    }
}
