using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Camera_C : MonoBehaviour
{
    public static Camera_C Instance;

    private void Awake()
    {
        Instance = this;
        transform.position = new Vector3(-3, 0, -10);
    }

    // 开始移动
    public void StartMove(UnityAction action)
    {
        //一开始往右，然后回归到终点时调用传进来的委托方法
        MoveForLVStart(() => MoveForLVStartBack(action));
    }

    // 关卡开始时的移动
    private void MoveForLVStart(UnityAction action)
    {
        StartCoroutine(DoMove(2.83f, action));
    }

    // 关卡开始时的摄像机回归
    private void MoveForLVStartBack(UnityAction action)
    {
        StartCoroutine(DoMove(-3, action));
    }

    IEnumerator DoMove(float targetPosX, UnityAction action)
    {
        // 获取一个目标
        Vector3 target = new Vector3(targetPosX, transform.position.y, -10f);
        // 获取一个标准长度的方向
        Vector2 direction = (target - transform.position).normalized;
        // 如果距离目标点比较远从，就一直移动
        while (Vector2.Distance(target, transform.position) > 0.1)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction * 0.1f);
        }
        // 如果到了这个位置，意味着相机已经到了最右边
        yield return new WaitForSeconds(1.5f);
        if (action != null) action();
    }
}
