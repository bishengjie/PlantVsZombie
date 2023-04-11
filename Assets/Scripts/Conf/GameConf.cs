using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏配置
/// </summary>
[CreateAssetMenu(fileName = "GameConf",menuName = "GameConf")]
public class GameConf : ScriptableObject
{
     [Tooltip("音乐")] 
     public GameObject EFAudio;
     public AudioClip buttonClick;
     public AudioClip pause;
     public AudioClip shovel;
     public AudioClip place;
     public AudioClip sunClick;
     public AudioClip boom;
    
     
     public AudioClip zombieEat;
     public AudioClip zombieHurtForPea;
     public AudioClip zombieGroan;//痛苦而发出声音 

     public AudioClip gameOver;
     
     
     [Tooltip("阳光")] 
     public GameObject Sun;
     
     [Tooltip("太阳花")] 
     public GameObject SunFlower;
     
     [Tooltip("豌豆射手")] 
     public GameObject Peashooter; 
     
     [Tooltip("土豆")] 
     public GameObject WallNut;
     
     [Tooltip("地刺")] 
     public GameObject Spike;
     
     [Tooltip("樱桃")] 
     public GameObject Cherry;
     
     [Header("僵尸")]
     [Tooltip("僵尸的头")] 
     public GameObject Zombie_Head;
     [Tooltip("僵尸的死亡身体")] 
     public GameObject Zombie_DieBody;
     [Tooltip("普通僵尸")] 
     public GameObject Zombie;
     
     [Header("子弹")]
     [Tooltip("豌豆击中")] 
     public Sprite Bullet1Hit;
     [Tooltip("豌豆子弹")] 
     public GameObject Bullet1;
     [Tooltip("豌豆正常")] 
     public Sprite Bullet1Nor;
     [Tooltip("爆炸效果")] 
     public GameObject Boom;

}
