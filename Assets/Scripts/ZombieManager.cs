using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance;
    private List<Zombie> zombies = new List<Zombie>();
    private int currOrderNum = 0;

    // 创建僵尸最大和最小的X坐标
    private float creatMaxX = 8.5f;
    private float creatMinX = 7.4f;
    public int CurrOrderNum
    {
        get => currOrderNum;
        set
        {
            currOrderNum = value;
            if (value>50)
            {
                currOrderNum = 0;
            }
        } 
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateZombie(0,GetPosXRangeForCreateZombie());
        CreateZombie(1,GetPosXRangeForCreateZombie());
        CreateZombie(2,GetPosXRangeForCreateZombie());
        CreateZombie(3,GetPosXRangeForCreateZombie());
        CreateZombie(4,GetPosXRangeForCreateZombie());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateZombie(0,GetPosXRangeForCreateZombie());
            CreateZombie(1,GetPosXRangeForCreateZombie());
            CreateZombie(2,GetPosXRangeForCreateZombie());
            CreateZombie(3,GetPosXRangeForCreateZombie());
            CreateZombie(4,GetPosXRangeForCreateZombie());
        }
    }

    // 获取一个X随机坐标，为了创建僵尸
    private float GetPosXRangeForCreateZombie()
    {
        return Random.Range(creatMinX, creatMaxX);
    }

    // 创建僵尸
    private void CreateZombie(int lineNum, float posX)
    {
        Zombie zombie = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Zombie).GetComponent<Zombie>();
        AddZombie(zombie);
        zombie.transform.SetParent(transform);
        zombie.Init(lineNum, CurrOrderNum,new Vector2(posX,0));
        CurrOrderNum++;
    }

    public void AddZombie(Zombie zombie)
    {
        zombies.Add(zombie);
    }
    
    public void RemoveZombie(Zombie zombie)
    {
        zombies.Remove(zombie);
    }

    // 获取一个距离最近的僵尸
    public Zombie GetZombieByLineMinDistance(int lineNum, Vector3 pos)
    {
        Zombie zombie = null;
        float distance = 10000;
        for (int i = 0; i < zombies.Count; i++)
        {
            if (zombies[i].CurrGrid.Point.y == lineNum
                && Vector2.Distance(pos, zombies[i].transform.position) < distance)
            {
                distance = Vector2.Distance(pos, zombies[i].transform.position);
                zombie = zombies[i];
            }
        }

        return zombie;
    }
}
