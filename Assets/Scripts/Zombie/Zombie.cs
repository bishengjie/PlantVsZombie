using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator animator;
    private Grid currGrid;
    // 速度决定几秒走一路
    private float speed = 5;
    // 在攻击中
    private bool isAttackState;
    // 攻击力
    private float attackValue = 100;
    void Start()
    {
        animator=GetComponentInChildren<Animator>();
        int rangeWalk = Random.Range(1, 4);
        switch (rangeWalk)
        {
            case 1:
                animator.Play("Zombie_Walk1");
                break;
            case 2:
                animator.Play("Zombie_Walk2");
                break;
            case 3:
                animator.Play("Zombie_Walk3");
                break;
        }

        GetGridByVerticalNum(0);
    }
   
    void Update()
    {
        Move();
    }


    // 获取一个网格，决定僵尸在哪出现
    private void GetGridByVerticalNum(int verticalNum)
    {
        currGrid = GridManager.Instance.GetGridByVerticalNum(verticalNum);
        transform.position = new Vector3(transform.position.x, currGrid.Position.y);
    }

    private void Move()
    {
        // 如果当前网格为空跳过移动检测
        if (currGrid == null) return;
        // 如果在攻击中也跳过移动检测
        if(isAttackState)return;
        currGrid = GridManager.Instance.GetGridByWorldPos(transform.position);
        // 当前网格中有植物并且在我的左边且距离很近
        if (currGrid.HavePlant
            && currGrid.CurrPlantBase.transform.position.x < transform.position.x
            && transform.position.x - currGrid.CurrPlantBase.transform.position.x < 0.3f)
        {
            // 攻击植物
            Attack(currGrid.CurrPlantBase);

        }
        transform.Translate((new Vector2(-1.33f, 0) * (Time.deltaTime / 1)) / speed);
    }

    private void Attack(PlantBase plant)
    {
        isAttackState = true;
        // 自身播放攻击动画
        animator.Play("Zombie_Attack");
        
        // 植物的相关逻辑
        StartCoroutine(DoHurt(plant));
    }
    
    // 附伤害给植物
    IEnumerator DoHurt(PlantBase plant)
    {
        // 植物的什么大于则扣血
        while (currGrid.CurrPlantBase.Hp>0)
        {
            plant.Hurt(attackValue/5);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
