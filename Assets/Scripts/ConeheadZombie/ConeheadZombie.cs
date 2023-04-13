using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConeheadZombie : ZombieBase
{ 
    protected override int MaxHp => 640;

    // 速度决定几秒走一路
    protected override float speed => 6;

    // 攻击力
    protected override float attackValue => 100;

    protected override GameObject Prefab => GameManager.Instance.GameConf.ConeheadZombie;

    public override void InitZombieHpState()
    {
        zombieHpState = new ZombieHpState(
            0,
            new List<int>() { MaxHp, 270 },
            new List<string>() { "ConeheadZombie_Walk"},
            new List<string>() { "ConeheadZombie_Attack"},
            new List<UnityAction>() { null, HpStateEvent }
        );
    }


    public override void OnDead()
    {
        
    }

    // 生命值过低时加载一个普通僵尸
    public void HpStateEvent()
    {
        // 先召唤一个普通僵尸
        Zombie zombie = ZombieManager.Instance.CreateStateZombie((int)currGrid.Point.y, transform.position);
        // 同步动画
        zombie.InitForOnTheZombieCreate(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        // 自身死亡—不涉及到动画，直接消失 
        State = ZombieState.Dead;
    }

}
