using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI waveText;

    private float gameTime;

    public void InitializeHUD(int lives, int startWave)
    {
        UpdateLives(lives);
        UpdateWave(startWave);
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        timerText.text = "" + Mathf.FloorToInt(gameTime).ToString();
    }

    public void UpdateLives(int lives)
    {
        if (livesText != null)
        {
            livesText.text = "" + lives.ToString();
            
            if (lives <= 0)
            {
                GameOver();
            }
        }
        else
        {
            Debug.LogWarning("livesText is not assigned!");
        }
    }

    public void UpdateWave(int wave)
    {
        waveText.text = "" + wave.ToString();
    }

    private void GameOver()
    {
        livesText.text = "Game Over";

        Time.timeScale = 0;

        Debug.Log("Game Over!");
    }
}
