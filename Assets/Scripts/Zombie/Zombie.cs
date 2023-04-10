using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
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
    private SpriteRenderer spriteRenderer;
    private Grid currGrid;

    // 生命值
    private int hp = 270;
    // 速度决定几秒走一路
    private float speed = 5;

    // 在攻击中
    private bool isAttackState;
    
    // 是否已经失去头
    private bool isLostHead;

    // 攻击力
    private float attackValue = 100;

    // 行走动画的名称，随机来
    private string walkAnimationStr;
    
       // 攻击动画的名称
    private string attackAnimationStr;

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
        set
        {
            hp = value;
            if (hp <= 90 && !isLostHead)
            {
                // 头掉
                isLostHead = true;
                walkAnimationStr = "Zombie_LostHead";
                attackAnimationStr = "Zombie_LostHeadAttack";
                // 创建一个头
                Zombie_Head head = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Zombie_Head)
                    .GetComponent<Zombie_Head>();
                head.Init(animator.transform.position);
                // 状态检测
                CheckState();
            }
            if (hp <= 0)
            {
                print("死死死");
                State = ZombieState.Dead;
                // 创建一个死亡身体，用于体现效果
                Zombie_DieBody body = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Zombie_DieBody)
                    .GetComponent<Zombie_DieBody>();
                body.Init(animator.transform.position);
            }

        }
    }

    public void Init(int lineNum,int orderNum,Vector2 pos)
    {
        hp = 270;
        isLostHead = false;
        transform.position = pos;
        InitAnimationName();
        Find();
        GetGridByVerticalNum(lineNum);
        CheckOrder(orderNum);
        State = ZombieState.Idel;
    }
    
    // 检查排序
    private void CheckOrder(int orderNum)
    {
        // 在草坪上，越靠近0越大，反之越小
        // 0层是最大的（400~499） 4层是最小的（0~99）
        int startNum = 0;
        switch ((int)CurrGrid.Point.y)
        {
            case 0:
                startNum = 400;
                break;
            case 1:
                startNum = 300;
                break;
            case 2:
                startNum = 200;
                break;
            case 3:
                startNum = 100;
                break;
            case 4:
                startNum = 0;
                break;
        }

        spriteRenderer.sortingOrder = startNum + orderNum;

    }
    
    // 初始化动画名称
    private void InitAnimationName()
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

        attackAnimationStr = "Zombie_Attack";
       
    }

    void Find()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
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
                animator.Play(walkAnimationStr,0,animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
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
        // 可以被僵尸吃掉它
        if (currGrid.HavePlant
            && CurrGrid.CurrPlantBase.ZombieCanEat
            && currGrid.CurrPlantBase.transform.position.x < transform.position.x
            && transform.position.x - currGrid.CurrPlantBase.transform.position.x < 0.3f)
        {
            // 攻击植物
            State = ZombieState.Attack;
            return;
        }
        // 如果我在最左边的网格，并且我已经越过了它
        else if (currGrid.Point.x == 0 && currGrid.Position.x - transform.position.x > 1f)
        {
            // 我们要走向终点 - 房子
            Vector2 pos = transform.position;
            Vector2 target = new Vector2(-9.17f, -1.37f);
            Vector2 direction = (target - pos).normalized * 3f;
            transform.Translate((direction * (Time.deltaTime / 1)) / speed);
            // 如果我距离终点很近，意味着游戏结束
            if (Vector2.Distance(target,pos)<0.05f)
            {
                // 出发游戏结束
                LVManager.Instance.GameOver();
               
            }
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
        while (plant != null && plant.Hp > 0)
        {
            int num = 0;
            if (num == 5) num = 0;
            // 播放僵尸吃植物的音效
            if (num == 0)
            {
                AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.zombieEat);
            }
            num += 1;
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
        StartCoroutine(ColorEF(0.2f, new Color(0.4f, 0.4f, 0.4f), 0.05f, null));
    }

    public void Dead()
    {
        // 告诉僵尸管理器，僵尸死了
        ZombieManager.Instance.RemoveZombie(this);
        StopAllCoroutines();
        currGrid = null;
        PoolManager.Instance.PushObj(GameManager.Instance.GameConf.Zombie,gameObject);
    }
    // 颜色变化效果
    protected IEnumerator ColorEF(float wantTime, Color targetColor, float delayTime, UnityAction fun)
    {
        float currentTime = 0;
        float lerp;
        while (currentTime < wantTime)
        {
            yield return new WaitForSeconds(delayTime);
            lerp = currentTime / wantTime;
            currentTime += delayTime;
            spriteRenderer.color = Color.Lerp(Color.white, targetColor, lerp);

        }

        spriteRenderer.color = Color.white;
        if (fun != null) fun();
    }

    public void StartMove()
    {
        State = ZombieState.Walk;
    }
}
