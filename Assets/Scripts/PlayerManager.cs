using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;//委托

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    // 阳光的数量
    private int sunNum = 100;
    // 阳光数量更新时的事件
    private UnityAction SunNumUpdateAction;

    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            UIManager.Instance.UpdateSunNum(sunNum);
            if(SunNumUpdateAction!=null) SunNumUpdateAction();
        }
    }

    private void Awake()
    {
        Instance = this;

    }
    
    // 添加阳光数量更新时的事件监听
    public void AddSunNumUpdateActionListener(UnityAction action)
    {
        SunNumUpdateAction += action;
    }
}
