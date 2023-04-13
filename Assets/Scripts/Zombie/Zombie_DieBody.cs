using UnityEngine;

public class Zombie_DieBody : BaseEffectObj
{
    public override string AnimationName => "Zombie_DieBody";
    public override GameObject PrefabForObjPool => GameManager.Instance.GameConf.Zombie_DieBody;
    
    // 用于炸死时的初始化
    public void InitForBoomDie(Vector2 pos)
    {
        Init(pos,"Zombie_BoomBody");
    }
}