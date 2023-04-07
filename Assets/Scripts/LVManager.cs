using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 管卡状态
public enum LVState
{
    // 开始游戏
    Start,
    // 战斗中
    Fight,
    // 结束
    Over
}
public class LVManager : MonoBehaviour
{
    public static LVManager Instance;
    private static LVState currLVState;
    public LVState CurrLVState
    {
        get => currLVState;
        set
        {
            currLVState = value;
            switch (currLVState)
            {
                case LVState.Start:
                    // 隐藏UI面板
                    UIManager.Instance.SetmainPanelActive(false);
                    // 摄像机移动到右侧观察关卡僵尸
                    Camera_C.Instance.MoveForLVStart();
                    // 移回左侧 并显示UI
                    // 切换到战斗模式
                    break;
                case LVState.Fight:
                    break;
                case LVState.Over:
                    break;
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrLVState = LVState.Start;
    }

}
