using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    private List<Vector2> pointList = new List<Vector2>();
    private List<Grid> GridList = new List<Grid>();

    private void Awake()
    {
        Instance = this;
        CreateGridBaseGrid();
    }

    void Start()
    {
        // CreateGridBaseColl();
        // CreateGridBasePointList();
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //  print(GetGridPointByMouse());
        }
    }

    // 基于碰撞的形式创建网格
    private void CreateGridBaseColl()
    {
        // 创建一个预制体网格
        GameObject prefabGrid = new GameObject();
        prefabGrid.AddComponent<BoxCollider2D>().size = new Vector2(1, 1.5f);
        prefabGrid.transform.SetParent(transform);
        prefabGrid.transform.position = transform.position;
        prefabGrid.name = 0 + "-" + 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject grid = Instantiate(prefabGrid, transform.position + new Vector3(1.33f * i, 1.63f * j, 0),
                    Quaternion.identity, transform);
                grid.name = i + "-" + j;

            }
        }
    }

    // 基于坐标List的形式创建网格
    private void CreateGridBasePointList()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                pointList.Add(transform.position + new Vector3(1.33f * i, 1.63f * j, 0));
            }
        }
    }

    // 基于脚本的形式创建网格
    private void CreateGridBaseGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GridList.Add(new Grid(new Vector2(i, j), transform.position + new Vector3(1.33f * i, 1.63f * j, 0),
                    false));

            }
        }
    }


    // 通过鼠标获取网格坐标点
    public Vector2 GetGridPointByMouse()
    {
        return GetGridPointByWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    // 通过世界坐标来获取一个网格坐标点
    public Vector2 GetGridPointByWorldPos(Vector2 worldPos)
    {
       return GetGridByWorldPos(worldPos).Position;
    }
    
    public Grid GetGridByWorldPos(Vector2 worldPos)
    {
        float distance = 1000000;
        Grid grid= null;
        for (int i = 0; i < GridList.Count; i++)
        {
            if (Vector2.Distance(worldPos, GridList[i].Position) < distance)
            {
                distance = Vector2.Distance(worldPos, GridList[i].Position);
                grid = GridList[i];
            }
        }
        return grid;
    }

    public Grid GetGridByMouse()
    {
        return GetGridByWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    
    ///<summary>
    ///通过Y轴来寻找一个网格，从下往上 0开始
    /// </summary>>
    /// <param name="verticalNum"></param>
    /// <returns></returns>
    public Grid GetGridByVerticalNum(int verticalNum)
    {
        for (int i = 0; i < GridList.Count; i++)
        {
            if (GridList[i].Point == new Vector2(8, verticalNum))
            {
                return GridList[i];
            }
        }
        return null;
    }
}
