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
    private GameObject mainPanel;

    private UIPlantCard currCard;
    public UIPlantCard CurrCard
    {
        get => currCard;
        set
        {
            // if(currCard==value)return;//
            // 置空上一个卡片的状态
            // if (currCard!=null)
            // {
            //     currCard.WantPlant = false;
            // }
            currCard = value;
        }
    }
    private void Awake()
    {
        Instance = this;
        mainPanel = transform.Find("MainPanel").gameObject;
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
    // 设置主面板的显示
    public void SetmainPanelActive(bool isShow)
    {
        mainPanel.SetActive(isShow);
    }
}
