using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPanel : MonoBehaviour
{
    public void Show(bool isShow)
    {
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.buttonClick);
        gameObject.SetActive(isShow);
        // 如果显示出来，意味着游戏暂停
        if (isShow)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void BackMainScene()
    {
        // 清理对象池
        PoolManager.Instance.Clear();
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.buttonClick);
        Time.timeScale = 1;
        Invoke("DoBackMainScene",0.5f);
    }

    private void DoBackMainScene()
    {
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.buttonClick);
        Application.Quit(); //应用 退出
    }
}
