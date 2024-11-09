using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;  // Reference to GameManager
    private int health;

    private void Start()
    {
        health = gameManager != null ? gameManager.GetInitialLives() : 5;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        gameManager.PlayerDamaged();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has been defeated!");
        gameManager.EndGame();
    }
}
