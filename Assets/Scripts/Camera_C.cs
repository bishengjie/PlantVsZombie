using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_C : MonoBehaviour
{
    public static Camera_C Instance;
    private void Awake()
    {
        Instance = this;
        transform.position = new Vector3(-3.02f,0.2f,-10f);
    }

    // 关卡开始时的移动
    public void MoveForLVStart()
    {
        StartCoroutine(DoMove(2.83f));
    }

    IEnumerator DoMove(float targetPosX)
    {
        // 获取一个目标
        Vector3 target = new Vector3(targetPosX, transform.position.y, -10f);
        // 获取一个标准长度的方向
        Vector2 direction = (target - transform.position).normalized;
        // 如果距离目标点比较远从，就一直移动
        while (Vector2.Distance(target,transform.position)>0.1)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction*0.1f);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
