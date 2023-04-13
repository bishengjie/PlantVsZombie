using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private bool isOver;
    // 在刷新僵尸中
    private bool isUpdateZombie;
    // 当前第几天 关卡数
    private int currentLevel;
    // 关卡中的阶段 波数
    private int stageInLV;
    private UnityAction LevelStartAction;
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
                    ZombieManager.Instance.UpdateZombie(5,ZombieType.Zombie);
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
            UIManager.Instance.UpdateStageNum(stageInLV - 1);
            if (stageInLV > 2)
            {
                // 杀掉当前关卡的全部僵尸，就进入下一天
                ZombieManager.Instance.AddAllZombieDeadAction(OnAllZombieDeadAction);
                currentLVState = LVState.Over;

            }

        }
    }

    public int CurrentLevel
    {
        get => currentLevel;
        set
        {
            currentLevel = value;
            StartLV(currentLevel);
        }
    }
    
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentLevel = 1;
    }
    

    //开始关卡
    public void StartLV(int level)
    {
        if (isOver)return;
        currentLevel = level; // 关卡
        UIManager.Instance.UpdateDayNum(currentLevel);
        StageInLV = 1;
        CurrLVState = LVState.Start;
    }

    private void Update()
    {
        if (isOver)return;
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
                    // 意味着是最后一波，需要刷新一个旗帜僵尸
                    if (StageInLV==2)
                    {
                        ZombieManager.Instance.UpdateZombie(1,ZombieType.FlagZombie);
                    }
                    // 僵尸刷新的时间                   时间，数量
                    float updateZombie = Random.Range(15 - stageInLV / 2, 20 - stageInLV / 2);
                    // 僵尸刷新的数量
                    int updateNum = Random.Range(1, 3 + currentLevel);
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
        // 关卡开始时要做的事情
        if (LevelStartAction != null) LevelStartAction();
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
        // 临时测试刷新僵尸
        ZombieManager.Instance.UpdateZombie(zombieNum,ZombieType.BucketheadZombie);
        ZombieManager.Instance.ZombieStartMove();
        isUpdateZombie = false;
        StageInLV += 1;
    }

    // 添加关卡开始事件的监听者
    public void AddLevelStartActionListener(UnityAction action)
    {
        LevelStartAction += action;
    }

    // 当全部僵尸死亡时触发的事件
    private void OnAllZombieDeadAction()
    {
        // 更新天数
        CurrentLevel += 1;
        // 执行一次之后，自己移除委托
        ZombieManager.Instance.RemoveAllZombieDeadAction(OnAllZombieDeadAction);
    }

    // 游戏结束
    public void GameOver()
    {
        // 效果
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.zombieEat);
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.gameOver);
        isOver = true;
        // 逻辑
        SkySunManager.Instance.StopCreateSun();
        ZombieManager.Instance.ClearZombie();
        // UI
        UIManager.Instance.GameOver();
    }
}