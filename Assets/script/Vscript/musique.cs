using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Détruit le doublon
            return;
        }

        Instance = this;
    }
}