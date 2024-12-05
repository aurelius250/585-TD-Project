using System.Collections;
using TMPro;
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
    // Reset all buttons to a definitive state
    foreach (Button button in levelButtons)
    {
        // Create a completely new ColorBlock with explicit colors
        ColorBlock resetColors = new ColorBlock
        {
            normalColor = Color.white,
            highlightedColor = Color.white,
            pressedColor = Color.white,
            selectedColor = Color.white,
            disabledColor = Color.gray,
            colorMultiplier = 1f // Reset color multiplier
        };
        
        // Completely override the color block
        button.colors = resetColors;

        // Reset text color to black
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.color = Color.black;
        }

        // Reset scale
        StartCoroutine(ScaleButton(button.transform, Vector3.one, 0.2f));
    }

    // Create a new ColorBlock for the selected button with explicit colors
    ColorBlock selectedColors = new ColorBlock
    {
        normalColor = new Color(0.5f, 0.8f, 1f, 1f), // Soft blue highlight
        highlightedColor = new Color(0.5f, 0.8f, 1f, 1f),
        pressedColor = new Color(0.4f, 0.7f, 0.9f, 1f),
        selectedColor = new Color(0.5f, 0.8f, 1f, 1f),
        disabledColor = Color.gray,
        colorMultiplier = 1f
    };
    
    selectedButton.colors = selectedColors;

    // Change text color to white for the selected button
    TMP_Text selectedButtonText = selectedButton.GetComponentInChildren<TMP_Text>();
    if (selectedButtonText != null)
    {
        selectedButtonText.color = Color.white;
    }

    // Scale up the selected button
    StartCoroutine(ScaleButton(selectedButton.transform, Vector3.one * 1.2f, 0.2f));

    // Update the selected level
    selectedLevel = levelName;
    Debug.Log("Selected level: " + selectedLevel);
}

// New method to handle smooth scaling of buttons
private IEnumerator ScaleButton(Transform buttonTransform, Vector3 targetScale, float duration)
{
    Vector3 startScale = buttonTransform.localScale;
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        buttonTransform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Ensure we end exactly on the target scale
    buttonTransform.localScale = targetScale;
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