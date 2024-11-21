using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;  // For audio volume control
using UnityEngine.SceneManagement; // To load scenes (optional)

public class SettingsManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public Dropdown difficultyDropdown;
    public Button applyButton;
    public Button backButton;  // Reference for the Back button

    [Header("Settings")]
    public AudioMixer audioMixer;  // Reference to the AudioMixer (for volume control)
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
        applyButton.onClick.AddListener(ApplySettings);
        backButton.onClick.AddListener(GoBack); // Add listener for the Back button
    }

    void SetVolume(float volume)
    {
        currentVolume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);  // Apply logarithmic scaling for volume
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
        // Save settings using PlayerPrefs (optional, can be loaded next time)
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
        // Load previously saved settings from PlayerPrefs (if they exist)
        if (PlayerPrefs.HasKey("Volume"))
            currentVolume = PlayerPrefs.GetFloat("Volume");

        if (PlayerPrefs.HasKey("Fullscreen"))
            isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;

        if (PlayerPrefs.HasKey("Difficulty"))
            currentDifficulty = PlayerPrefs.GetInt("Difficulty");
    }
}
