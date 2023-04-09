using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameConf GameConf;//config配置
    private int currentLevel;
    public int CurrentLevel
    {
        get => currentLevel;
        set
        {
            currentLevel = value;
            LVManager.Instance.StartLV(currentLevel);
        }
    }

    private void Awake()
    {
        Instance = this;
        GameConf = Resources.Load<GameConf>("GameConf");
    }

     void Start()
    {
        CurrentLevel = 1;
    }
}
