using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Ensure this namespace is included

public class LevelSelectManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button[] levelButtons; // Array for the 8 level buttons
    public Button startButton;    // Start button
    public Button backButton;     // Back button

    private string selectedLevel = null; // Store the selected level name

    private void Start()
    {
        // Ensure all buttons are assigned
        if (levelButtons.Length == 0 || startButton == null || backButton == null)
        {
            Debug.LogError("Buttons not assigned in the Inspector!");
            return;
        }

        // Add listeners to the level buttons
        foreach (Button button in levelButtons)
        {
            string levelName = button.name; // Use button name as the level name
            button.onClick.AddListener(() => SelectLevel(button, levelName));
        }

        // Add listener to the start button
        startButton.onClick.AddListener(LoadSelectedLevel);

        // Add listener to the back button
        backButton.onClick.AddListener(GoBack);
    }

    private void SelectLevel(Button selectedButton, string levelName)
    {
        // Reset all buttons' colors to default
        foreach (Button button in levelButtons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white; // Default color
            button.colors = colors;
        }

        // Highlight the selected button
        ColorBlock selectedColors = selectedButton.colors;
        selectedColors.normalColor = Color.green; // Highlight color
        selectedButton.colors = selectedColors;

        // Update the selected level
        selectedLevel = levelName;
        Debug.Log("Selected level: " + selectedLevel);
    }

    private void LoadSelectedLevel()
    {
        if (!string.IsNullOrEmpty(selectedLevel))
        {
            Debug.Log("Loading level: " + selectedLevel);
            SceneManager.LoadScene(selectedLevel);
        }
        else
        {
            Debug.Log("No level selected. Please select a level first.");
        }
    }

    private void GoBack()
    {
        Debug.Log("Returning to the main menu.");
        SceneManager.LoadScene("MainMenuScene"); // Replace with your main menu scene name
    }
}
