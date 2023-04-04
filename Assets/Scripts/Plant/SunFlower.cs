using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    // 寻找自身相关组件
    private void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
  
    // 创建时的初始化
    public void InitForCreate(bool inGrid)
    {
        Find();
        animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = -1;
            spriteRenderer.color = new Color(1,1,1,0.6f);
        }
    }
    
    // 放置时的初始化
    public void InitForPlace()
    {
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        InvokeRepeating("CreateSun", 3, 3);
    }

    // 创建阳光
    private void CreateSun()
    {
        Sun sun = Instantiate(GameManager.Instance.GameConf.Sun, transform.position, Quaternion.identity, transform)
            .GetComponent<Sun>();
        // 让阳光进行跳跃动画
        sun.JumpAnimation();
    }
}
