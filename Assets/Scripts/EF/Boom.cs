using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : BaseEffectObj
{
    protected override GameObject PrefabForObjPool => GameManager.Instance.GameConf.Boom;
}
