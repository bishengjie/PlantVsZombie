using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Zombie : ZombieBase
{
    protected override int MaxHp => 270;

    // 速度决定几秒走一路
    protected override float speed => 6;

    // 攻击力
    protected override float attackValue => 100;

    protected override GameObject Prefab => GameManager.Instance.GameConf.Zombie;

    public override void InitZombieHpState()
    {
        int rangeWalk = Random.Range(1, 4);
        string walkAnimationStr = "";
        switch (rangeWalk)
        {
            case 1:
                walkAnimationStr = "Zombie_Walk1";
                break;
            case 2:
                walkAnimationStr = "Zombie_Walk2";
                break;
            case 3:
                walkAnimationStr = "Zombie_Walk3";
                break;
        }
        zombieHpState = new ZombieHpState(
             0,
             new List<int>() { MaxHp, 90 },
            new List<string>() { walkAnimationStr, "Zombie_LostHead" },
            new List<string>() { "Zombie_Attack", "Zombie_LostHeadAttack" },
            new List<UnityAction>() { null, CheckLostHead }
        );
    }


    public override void OnDead()
    {
        // 创建一个死亡身体，用于体现效果
        Zombie_DieBody body = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Zombie_DieBody)
            .GetComponent<Zombie_DieBody>();
        body.Init(animator.transform.position);
    }

    // 检查掉头
    private void CheckLostHead()
    {
        // 创建一个死亡身体
        if (!isLostHead)
        {
            // 头掉
            isLostHead = true;
            // 创建一个头
            Zombie_Head head = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Zombie_Head)
                .GetComponent<Zombie_Head>();
            head.Init(animator.transform.position);
            // 状态检测
            CheckState();
        }
    }

    // 从其他僵尸那里初始化
    public void InitForOnTheZombieCreate(float time)
    {
        // 把行走动画确定在walk3
        zombieHpState.hpLimitWakAnimationStr[0] = "Zombie_Walk3";
        animator.Play("Zombie_Walk3",0,time);
    }
}
