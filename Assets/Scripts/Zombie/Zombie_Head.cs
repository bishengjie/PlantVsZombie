using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Head : MonoBehaviour
{
    private Animator animator;
    private bool isOver;
    public void Init(Vector2 pos)
    {
        animator = GetComponent<Animator>();
        transform.position = pos;
        isOver = false;
        animator.speed = 1;
        animator.Play("Zombie_Head",0,0);
    }

    void Update()
    {
        if (!isOver && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1)
        {
            // 播放完毕
            animator.speed = 0;
            isOver = true;
            // 两秒之后销毁自身
            Invoke("Destroy", 2);

        }
    }

    private void Destroy()
    {
        // 取消延迟调用
        CancelInvoke();
        // 把自己放进缓存池
        PoolManager.Instance.PushObj(GameManager.Instance.GameConf.Zombie_Head,gameObject);
    }
}
