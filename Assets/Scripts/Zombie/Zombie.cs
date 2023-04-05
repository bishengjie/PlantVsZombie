using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator animator;
    private Grid currGrid;
    // 速度决定几秒走一路
    private float speed = 5;
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

    private void Move()
    {
        if(currGrid==null)return;

        transform.Translate((new Vector2(-1.33f, 0) * (Time.deltaTime / 1))/ speed);
        
    }

    // 获取一个网格，决定僵尸在哪出现
    private void GetGridByVerticalNum(int verticalNum)
    {
        currGrid = GridManager.Instance.GetGridByVerticalNum(verticalNum);
        transform.position = new Vector3(transform.position.x, currGrid.Position.y);
    }
}
