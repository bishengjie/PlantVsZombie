using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 植物的基类
/// </summary>
///   抽象类
public abstract  class PlantBase : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    
    // 寻找自身相关组件
    protected void Find()
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
        OnInitForPlace();
    }

    protected virtual void OnInitForPlace()
    {
        
    }

}
