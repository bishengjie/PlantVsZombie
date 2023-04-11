using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieHpState
{
    // 当前HP范围的索引 0~ 0代表最健康的状态
    private int currentHpLimitIndex;
    // 生命值阶段maxhp,80,30
    private List<int> hpLimit;
    // 生命值阶段的不同行走动画
    private List<string> hpLimitWakAnimationStr;
    // 生命值阶段的不同攻击动画
    private List<string> hpLimitAttackAnimationStr;
    
    // 不同状态切换时要做的事情
    private List<UnityAction> hpLimitAction;

    public ZombieHpState(int currentHpLimitIndex, List<int> hpLimit, List<string> hpLimitWakAnimationStr,List<string> hpLimitAttackAnimationStr, List<UnityAction> hpLimitAction)
    {
        this.currentHpLimitIndex = currentHpLimitIndex;
        this.hpLimit = hpLimit;
        this.hpLimitWakAnimationStr = hpLimitWakAnimationStr;
        this.hpLimitAttackAnimationStr = hpLimitAttackAnimationStr;
        this.hpLimitAction = hpLimitAction;
    }

    public void UpdateZombieHpState(int hp)
    {
        int tempIndex = 0;
        // 先确定当前僵尸在哪个阶段
        for (int i = 0; i < hpLimit.Count; i++)
        {
            // 如果传进来的hp小于这个hp界限的值，意味着他可能就是我们需要的当前index
            if (hp<=hpLimit[i])
            {
                tempIndex = i;
            }
        }
        // 如果需要修改
        if (currentHpLimitIndex!=tempIndex)
        {
            currentHpLimitIndex = tempIndex;
            // 状态切换时需要做的事情
            if (hpLimitAction[currentHpLimitIndex]!=null)
            {
                hpLimitAction[currentHpLimitIndex]();
            }
                
        }
    }
    
    // 获取当前行走动画 
    public string GetCurrentWalkAnimationStr()
    {
        return hpLimitWakAnimationStr[currentHpLimitIndex];
    }
    // 获取当前行走动画 
    public string GetCurrentAttackAnimationStr()
    {
        return hpLimitAttackAnimationStr[currentHpLimitIndex];
    }
}
