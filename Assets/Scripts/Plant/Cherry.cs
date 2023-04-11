using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : PlantBase
{
    public override float MaxHp => 300;
    
    // 攻击力
    protected override int attackValue => 1800;

    protected override void OnInitForPlace()
    {
        StartCoroutine(CheckBoom());
    }

    // 检测爆炸
    IEnumerator CheckBoom()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
            {
                // 爆炸
                Boom();
            }
        }
    }

    private void Boom()
    {
        // 播放爆炸音效
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.boom);
        // 找到可以被我攻击的并且附加伤害
        List<Zombie> zombies = ZombieManager.Instance.GetZombies(transform.position,2.25f);
        if (zombies == null) return;
        for (int i = 0; i < zombies.Count; i++)
        {
            zombies[i].BoomHurt(attackValue);
        }
        // 生成攻击特效
        // 创建一个头
        Boom boom = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Boom)
            .GetComponent<Boom>();
        boom.Init(transform.position);
        // 自身死亡
        Dead();
            
    }
}
