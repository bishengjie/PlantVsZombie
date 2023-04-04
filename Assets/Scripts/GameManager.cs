using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    // 阳光预制体
    public GameConf GameConf { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        GameConf = Resources.Load<GameConf>("GameConf");
    }

    void Start()
    {
        
    }
    
    void Update()
    {
      
    }
}
