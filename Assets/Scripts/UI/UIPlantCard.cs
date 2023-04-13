using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 卡片的四种状态
public enum CardState
{
    // 有阳光有CD
    CanPlant,
    // 有阳光没有CD
    NotCD,
    // 没有阳光有CD
    NotSum,
    // 都没有
    NotAll
}

public class UIPlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // 遮罩图片的img组件
    private Image maskImage;

    // 自身的img组件
    private Image image;

    // 需要阳光数量的text
    private Text wantSunText;

    // 种植需要多少阳光
    public int WantSunNum;

    // 冷却时间：几秒可以放置一次植物
    public float CDTime;

    // 冷却时间：用于冷却时间的计算
    private float currTimeForCard;

    // 是否可以放置植物的CD
    private bool canPlant;
    
    // 植物的预制体
    private GameObject prefab;

    // 是否需要放置植物
    private bool wantPlant;

    // 用来创建的植物
    private PlantBase plant;

    // 在网格中的植物，它是透明的
    private PlantBase plantInGrid;

    // 当前卡片所对应的植物类型
    public PlantType CardPlantType;

    private CardState cardState = CardState.NotAll;

    public CardState CardState
    {
        get => cardState;
        set
        {
            // 如果要修改成为的值和当前值一样， 就跳出，不需要运行任何逻辑
            if (cardState == value)
            {
                return;
            }

            switch (value)
            {
                case CardState.CanPlant:
                    // CD没有遮罩 自身是明亮的
                    maskImage.fillAmount = 0;
                    image.color = Color.white;
                    break;
                case CardState.NotCD:
                    // CD有遮罩 自身是明亮的
                    image.color = Color.white;
                    if (cardState == CardState.NotAll) return;
                    CDEnter();
                    break;
                case CardState.NotSum:
                    // CD没有遮罩 自身是昏暗的
                    maskImage.fillAmount = 0;
                    image.color = new Color(0.75f, 0.75f, 0.75f);
                    break;
                case CardState.NotAll:
                    image.color = new Color(0.75f, 0.75f, 0.75f);
                    if (cardState == CardState.NotCD) return;
                    CDEnter();
                    break;
            }
            cardState = value;
        }
    }

    public bool CanPlant
    {
        get => canPlant;
        set
        {
            canPlant = value;
            CheckState();
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
                prefab = PlantManager.Instance.GetPlantByType(CardPlantType);
                plant = PoolManager.Instance.GetObj(prefab).GetComponent<PlantBase>();
                plant.transform.SetParent( PlantManager.Instance.transform);
                plant.InitForCreate(false,CardPlantType,Vector2.zero);
               // UIManager.Instance.CurrCard = this;
            }
            else
            {
                if (plant != null)
                {
                    plant.Dead();
                    plant = null;
                }
            }
        }
    }

    void Start()
    {
        maskImage = transform.Find("Mask").GetComponent<Image>();
        wantSunText = transform.Find("Text ").GetComponent<Text>();
        wantSunText.text = WantSunNum.ToString();
        image = GetComponent<Image>();
        CanPlant = true;
        PlayerManager.Instance.AddSunNumUpdateActionListener(CheckState);
        LVManager.Instance.AddLevelStartActionListener(OnLevelStartAction);
    }

    private void Update()
    {
        // 如果需要放置植物，并且要放置的植物不为空
        if (WantPlant && plant != null)
        {
            // 让植物跟随鼠标
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Grid grid = GridManager.Instance.GetGridByWorldPos(mousePoint);

            plant.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);

            // 如果距离网格较近，并且没有植物，需要在网格上出现一个透明的植物
            if (grid.HavePlant == false && Vector2.Distance(mousePoint, grid.Position) < 1.5)
            {
                if (plantInGrid == null)
                {
                    plantInGrid = PoolManager.Instance.GetObj(prefab).GetComponent<PlantBase>();
                    plantInGrid.transform.SetParent(PlantManager.Instance.transform);
                    plantInGrid.InitForCreate(true, CardPlantType, grid.Position);
                }
                else
                {
                    plantInGrid.UpdateForCreate(grid.Position);
                    
                }

                // 如果点击鼠标，需要放置植物
                if (Input.GetMouseButtonDown(0)) 
                {
                    plant.transform.position = grid.Position;
                    plant.InitForPlace(grid,CardPlantType);
                    plant = null;
                    plantInGrid.Dead();
                    plantInGrid = null;
                    WantPlant = false;
                    CanPlant = false;
                    AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.place);
                    // 种植成功需要减少玩家的阳光
                    PlayerManager.Instance.SunNum -= WantSunNum;
                }
            }
            else
            {
                if (plantInGrid != null)
                {
                    plantInGrid.Dead();
                    plantInGrid = null;
                }
            }
        }

        // 如果右键取消放置状态
        if (Input.GetMouseButtonDown(1)) 
        {
            CancelPlace();
            
        }
    }
    // 取消放置
    private void CancelPlace()
    {
        if (plant != null) plantInGrid.Dead();
        if (plantInGrid != null) plantInGrid.Dead();
        plant = null;
        plantInGrid = null;
        WantPlant = false;
    }
    // 在关卡开始时需要做的事情
    private void OnLevelStartAction()
    {
        CancelPlace();
        // 重置CD
        canPlant = true;
    }

    // 状态检测
    private void CheckState()
    {
        // 有阳光 有CD
        if (canPlant && PlayerManager.Instance.SunNum >= WantSunNum)
        {
            CardState = CardState.CanPlant;
        }
        // 有阳光 没有CD
        else if (!canPlant && PlayerManager.Instance.SunNum >= WantSunNum)
        {
            CardState = CardState.NotCD;
        }
        // 没有阳光 有CD
        else if (canPlant && PlayerManager.Instance.SunNum < WantSunNum)
        {
            CardState = CardState.NotSum;
        }
        // 都没有
        else
        {
            CardState = CardState.NotAll;
        }
    }

    // 进入CD
    private void CDEnter()
    {
        // 完全遮罩来表示不可以种植
        maskImage.fillAmount = 1;
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
        if (CardState != CardState.CanPlant) return;
        transform.localScale = new Vector2(1.05f, 1.05f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CardState != CardState.CanPlant) return;
        transform.localScale = new Vector2(1f, 1f);
    }

    // 鼠标点击时的效果，放置植物
    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardState != CardState.CanPlant) return;
        AudioManager.Instance.PlayEFAudio(GameManager.Instance.GameConf.buttonClick);
        if (!WantPlant)
        {
            WantPlant = true;
        }
        print("放置植物");
    }
}
