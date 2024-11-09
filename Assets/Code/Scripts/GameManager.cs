using UnityEngine;

public class GameManager: MonoBehaviour
{
    public HUDManager hudManager;  

    private int playerLives = 5;

    void Start()
    {
        hudManager.InitializeHUD(playerLives, 1); 
    }

    public void PlayerDamaged()
    {
        playerLives--;
        hudManager.UpdateLives(playerLives);

        if (playerLives <= 0)
        {
            EndGame();
        }
    }

    public int GetInitialLives()
    {
        return playerLives;  // Assumes `playerLives` is set at the start
    }


    public void EndGame()
    {
        Debug.Log("Game Over!");
    }
}
