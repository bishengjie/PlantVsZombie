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
    private static LVState currentLVState;
    // 在刷新僵尸中
    private bool isUpdateZombie;
    // 当前第几天 关卡数
    private int currentLv;
    // 关卡中的阶段 波数
    private int stageInLV;
    public LVState CurrLVState
    {
        get => currentLVState;
        set
        {
            currentLVState = value;
            switch (currentLVState)
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

    public int StageInLV
    {
        get => stageInLV;
        set
        {
            stageInLV = value;
            if (stageInLV>=3)
            {
                // 更新天数
                GameManager.Instance.CurrentLevel += 1;
                return;
            }
            UIManager.Instance.UpdateStageNum(stageInLV-1);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    //开始关卡
    public void StartLV(int level)
    {
        currentLv = level; // 关卡
        UIManager.Instance.UpdateStageNum(currentLv);
        StageInLV = 1;
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
            case LVState.Start:
                break;
            case LVState.Fight:
                // 刷新僵尸
                //如果没有在刷新僵尸，则刷新僵尸
                if (isUpdateZombie == false)
                {
                    // 僵尸刷新的时间                   时间，数量
                    float updateZombie = Random.Range(15 - stageInLV / 2, 20 - stageInLV / 2);
                    // 僵尸刷新的数量
                    int updateNum = Random.Range(1, 3 + currentLv);
                    UpdateZombie(updateZombie, updateNum);
                }
                break;
            case LVState.Over:
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
        StageInLV += 1;
    }
}