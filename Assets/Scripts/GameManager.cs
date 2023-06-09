using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameConf GameConf;//config配置

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Application.targetFrameRate = 60;
            GameConf = Resources.Load<GameConf>("GameConf");
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
