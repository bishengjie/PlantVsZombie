using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 网格
/// </summary>
public class Grid 
{
    // 坐标点,(0,1)(1,1)
    public Vector2 Point;

    /// 世界坐标
    public Vector2 Position;
    
    // 是否有植物，如果有不能在这个点上创建植物
    public bool HavePlant;

    public Grid(Vector2 point, Vector2 position, bool havePlant)
    {
        Point = point;
        Position = position;
        HavePlant = havePlant;
    }
}
