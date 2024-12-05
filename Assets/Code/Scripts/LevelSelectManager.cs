using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button[] levelButtons; // Array for the 8 level buttons
    public Button startButton; // Start button
    public Button backButton; // Back button

    [Header("Audio")]
    public AudioSource buttonSfxSource; // Button sound effect AudioSource
    public AudioClip buttonClickSound; // Button click sound effect

    private string selectedLevel = null; // Store the selected level name

    private void Start()
    {
        // Ensure all buttons are assigned
        if (levelButtons.Length == 0 || startButton == null || backButton == null)
        {
            Debug.LogError("Buttons not assigned in the Inspector!");
            return;
        }

        // Setup button SFX source if not already configured
        if (buttonSfxSource != null)
        {
            buttonSfxSource.clip = buttonClickSound;
            buttonSfxSource.playOnAwake = false;
        }

        // Add listeners to the level buttons
        foreach (Button button in levelButtons)
        {
            string levelName = button.name; // Use button name as the level name
            button.onClick.AddListener(() => HandleButtonClick(() => SelectLevel(button, levelName)));
        }

        // Add listener to the start button
        startButton.onClick.AddListener(() => HandleButtonClick(LoadSelectedLevel));

        // Add listener to the back button
        backButton.onClick.AddListener(() => HandleButtonClick(GoBack));
    }

    private void HandleButtonClick(System.Action action)
    {
        // Play button sound effect
        if (buttonSfxSource != null && buttonClickSound != null)
        {
            buttonSfxSource.Play();
        }

        // Wait a short duration before executing the action
        StartCoroutine(WaitAndExecute(action));
    }

    private System.Collections.IEnumerator WaitAndExecute(System.Action action)
    {
        yield return new WaitForSeconds(0.5f); // Small delay before action
        action.Invoke();
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