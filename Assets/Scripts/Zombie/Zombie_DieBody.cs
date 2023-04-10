using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_DieBody : BaseEffectObj
{
    protected override GameObject PrefabForObjPool => GameManager.Instance.GameConf.Zombie_DieBody;
}
