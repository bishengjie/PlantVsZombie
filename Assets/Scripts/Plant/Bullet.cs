using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    // 攻击力
    private int attackValue;
    // 是否击中
    private bool isHit = false;
    public void Init(int attackValue)
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody.AddForce(Vector2.right * 300);
        this.attackValue = attackValue;

    }


    void Update()
    {
        if(isHit)return;
        transform.Rotate(new Vector3(0, 0, -15f));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(isHit)return;
        if (col.tag == "Zombie")
        {
            isHit = true;
            // 让僵尸受伤
            col.GetComponentInParent<Zombie>().Hurt(attackValue);
            // 修改成击中的图片
            spriteRenderer.sprite = GameManager.Instance.GameConf.Bullet1Hit;
            // 暂停自身的运动
            rigidbody.velocity = Vector2.zero;
            // 下落
            rigidbody.gravityScale = 1;
            // 销毁自身
            Invoke("Destroy", 0.5f);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
