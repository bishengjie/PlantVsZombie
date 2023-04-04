using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏配置
/// </summary>
[CreateAssetMenu(fileName = "GameConf",menuName = "GameConf")]
public class GameConf : ScriptableObject
{
     [Tooltip("阳光")] 
     public GameObject Sun;

}
