using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void GoEndLess()
    {
        // 清理对象池
        PoolManager.Instance.Clear();
        // 播放音效
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.buttonClick);
        Invoke("DoGoEndLess",0.5f);
       
    }

    private void DoGoEndLess()
    {
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.buttonClick);
        SceneManager.LoadScene("EndLess");
    }
    
    public void Quit()
    {
        Application.Quit(); //应用 退出
    }
}
