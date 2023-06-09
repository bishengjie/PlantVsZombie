using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 播放特效音乐
    public void PlayEFAudio(AudioClip clip)
    {
        // 从对象池获取一个音效物体
        EFAudio ef = PoolManager.Instance.GetObj(GameManager.Instance.GameConf.EFAudio).GetComponent<EFAudio>();
        ef.Init(clip);
    }

}
