using UnityEngine;

public class DoubleCamera : MonoBehaviour
{
    private static DoubleCamera instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
