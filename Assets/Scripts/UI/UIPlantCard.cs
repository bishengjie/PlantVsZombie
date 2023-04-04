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

    // 是否需要放置植物
    private bool wantPlant;
    // 用来创建的植物
    private GameObject plant;
    // 在网格中的植物，它是透明的
    private GameObject plantInGrid;

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

    public bool WantPlant
    {
        get => wantPlant;
        set
        {
            wantPlant = value;
            if (wantPlant)
            {
                GameObject prefab = PlantManager.Instance.GetPlantForType(PlantType.SunFlower);
                // print(prefab);//null
                plant = Instantiate(prefab, Vector3.zero, Quaternion.identity, PlantManager.Instance.transform);
                plant.GetComponent<SunFlower>().InitForCreate(false);
            }
            else
            {
                Destroy(plant.gameObject);
                plant = null;
            }
        }
    }

    void Start()
    {
        maskImage = transform.Find("Mask").GetComponent<Image>();
        CanPlant = false;
    }
    
    private  void Update()
    {
        // 如果需要放置植物，并且要放置的植物不为空
        if (WantPlant && plant != null)
        {
            // 让植物跟随鼠标
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             // print(mousePoint);
            plant.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);
            
            // 如果距离网格较近，需要在网格上出现一个透明的植物
            if (Vector2.Distance(mousePoint, GridManager.Instance.GetGridPointByMouse()) < 1.5)
            {
                if (plantInGrid == null)
                {
                    plantInGrid = Instantiate(plant, GridManager.Instance.GetGridPointByMouse(), Quaternion.identity, PlantManager.Instance.transform);
                    plantInGrid.GetComponent<SunFlower>().InitForCreate(true);
                }
                else
                {
                    plantInGrid.transform.position = GridManager.Instance.GetGridPointByMouse();
                }
            }
            else
            {
                if (plantInGrid != null)
                {
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                }
            }
        }

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
        if (!WantPlant)
        {
            WantPlant = true;
        }
        print("放置植物");
    }
}
