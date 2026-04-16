using UnityEngine;
using UnityEngine.SceneManagement;

/*
  Changement de Scene Additive. 1 Seul player, qui desactive et active les scenes. Il n'y va pas jusqu'a la scene, la scene vient jusqu'a lui. 
*/


public class SceneLoader : MonoBehaviour
{
    private string currentSubScene = null;
    private string returnSpawnId = null;

    // charge la SubScene
    public void LoadSubSceneAtSpawn(string sceneName, string spawnId, string returnToMainSpawnId)
    {
        if (sceneName == "MSampleScene")
        {
            Debug.LogError("NE PAS CHARGER MainScene !");
            return;
        }

        returnSpawnId = returnToMainSpawnId;
        
        // si on a chargé une subScene, la main scene se desactive
        DisableMainScene();

        if (currentSubScene != null)
            SceneManager.UnloadSceneAsync(currentSubScene);

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += (op) =>
        {
            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(loadedScene);

            SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            foreach (var sp in spawnPoints)
            {
                if (sp.SpawnId == spawnId)
                {
                    player.transform.position = sp.transform.position;
                    break;
                }
            }
        };

        currentSubScene = sceneName;
    }

    // decharge la subScene au moment qu'on rentre dans le portal de retour
    public void UnloadCurrentSubScene()
    {
        if (currentSubScene != null)
        {
            SceneManager.UnloadSceneAsync(currentSubScene);
            currentSubScene = null;
        }
        
        // reactive la Main Scene
        EnableMainScene();

        // Téléportation dans MainScene à l'endroit choisi
        if (returnSpawnId != null)
        {
            SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            foreach (var sp in spawnPoints)
            {
                if (sp.SpawnId == returnSpawnId)
                {
                    player.transform.position = sp.transform.position;
                    break;
                }
            }
        }

        returnSpawnId = null;
    }

    // desactive la Main Scene
    private void DisableMainScene()
    {
        Scene main = SceneManager.GetSceneByName("MSampleScene");

        foreach (GameObject obj in main.GetRootGameObjects())
        {
            obj.SetActive(false);
        }
    }

    // reactive la Main Scene
    private void EnableMainScene()
    {
        Scene main = SceneManager.GetSceneByName("MSampleScene");

        foreach (GameObject obj in main.GetRootGameObjects())
        {
            obj.SetActive(true);
        }
    }
}
