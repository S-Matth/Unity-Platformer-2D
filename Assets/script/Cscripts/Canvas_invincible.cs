using UnityEngine;

public class Canvas_invincible : MonoBehaviour
{
    private static Canvas_invincible instance;
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
