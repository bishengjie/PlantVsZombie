using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance;
    private List<Zombie> zombies = new List<Zombie>();

    private void Awake()
    {
        Instance = this;
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
