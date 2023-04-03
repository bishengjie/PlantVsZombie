using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // 阳光的数量
    private int sunNum;
    
    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            UIManager.Instance.UpdateSunNum(sunNum);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SunNum = 100;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
