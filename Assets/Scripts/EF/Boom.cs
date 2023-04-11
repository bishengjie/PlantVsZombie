using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : BaseEffectObj
{
    public override string AnimationName => "Boom";
    public override GameObject PrefabForObjPool => GameManager.Instance.GameConf.Boom;

}
