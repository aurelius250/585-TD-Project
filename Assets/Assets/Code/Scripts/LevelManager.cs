using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main; 

    public Transform startPoint;
    public Transform[] path;


    public int money;

    public void Awake() 
    { 
        main = this;
    }

    private void Start()
    {
        money = 100;
    }

    public void IncreaseMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount) 
    { 
        if (amount <= money)
        {
            money -= amount;
            return true;
        }

        else
        {
            Debug.Log("You don't have enough money!");
            return false;
        }
    }
}