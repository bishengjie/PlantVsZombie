using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIPlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // 遮罩图片的img组件
    private Image maskImage;

    // 冷却时间：几秒可以放置一次植物
    public float CDTime;

    // 冷却时间：用于冷却时间的计算
    private float currTimeForCard;

    // 是否可以放置植物
    private bool canPlant;

    public bool CanPlant
    {
        get => canPlant;
        set
        {
            canPlant = value;
            // 如果不能放置
            if (!canPlant)
            {
                // 完全遮罩来表示不可以种植
                maskImage.fillAmount = 1;
                // 开始冷却
                CDEnter();
            }
            else
            {
                maskImage.fillAmount = 0;
            }
        }
    }

    void Start()
    {
        maskImage = transform.Find("Mask").GetComponent<Image>();
        CanPlant = false;
    }

    // 进入CD
    private void CDEnter()
    {
        // 遮罩后，开始计算冷却
        StartCoroutine(CalCD());
    }

    // 计算冷却时间
    IEnumerator CalCD()
    {
        float calCD = (1 / CDTime) * 0.1f;
        currTimeForCard = CDTime;
        while (currTimeForCard >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            maskImage.fillAmount -= calCD;
            currTimeForCard -= 0.1f;
        }

        // 到这里，意味着冷却时间到了，可以放置了
        CanPlant = true;
    }

    void Update()
    {

    }

    // 鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CanPlant) return;
        transform.localScale = new Vector2(1.05f, 1.05f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CanPlant) return;
        transform.localScale = new Vector2(1f, 1f);
    }

    // 鼠标点击时的效果，放置植物
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanPlant) return;
        print("放置植物");
    }
}
