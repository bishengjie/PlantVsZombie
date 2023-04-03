using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateSun",3,3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 创建阳光
    private void CreateSun()
    {
        Sun sun = Instantiate(GameManager.Instance.Prefab_sun, transform.position, Quaternion.identity, transform)
            .GetComponent<Sun>();
        // 让阳光进行跳跃动画
        sun.JumpAnimation();
    }
}
