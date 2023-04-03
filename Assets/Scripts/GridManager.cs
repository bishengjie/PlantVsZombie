using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
     private List<Vector2> pointList = new List<Vector2>();
     private List<Grid> GridList = new List<Grid>();
    void Start()
    {
        // CreateGridBaseColl();
       // CreateGridBasePointList();
        CreateGridBaseGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print(GetGridPointByMouse());
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
                GridList.Add(new Grid(new Vector2(i,j),transform.position+new Vector3(1.33f*i,1.63f*j,0),false));
                    
            }
        }
    }

    
    // 通过鼠标获取网格坐标点
    public Vector2 GetGridPointByMouse()
    {
        float distance = 1000000;
        Vector2 point = new Vector2();
        for (int i = 0; i < GridList.Count; i++)
        {
            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridList[i].Position) < distance)
            {
                distance=Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), GridList[i].Position);
                point = GridList[i].Position;
            }
        }
        return point;
    }
}
