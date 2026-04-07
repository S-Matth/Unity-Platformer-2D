using UnityEngine;
using UnityEngine.SceneManagement;

/*
    ne touchez pas au script ni au changement de scene, placé le prefab à l'endroit qui voulais et c'est tout
*/
public class CchangeScene : MonoBehaviour
{

    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
