using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main; 
    public Transform startPoint;

    public Transform[] path; 
    public void Awake() { 
        main = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}