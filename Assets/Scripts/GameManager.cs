using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // 阳光的数量
    private int sunNum;
    // 阳光预制体
    public GameObject Prefab_sun { get; private set; }
    
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
        Prefab_sun = Resources.Load<GameObject>("Sun");
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
