using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sun : MonoBehaviour
{
    // 下落的目标点
    private float downTargetPosY; 
    // 来自天空
    public bool isFormSky;
   
    // Update is called once per frame
    void Update()
    {
        if (!isFormSky)return;
        
        if (transform.position.y <= downTargetPosY)
        {
            Invoke("DestroySun",5);
            return;
        }
        transform.Translate(Vector3.down*Time.deltaTime);
    }

    /// <summary>
    /// 鼠标点击阳光的时候，增加游戏管理器中的阳光数量 并且销毁自身
    /// </summary>
    private void OnMouseDown()
    {
        PlayerManager.Instance.SunNum += 25;
        Vector3 sunNum= Camera.main.ScreenToWorldPoint(UIManager.Instance.GetSunNumTextPos());
        sunNum = new Vector3(sunNum.x, sunNum.y, 0); 
        FlyAnimation(sunNum);
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.sunClick);
    }
    
    // 当阳光从天空中初始化的方法
    public void InitForSky(float downTargetPosY,float createPosX,float CreatePosY)
    {
        this.downTargetPosY = downTargetPosY;
        transform.position = new Vector2(createPosX, CreatePosY);
        isFormSky = true;
    }
    
    //阳光来自太阳花的初始化
    public void InitForSunFlower(Vector2 pos)
    {
        transform.position = pos;
        // 肯定来自太阳花
        isFormSky = false;
        StartCoroutine(DoJump());
    }

    private IEnumerator DoJump()
    {
        bool isLeft = Random.Range(0, 2) == 0;
        Vector3 startPos = transform.position;
        float x;
        if (isLeft)
        {
            x = -0.01f;
        }
        else
        {
            x = 0.01f;
        }

        float speed = 0;
        while (transform.position.y <= startPos.y + 1)
        {
            yield return new WaitForSeconds(0.002f);
            speed += 0.001f;
            transform.Translate(new Vector3(x, 0.002f + speed, 0));
        }

        while (transform.position.y >= startPos.y)
        {
            yield return new WaitForSeconds(0.002f);
            speed += 0.001f;
            transform.Translate(new Vector3(x, -0.002f - speed, 0));
        }
    }

    /// <summary>
    /// 飞行动画
    /// </summary>

    private void FlyAnimation(Vector3 pos)
    {
        StartCoroutine(DoFly(pos));
    }

    private IEnumerator DoFly(Vector3 pos)
    {
        while (Vector3.Distance(pos, transform.position) > 0.05f)
        {
            Vector3 direction = (pos - transform.position).normalized*0.05f;
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction);
        }

        DestroySun();
        print("销毁");
    }

    // 销毁
    private void DestroySun()
    {
        // 取消自身全部协程和延迟调用
        StopAllCoroutines();
        CancelInvoke();
       // 放进缓存池， 不做真实销毁
       PoolManager.Instance.PushObj(GameManager.Instance.GameConf.Sun,gameObject);
    }

}
