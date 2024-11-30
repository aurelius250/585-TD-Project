using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button achievementsButton;
    public Button settingsButton;
    public Button exitButton;  // Reference for Exit button

    void Start()
    {
        // Adding listeners for button clicks
        startButton.onClick.AddListener(StartGame);
        achievementsButton.onClick.AddListener(ShowAchievements);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);  // Listener for Exit button
    }

    void StartGame()
    {
        // Here we load the scene for starting the game (LevelSelect or Game scene)
        Debug.Log("Start Game button clicked");
        SceneManager.LoadScene("LevelSelectScene");  // Change this to the actual name of your scene
    }

    void ShowAchievements()
    {
        // Here you can implement logic to open an achievements menu, or load a scene for achievements
        Debug.Log("Achievements button clicked");

        // Example: Load the achievements scene
        SceneManager.LoadScene("AchievementsScene");  // Change this to the actual name of your achievements scene
    }


    void OpenSettings()
    {
        // This method can open a settings menu, or load a scene with settings options
        Debug.Log("Settings button clicked");

        // Example: Load the settings scene
        SceneManager.LoadScene("SettingsScene");  // Change this to the actual name of your settings scene
    }

    void ExitGame()
    {
        // Exit the game or stop the editor play mode
        Debug.Log("Exit button clicked");
        Application.Quit();

        // If running in the Unity Editor, stop play mode
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
