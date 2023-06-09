using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BucketheadZombie :  ZombieBase
{
    protected override int MaxHp => 1370;

    // 速度决定几秒走一路
    protected override float speed => 6;

    // 攻击力
    protected override float attackValue => 100;

    protected override GameObject Prefab => GameManager.Instance.GameConf.BucketheadZombie;

    public override void InitZombieHpState()
    {
        zombieHpState = new ZombieHpState(
            0,
            new List<int>() { MaxHp, 270 },
            new List<string>() { "BucketheadZombie_walk"},
            new List<string>() { "BucketheadZombie_Attack"},
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