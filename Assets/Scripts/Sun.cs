using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    // 下落的目标点
    private float downTargetPosY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= downTargetPosY)
        {
            Invoke("DestroySun",5);
            return;
        }
        transform.Translate(Vector3.down*Time.deltaTime);
    }

    /// <summary>
    /// 鼠标点击阳光的时候，增加游戏管理器中的阳光数量 并且销毁自身
    /// </summary>
    private void OnMouseDown()
    {
        GameManager.Instance.SunNum += 50;
        DestroySun();
        print("销毁");
    }

    // 销毁
    private void DestroySun()
    {
       Destroy(gameObject);
    }

    // 当阳光从天空中初始化的方法
    public void InitForSky(float downTargetPosY,float createPosX,float CreatePosY)
    {
        this.downTargetPosY = downTargetPosY;
        transform.position = new Vector2(createPosX, CreatePosY);
    }
}
