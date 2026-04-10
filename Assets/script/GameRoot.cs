using UnityEngine;

public class GameRoot : MonoBehaviour
{
    void Awake()
    {   
        // ne detruit pas la Main Scene
        DontDestroyOnLoad(gameObject);
    }
}
