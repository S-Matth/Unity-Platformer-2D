using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Vous devez creer un objet vide (ou ajouter un autre objet) dans la hierarchie, ajouté un boxcollider2D et ce script.
    Dans les parametres du script vous allez voir l'index, vous devez placer le numero de la scene a laquelle vous voulez passer
    pour voir le numero de la scenes, vous devez ouvrir en haut a droit File -> Build Profiles. Suivez l'ordre. Si Vous etes numero 1 choisissez numero 2
*/
public class CchangeScene : MonoBehaviour
{
    public bool ChangeScene;
    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChangeScene = true;
            if (ChangeScene)
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}
