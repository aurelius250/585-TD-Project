using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button achievementsButton;
    public Button settingsButton;

    void Start()
    {
        // Adding listeners for button clicks
        startButton.onClick.AddListener(StartGame);
        achievementsButton.onClick.AddListener(ShowAchievements);
        settingsButton.onClick.AddListener(OpenSettings);
    }

    void StartGame()
    {
        // Load the Game scene (replace with your actual game scene name)
        SceneManager.LoadScene("LevelSelectScene");  // Use the name of your actual game scene
    }

    void ShowAchievements()
    {
        // Open achievements screen (you can replace this with actual achievement handling)
        Debug.Log("Achievements button clicked");
        // Example: Load the achievements scene
        // SceneManager.LoadScene("AchievementsScene");
    }

    void OpenSettings()
    {
        // Open the settings menu (replace this with actual settings logic)
        Debug.Log("Settings button clicked");
        // Example: Load the settings scene
        // SceneManager.LoadScene("SettingsScene");
    }
}
