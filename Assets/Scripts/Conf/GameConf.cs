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
     
     [Tooltip("太阳花")] 
     public GameObject SunFlower;
     
     [Tooltip("豌豆射手")] 
     public GameObject Peashooter;
     
     [Header("僵尸")]
     [Tooltip("僵尸的头")] 
     public GameObject Zombie_Head;
     [Tooltip("普通僵尸")] 
     public GameObject Zombie;
     
     [Header("子弹")]
     [Tooltip("豌豆击中")] 
     public Sprite Bullet1Hit;
     [Tooltip("豌豆子弹")] 
     public GameObject Bullet1;

}
