using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagZombie :  ZombieBase
{
    protected override int MaxHp => 270;

    // 速度决定几秒走一路
    protected override float speed => 4;

    // 攻击力
    protected override float attackValue => 100;

    protected override GameObject Prefab => GameManager.Instance.GameConf.FlagZombie;

    public override void InitZombieHpState()
    {
        zombieHpState = new ZombieHpState(
            0,
            new List<int>() { MaxHp, 90 },
            new List<string>() { "FlagZombie_Walk", "FlagZombie_LostHeadWalk" },
            new List<string>() { "FlagZombie_Attack", "FlagZombie_LostHeadAttack" },
            new List<UnityAction>() { null, CheckLostHead }
        );
    }


    public override void OnDead()
    {
        // 创建一个死亡身体，用于体现效果
        Zombie_DieBody body = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Zombie_DieBody)
            .GetComponent<Zombie_DieBody>();
        body.Init(animator.transform.position);
    }

    // 检查掉头
    private void CheckLostHead()
    {
        // 创建一个死亡身体
        if (!isLostHead)
        {
            // 头掉
            isLostHead = true;
            // 创建一个头
            Zombie_Head head = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Zombie_Head)
                .GetComponent<Zombie_Head>();
            head.Init(animator.transform.position);
            // 状态检测
            CheckState();
        }

    }
}
