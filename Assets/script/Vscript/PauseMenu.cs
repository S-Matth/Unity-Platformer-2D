using UnityEngine;
using UnityEngine.UI; // AJOUT

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    [SerializeField] private Slider volumeSlider; // AJOUT

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // AJOUT : initialise le slider au volume actuel
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
                resume();
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // AJOUT : change le volume global du jeu
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}