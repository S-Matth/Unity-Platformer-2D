using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string levelToLoad;

    public GameObject CreditWindow;

    public GameObject settingWindow;
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void SettingsGame()
    {
        settingWindow.SetActive (true);
    }
    public void CloseSettingWindow()
        {
        settingWindow.SetActive (false); 
        }
    public void CreditsGame()
    {
        CreditWindow.SetActive (true);
    }
    public void closeCreditWindow()
    {
        CreditWindow.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();//dans le build

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//dans l editeur
        #endif
    }
}
