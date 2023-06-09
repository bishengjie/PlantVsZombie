using System.Collections;
using UnityEngine;
/// <summary>
/// 豌豆射手
/// </summary>
public class Peashooter : PlantBase
{
    public override float MaxHp
    {
        get
        {
            return 300;
        }
    }
    
    // 攻击的CD， 也就是攻击间隔
    protected override float attackCD => 1.4f;
    // 攻击力
    protected override int attackValue => 20;

    
    // 是否可以攻击
    private bool canAttack;
   
    // 创建子弹的偏移量
    private Vector3 creatBulletoffsetPos = new Vector2(0.562f, 0.386f);

    protected  override void OnInitForPlace()
    {
        canAttack = true;
        // 可能要攻击
        InvokeRepeating("Attack",0,0.2f);
    }

    private void Attack()
    {
        if (canAttack == false) return;
        // 从僵尸管理器中获取一个最近的僵尸
        ZombieBase zombie = ZombieManager.Instance.GetZombieByLineMinDistance((int)currentGrid.Point.y, transform.position);
        // 没有僵尸 跳出
        if (zombie == null) return;
        // 僵尸必须在草坪上， 否侧跳出
        if (zombie.CurrGrid.Point.x == 8 &&
            Vector2.Distance(zombie.transform.position, zombie.CurrGrid.Position) > 1.5f) return;
        // 如果僵尸不在我的右边，也跳出
        if (zombie.transform.position.x < transform.position.x) return;

        // 从这里开始，都是可以正常攻击的
        // 在枪口实例化一个子弹
        Bullet bullet = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.Bullet1).GetComponent<Bullet>();
        bullet.transform.SetParent(transform);
        bullet.Init(attackValue, transform.position + creatBulletoffsetPos);
        CDEnter();
        canAttack = false;
    }

    // 进入CD
    private void CDEnter()
    {
        // 遮罩后，开始计算冷却
        StartCoroutine(CalCD());
    }

    // 计算冷却时间
    IEnumerator CalCD()
    {
       yield return new WaitForSeconds(attackCD);
       // 到这里，意味着冷却时间到了，可以放置了
        canAttack = true;
    }
}
