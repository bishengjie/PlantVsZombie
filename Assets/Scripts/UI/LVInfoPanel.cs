using UnityEngine;
using UnityEngine.UI;

public class LVInfoPanel : MonoBehaviour
{
    // 天数
    private Text dayNumText;

    // 波数
    private Text stageNumText;
    
    void Awake()
    {
        dayNumText = transform.Find("DayNumText").GetComponent<Text>();
        stageNumText = transform.Find("StageNumText").GetComponent<Text>();
    }


    public void UpdateDayNum(int day)
    {
        dayNumText.text = "当前第" + day + "天";
    }

    public void UpdateStageNum(int stage)
    {
        if (stage == 0)
        {
            stageNumText.text = "僵尸即将来临！";
            return;
        }

        stageNumText.text = "第" + stage + "波僵尸来了";
    }
}