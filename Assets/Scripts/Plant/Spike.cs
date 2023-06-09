using System.Collections.Generic;
using UnityEngine;

public class Spike : PlantBase
{
    public override float MaxHp => 300;

    public override bool ZombieCanEat => false;

    protected override Vector2 offset => new Vector2(0, -0.4f);
    
    // 攻击的CD， 也就是攻击间隔
    protected override float attackCD => 1;
    // 攻击力
    protected override int attackValue => 20;

    protected override void OnInitForPlace()
    {
        // 每秒检测有没有敌人被我攻击
        InvokeRepeating("CheckAttack", 0, attackCD);
    }

    // 检测攻击
    private void CheckAttack()
    {
        // 找到可以被我攻击的并且附加伤害
        List<ZombieBase> zombies = ZombieManager.Instance.GetZombies((int)currentGrid.Point.y, transform.position, 0.665f);
        if (zombies == null) return;
        for (int i = 0; i < zombies.Count; i++)
        {
            zombies[i].Hurt(attackValue);
        }
    }
}
