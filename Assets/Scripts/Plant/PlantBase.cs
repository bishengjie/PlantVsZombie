using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 植物的基类
/// </summary>
///   抽象类
public abstract  class PlantBase : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    // 当前植物所在的网格
    protected Grid currentGrid;
    protected float hp;
    protected PlantType plantType;

    public float Hp { get => hp; }
    public abstract float MaxHp { get ; }
    
    // 任何情况的的通用初始化
    protected void InitForAll(PlantType type)
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.plantType = type;
    }
  
    // 创建时的初始化
    public void InitForCreate(bool inGrid,PlantType type,Vector2 pos)
    {
        transform.position = pos;
        InitForAll(type);
        animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = -1;
            spriteRenderer.color = new Color(1,1,1,0.6f);
        }
        else
        {
            spriteRenderer.color = new Color(1,1,1,1);
            spriteRenderer.sortingOrder = 1;
        }
    }
    
    // 放置时的初始化
    public void InitForPlace(Grid grid,PlantType type)
    {
        InitForAll(type);
       spriteRenderer.color = new Color(1,1,1,1);
        hp = MaxHp;
        currentGrid = grid;
        currentGrid.CurrPlantBase = this;
        transform.position = grid.Position;
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        OnInitForPlace();
    }

    /// <summary>
    /// 受伤方法，被僵尸攻击调用
    /// </summary>
    /// <param name="hurtValue"></param>
    public void Hurt(float hurtValue)
    {
        hp -= hurtValue;
        print(hp);
        // 发光效果
        StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        if (hp <= 0)
        {
            // 死亡
            Dead();
        }
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

    public void Dead()
    {
        if (currentGrid != null)
        {
            currentGrid.CurrPlantBase = null;
            currentGrid = null;
        }
        StopAllCoroutines();
        CancelInvoke();
        PoolManager.Instance.PushObj(PlantManager.Instance.GetPlantByType(plantType),gameObject);
    }
    protected virtual void OnInitForPlace()
    {
        
    }

}
