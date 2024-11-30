using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;   // Array of enemy prefabs
    [SerializeField] private GameManager gameManager;    // Reference to GameManager
    [SerializeField] private HUDManager hudManager;      // Reference to HUDManager    

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;           // Base number of enemies
    [SerializeField] private float enemiesPerSecond = 0.5f; // Rate of enemy spawn per second
    [SerializeField] private float timeBetweenWaves = 5f;   // Time between waves
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Difficulty scaling factor per wave

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Event for enemy destruction

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        // Subscribe to the event when an enemy is destroyed
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        // Validate references and notify if missing
        ValidateReferences();

        // Start the first wave
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        // Spawn enemies at the specified rate
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        // End the wave when no enemies are left to spawn and all enemies are destroyed
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private IEnumerator StartWave()
    {
        // Wait before starting the next wave
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();

        // Update the HUD with the current wave
        if (hudManager != null)
        {
            hudManager.UpdateWave(currentWave);
        }
        else
        {
            Debug.LogError("HUDManager is not assigned.");
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        // Start the next wave
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void SpawnEnemy()
    {
        // Validate critical references before spawning
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy Prefabs are not assigned or empty.");
            return;
        }

        if (LevelManager.main == null || LevelManager.main.startPoint == null)
        {
            Debug.LogError("LevelManager.main or startPoint is not assigned.");
            return;
        }

        // Spawn the first enemy prefab at the start point
        GameObject prefabToSpawn = enemyPrefabs[0]; // You could change this to spawn different enemies per wave
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        // Calculate the number of enemies per wave with difficulty scaling
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private void ValidateReferences()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("Enemy Prefabs array is unassigned or empty. Please assign at least one prefab.");
        }

        if (hudManager == null)
        {
            Debug.LogError("HUDManager is not assigned. Please assign it in the inspector.");
        }

        if (LevelManager.main == null)
        {
            Debug.LogError("LevelManager.main is not assigned. Ensure it is set up correctly.");
        }
        else if (LevelManager.main.startPoint == null)
        {
            Debug.LogError("LevelManager.main.startPoint is not assigned. Please assign a valid Transform.");
        }
    }
}