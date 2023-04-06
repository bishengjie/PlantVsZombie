using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ZombieState
{
    Idel,
    Walk,
    Attack,
    Dead
}

public class Zombie : MonoBehaviour
{
    // 我的状态
    private ZombieState state;
    private Animator animator;
    private Grid currGrid;

    // 生命值
    private int hp = 270;
    // 速度决定几秒走一路
    private float speed = 5;

    // 在攻击中
    private bool isAttackState;
    
    // 是否已经失去头
    private bool isLostHead = false;

    // 攻击力
    private float attackValue = 100;

    // 行走动画的名称，随机来
    private string walkAnimationStr;

    // 修改状态会直接改变动画
    public ZombieState State { get => state;
        set
        {
            state = value;
            CheckState();
        }
    }
    public Grid CurrGrid { get => currGrid; }

    public int Hp
    {
        get => hp;
        set { hp = value;
            if (hp <= 90&&!isLostHead)
            {
                // 头掉
                isLostHead = true;
                walkAnimationStr =  "Zombie_LostHead";
                // 状态检测
                CheckState();
                
            }
            if (hp<=0)
            {
                State = ZombieState.Dead;
            }
        }
    }

    private void Awake()
    {
        int rangeWalk = Random.Range(1, 4);
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

        GetGridByVerticalNum(0);
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        ZombieManager.Instance.AddZombie(this);
    }

    void Update()
    {
        FSM();
    }
    
    // 状态检测
    private void CheckState()
    {
        switch (State)
        {
            case ZombieState.Idel:
                // 播放行走动画，但是要卡在第一帧
                animator.Play(walkAnimationStr, 0, 0);
                animator.speed = 0;
                break;
            case ZombieState.Walk:
                animator.Play(walkAnimationStr);
                animator.speed = 1;
                break;
            case ZombieState.Attack:
                animator.Play("Zombie_Attack");
                animator.speed = 1;
                break;
            case ZombieState.Dead:
                break;
        }
    }
    // 状态检测
    private void FSM()
    {
        switch (State)
        {
            case ZombieState.Idel:
                State = ZombieState.Walk;
                break;
            case ZombieState.Walk:
                // 一直向左右，并且遇到植物会攻击，攻击结束继续走
                Move();
                break;
            case ZombieState.Attack:
                if (isAttackState) break;
                Attack(currGrid.CurrPlantBase);
                break;
            case ZombieState.Dead:
                Dead();
                break;
        }
    }


    // 获取一个网格，决定僵尸在哪出现
    private void GetGridByVerticalNum(int verticalNum)
    {
        //
        currGrid = GridManager.Instance.GetGridByVerticalNum(verticalNum);
        transform.position = new Vector3(transform.position.x, currGrid.Position.y);
    }

    private void Move()
    {
        // 如果当前网格为空跳过移动检测
        if (currGrid == null) return;
        currGrid = GridManager.Instance.GetGridByWorldPos(transform.position);
        // 当前网格中有植物并且在我的左边且距离很近
        if (currGrid.HavePlant
            && currGrid.CurrPlantBase.transform.position.x < transform.position.x
            && transform.position.x - currGrid.CurrPlantBase.transform.position.x < 0.3f)
        {
            // 攻击植物
            State = ZombieState.Attack;
            return;
        }

        transform.Translate((new Vector2(-1.33f, 0) * (Time.deltaTime / 1)) / speed);
    }

    private void Attack(PlantBase plant)
    {
        isAttackState = true;
        // 植物的相关逻辑
        StartCoroutine(DoHurtPlant(plant));
    }

    // 附伤害给植物
    IEnumerator DoHurtPlant(PlantBase plant)
    {
        // 植物的什么大于则扣血
        while (plant.Hp > 0)
        {
            plant.Hurt(attackValue / 5);
            yield return new WaitForSeconds(0.2f);
        }

        isAttackState = false;
        State = ZombieState.Walk;
    }

    // 自身受伤
    public void Hurt(int attackValue)
    {
        Hp -= attackValue;
    }

    private void Dead()
    {
        // 告诉僵尸管理器，僵尸死了
        ZombieManager.Instance.RemoveZombie(this);
        Destroy(gameObject);
    }
}
