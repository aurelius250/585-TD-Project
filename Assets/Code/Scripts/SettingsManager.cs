using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;  // For audio volume control
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public Dropdown difficultyDropdown;
    public Button applyButton;
    public Button backButton; // Reference for the Back button

    [Header("Audio")]
    public AudioMixer audioMixer; // Reference to the AudioMixer (for volume control)
    public AudioSource backgroundMusicSource; // Background music AudioSource
    public AudioSource buttonSfxSource; // Button sound effect AudioSource
    public AudioClip buttonClickSound; // Button click sound effect

    [Header("Settings")]
    private float currentVolume = 1.0f; // Default volume (1.0 = 100%)
    private bool isFullscreen = false;
    private int currentDifficulty = 0; // Default difficulty

    void Start()
    {
        // Load previously saved settings (PlayerPrefs)
        LoadSettings();

        // Initialize UI elements based on current settings
        volumeSlider.value = currentVolume;
        fullscreenToggle.isOn = isFullscreen;
        // difficultyDropdown.value = currentDifficulty;

        // Add listeners to the UI elements
        volumeSlider.onValueChanged.AddListener(SetVolume);
        fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);
        // difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
        applyButton.onClick.AddListener(() => HandleButtonClick(ApplySettings));
        backButton.onClick.AddListener(() => HandleButtonClick(GoBack));

        // Initialize Audio Sources
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.loop = true; // Loop the background music
            backgroundMusicSource.volume = currentVolume; // Set initial volume
            backgroundMusicSource.Play(); // Start playing background music
        }

        if (buttonSfxSource != null)
        {
            buttonSfxSource.clip = buttonClickSound; // Assign the button click sound
            buttonSfxSource.playOnAwake = false; // Prevent sound from playing on awake
        }
    }

    void SetVolume(float volume)
    {
        currentVolume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Apply logarithmic scaling for volume
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = volume; // Update background music volume
        }
    }

    void ToggleFullscreen(bool isOn)
    {
        isFullscreen = isOn;
        Screen.fullScreen = isFullscreen;
    }

    void SetDifficulty(int difficulty)
    {
        currentDifficulty = difficulty;
    }

    void ApplySettings()
    {
        // Save settings using PlayerPrefs
        PlayerPrefs.SetFloat("Volume", currentVolume);
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.SetInt("Difficulty", currentDifficulty);

        PlayerPrefs.Save();

        Debug.Log("Settings Applied!");
    }

    void GoBack()
    {
        // Navigate to the previous scene (e.g., Main Menu)
        Debug.Log("Back Button Clicked");
        SceneManager.LoadScene("MainMenuScene");
    }

    void LoadSettings()
    {
        // Load previously saved settings from PlayerPrefs
        if (PlayerPrefs.HasKey("Volume"))
            currentVolume = PlayerPrefs.GetFloat("Volume");

        if (PlayerPrefs.HasKey("Fullscreen"))
            isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;

        if (PlayerPrefs.HasKey("Difficulty"))
            currentDifficulty = PlayerPrefs.GetInt("Difficulty");
    }

    void HandleButtonClick(System.Action action)
    {
        if (buttonSfxSource != null && buttonClickSound != null)
        {
            buttonSfxSource.Play(); // Play the button click sound
        }

        // Wait a short duration before executing the action
        StartCoroutine(WaitAndExecute(action));
    }

    System.Collections.IEnumerator WaitAndExecute(System.Action action)
    {
        yield return new WaitForSeconds(0.5f); // Adjust delay if needed
        action.Invoke();
    }
}
