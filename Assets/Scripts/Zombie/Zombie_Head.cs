using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Head : BaseEffectObj
{
    protected override GameObject PrefabForObjPool => GameManager.Instance.GameConf.Zombie_Head;

}
