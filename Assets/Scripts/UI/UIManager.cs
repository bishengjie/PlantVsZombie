using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Text SunNumText;
  
    private void Awake()
    {
        Instance = this;
        SunNumText = transform.Find("MainPanel/SunNumText").GetComponent<Text>();
    }

    void Start()
    {
       
    }

    // 更新阳光的数字
    public void UpdateSunNum(int num)
    {
        SunNumText.text = num.ToString();
    }
    
    // 获取阳光数量Text的坐标
    public Vector3 GetSunNumTextPos()
    {
        return SunNumText.transform.position;
    }
}
