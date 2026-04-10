using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnId;
    public string SpawnId => spawnId; // responsable de l'apparition du player dans la scene
}

