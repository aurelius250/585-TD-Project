using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Attributes")] 
    [SerializeField] private int hitPoints = 2; 

    public void TakeDamage(int dmg) { 
        hitPoints -= dmg; 

        if (hitPoints <= 0) { 
            EnemySpawner.onEnemyDestroy.Invoke(); 
            Destroy(gameObject); 

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
