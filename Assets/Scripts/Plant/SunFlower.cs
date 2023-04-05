using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : PlantBase
{
    // 创建阳光需要的时间
    private float createSunTime = 5;
    // 变成金色需要的时间
    private float goldWantTime = 1;
    
    protected override void OnInitForPlace()
    {
        InvokeRepeating("CreateSun", createSunTime, createSunTime);
    }
    // 创建阳光
    private void CreateSun()
    {
        StartCoroutine(DoCreateSun());
    }

    IEnumerator DoCreateSun()
    {
        float currentTime = 0;
        currentTime += 0.8f;
        float lerp;
        while (currentTime<goldWantTime)
        {
            yield return new WaitForSeconds(0.8f);
            lerp = currentTime / goldWantTime;
            currentTime += 0.8f;
            spriteRenderer.color = Color.Lerp(Color.white, new Color(1, 0.6f, 0), lerp);

        }
        spriteRenderer.color=Color.white;
        Sun sun = Instantiate(GameManager.Instance.GameConf.Sun, transform.position, Quaternion.identity, transform)
            .GetComponent<Sun>();
        // 让阳光进行跳跃动画
        sun.JumpAnimation();
    }
}
