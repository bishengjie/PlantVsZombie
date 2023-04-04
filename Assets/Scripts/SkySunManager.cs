using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunManager : MonoBehaviour
{
   
    // 创建阳光时的坐标
    private float createSunPosY = 6;
    
    // 创建阳光时 最左最右的X轴坐标，在这个范围内随机
    private float createSunMaxPosX = 3;
    private float createSunMinPosX = -7.5f;
  
    // 阳光下落时 最高和最低的Y轴坐标，在这个范围内随机
    private float sunDownMaxPosY = 2.5f;
    private float sunDownMinPosY = -4.7f;
    void Start()
    {
        
        InvokeRepeating("CreateSun", 3, 3);
    }

    

    // 在天空中生成阳光
    void CreateSun()
    {
       Sun sun= Instantiate(GameManager.Instance.GameConf.Sun, Vector3.zero, Quaternion.identity,transform).GetComponent<Sun>();
       float downY = Random.Range(sunDownMinPosY, sunDownMaxPosY);
       float createX = Random.Range(createSunMinPosX, createSunMaxPosX);
       sun.InitForSky(downY,createX,createSunPosY);
    }
}
