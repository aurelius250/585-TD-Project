using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button achievementsButton;
    public Button settingsButton;
    public Button exitButton;

    public AudioClip buttonClickSound; // Assign the sound clip for button clicks in the Inspector
    private AudioSource buttonAudioSource; // Separate AudioSource for button sounds
    private AudioSource backgroundAudioSource; // Reference to the background music AudioSource

    public float buttonSoundVolume = 1.0f; // Volume for button click sound (default to max)
    public float backgroundVolume = 0.3f; // Volume for background music (lowered by default)

    void Start()
    {
        // Find or assign the background music AudioSource
        backgroundAudioSource = FindObjectOfType<AudioSource>(); // Assumes a background music AudioSource exists in the scene
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.loop = true; // Ensure the background music loops
            backgroundAudioSource.volume = backgroundVolume; // Set background music volume
        }

        // Create a separate AudioSource for button sounds
        buttonAudioSource = gameObject.AddComponent<AudioSource>();
        buttonAudioSource.playOnAwake = false; // Prevent sound from playing on awake
        buttonAudioSource.clip = buttonClickSound;
        buttonAudioSource.volume = buttonSoundVolume; // Set button sound volume

        // Adding listeners for button clicks
        startButton.onClick.AddListener(() => HandleButtonClick(StartGame));
        achievementsButton.onClick.AddListener(() => HandleButtonClick(ShowAchievements));
        settingsButton.onClick.AddListener(() => HandleButtonClick(OpenSettings));
        exitButton.onClick.AddListener(() => HandleButtonClick(ExitGame));
    }

    void HandleButtonClick(System.Action action)
    {
        if (buttonAudioSource != null && buttonClickSound != null)
        {
            buttonAudioSource.Play();
            StartCoroutine(WaitForSoundAndExecute(action));
        }
        else
        {
            action.Invoke();
        }
    }

    private System.Collections.IEnumerator WaitForSoundAndExecute(System.Action action)
    {
        yield return new WaitForSeconds(buttonAudioSource.clip.length / 2);
        action.Invoke();
    }

    void StartGame()
    {
        Debug.Log("Start Game button clicked");
        SceneManager.LoadScene("LevelSelectScene"); // Change this to the actual name of your scene
    }

    void ShowAchievements()
    {
        Debug.Log("Achievements button clicked");
        SceneManager.LoadScene("AchievementsScene"); // Change this to the actual name of your achievements scene
    }

    void OpenSettings()
    {
        Debug.Log("Settings button clicked");
        SceneManager.LoadScene("SettingsScene"); // Change this to the actual name of your settings scene
    }

    void ExitGame()
    {
        Debug.Log("Exit button clicked");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
