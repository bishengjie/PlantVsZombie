using UnityEngine;

public class LVStartEF : MonoBehaviour
{
    private Animator animator;
        
    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // 获取当前的播放时间
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
        {
           gameObject.SetActive(false);
        }
    }

    // 显示自身
    public void Show()
    {
        gameObject.SetActive(true);
        animator.Play("LVStartEF",0,0);
    }
}
