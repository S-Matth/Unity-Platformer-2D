using UnityEngine;

public class ReturnToMain : MonoBehaviour
{
    public void Back()
    {
        FindObjectOfType<SceneLoader>().UnloadCurrentSubScene();
    }
}

