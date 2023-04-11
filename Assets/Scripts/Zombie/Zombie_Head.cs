using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Head : BaseEffectObj
{
    
    public override string AnimationName => "Zombie_Head";
    public override GameObject PrefabForObjPool => GameManager.Instance.GameConf.Zombie_Head;

}
