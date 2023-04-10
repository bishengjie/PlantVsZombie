using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance;
    private List<Zombie> zombies = new ();
    private int currOrderNum = 0;
    
    //所有僵尸都死亡时的事件
    private UnityAction AllZombieDeadAction;
    // 创建僵尸最大和最小的X坐标
    private float creatMaxX = 8.5f;
    private float creatMinX = 7.4f;
    public int CurrOrderNum { get => currOrderNum;
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
        Groan();
    }

    // 更新僵尸
    public void UpdateZombie(int zombieNum)
    {
        for (int i = 0; i < zombieNum; i++)
        {
            CreateZombie(Random.Range(0,5));
        }

        print("生成僵尸");
    }

    // 清理掉所有僵尸
    public void ClearZombie()
    {
        while (zombies.Count > 0)
        {
            zombies[0].Dead();
        }
    }


    // 获取一个X随机坐标，为了创建僵尸
    private float GetPosXRangeForCreateZombie()
    {
        return Random.Range(creatMinX, creatMaxX);
    }

    // 创建僵尸
    private void CreateZombie(int lineNum)
    {
        Zombie zombie = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Zombie).GetComponent<Zombie>();
        AddZombie(zombie);
        zombie.transform.SetParent(transform);
        zombie.Init(lineNum, CurrOrderNum,new Vector2(GetPosXRangeForCreateZombie(),0));
        CurrOrderNum++;
    }

    public void AddZombie(Zombie zombie)
    {
        zombies.Add(zombie);
    }
    
    public void RemoveZombie(Zombie zombie)
    {
        zombies.Remove(zombie);
        CheckAllZombieDeadForLevel();
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

    public void ZombieStartMove()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            zombies[i].StartMove();
        }
    }

    // 为关卡管理器检查所有僵尸死亡时的事件
    private void CheckAllZombieDeadForLevel()
    {
        if (zombies.Count == 0)
        {
            if (AllZombieDeadAction != null) AllZombieDeadAction();
        }
    }
    public void AddAllZombieDeadAction(UnityAction action)
    {
        AllZombieDeadAction += action;
    }
    public void RemoveAllZombieDeadAction(UnityAction action)
    {
        AllZombieDeadAction -= action;
    }

    // 5秒进行一次，有一定概率播放呻吟音效
    private void Groan()
    {
        StartCoroutine(DoGroan());
    } 
    IEnumerator DoGroan()
    {
        while (true)
        {
            // 有僵尸才进行随机
            if (zombies.Count>0)
            {
                // 如果随机数大于6则播放
                if (Random.Range(0, 10) > 6)
                {
                    AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.zombieGroan);
                }
            }
            yield return new WaitForSeconds(5);
        }
    }
}
