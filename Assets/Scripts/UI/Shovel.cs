using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shovel : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Transform _shovelImage;
    // 是否在使铲子中//shove随便放置，乱放（某物）；<非正式>移动身体，挪动身体（以腾出地方）
    private bool _isShove;
    
    public bool IsShove
    {
        get { return _isShove; }
        set { _isShove = value;
            // 需要铲植物
            if (_isShove)
            {
                _shovelImage.localRotation=Quaternion.Euler(0,0,45);
            }
            // 把铲子放回去
            else
            {
                _shovelImage.localRotation=Quaternion.Euler(0,0,0);
                _shovelImage.transform.position = transform.position;
            }
        }
    }
    void Start()
    {
        _shovelImage = transform.Find("Image");
    }

   
    void Update()
    {
        // 如果需要铲植物
        if (IsShove)
        {
            _shovelImage.position = Input.mousePosition;
            // 点击左键，判断是否要铲除植物
            if (Input.GetMouseButtonDown(0))
            {
                Grid grid = GridManager.Instance.GetGridByMouse();
                //print(grid);
                // 如果没有植物，直接跳过所有逻辑
                if (grid.CurrPlantBase == null) return;
                // print(grid.CurrPlantBase == null);
                // 如果鼠标距离网格的距离小于1.5m,则杀死这个植物
                if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition),grid.CurrPlantBase.transform.position)<1.5f) 
                {
                    grid.CurrPlantBase.Dead();
                    IsShove = false;
                    print("杀死植物");
                }
            }

            // 点击右键，取消铲子状态
            if (Input.GetMouseButtonDown(1))
            {
                IsShove = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsShove==false)
        {
            IsShove = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _shovelImage.transform.localScale = new Vector2(1.4f, 1.4f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _shovelImage.transform.localScale = new Vector2(1f, 1f);
    }
}
