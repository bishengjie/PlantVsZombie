using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


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
    // 在刷新僵尸中
    private bool isUpdateZombie;
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
                    // 20秒后重新刷新僵尸
                    UpdateZombie(20,1);
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

    private void Update()
    {
        FSM();
    }

    public void FSM()
    {
        switch (CurrLVState)
        {
            case  LVState.Start:
                break;
            case  LVState.Fight:
                // 刷新僵尸
                //如果没有在刷新僵尸，则刷新僵尸
                if (isUpdateZombie==false)
                {
                    //                                时间，数量
                    UpdateZombie(Random.Range(10,20),Random.Range(1,3));
                }
                break;
            case  LVState.Over:
                break;
        }
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

    //更新僵尸
    private void UpdateZombie(float delay,int zombieNum)
    {
        StartCoroutine(DoUpdateZombie(delay, zombieNum));
    }
    IEnumerator DoUpdateZombie(float delay,int zombieNum)
    {
        isUpdateZombie = true;
        yield return new WaitForSeconds(delay);
        ZombieManager.Instance.UpdateZombie(zombieNum);
        ZombieManager.Instance.ZombieStartMove();
        isUpdateZombie = false;
    }
}