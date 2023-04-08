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
                    // 隐藏UI主面板
                    UIManager.Instance.SetmainPanelActive(false);
                    //刷新僵尸秀的僵尸
                    ZombieManager.Instance.UpdateZombie(5);
                    // 摄像机移动到右侧观察关卡僵尸
                    Camera_C.Instance.StartMove(LVStartCameraBackAction);
                    break;
                case LVState.Fight:
                    // 显示主面板
                    UIManager.Instance.SetmainPanelActive(true);
                    // 重新刷新僵尸，根据关卡难度
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

    // 关卡开始时 摄像机回归后要执行的方法
    private void LVStartCameraBackAction()
    {
        // 让阳光开始创建
        SkySunManager.Instance.StartCreateSun(6);
        // 开始显示UI特效
        UIManager.Instance.ShowLVStartEF();
        // 清理掉僵尸
        ZombieManager.Instance.ClearZombie();
        CurrLVState = LVState.Fight;
    }
}