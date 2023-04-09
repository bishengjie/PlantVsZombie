using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPanel : MonoBehaviour
{
    public void Show(bool isShow)
    {
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
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        Application.Quit(); //应用 退出
    }
}
