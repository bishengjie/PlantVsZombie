using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    private static PoolManager instance;

    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PoolManager();
            }
            return instance;
        }
    }

    private GameObject poolObj;
   
   // Key是预制体
   // Value是具体的Obj
   private Dictionary<GameObject, List<GameObject>> poolDataDic = new();
   
   // 获取物体
   public GameObject GetObj(GameObject prefab)
   {
       GameObject obj = null;
       // 如果缓存池数据子典中有这个预制体的资源并且这种资源还有
       if (poolDataDic.ContainsKey(prefab) && poolDataDic[prefab].Count > 0)
       {
           // 返回list中的第一个
           obj = poolDataDic[prefab][0];
           // 从list中移除第一个
           poolDataDic[prefab].RemoveAt(0);
       }
       // 没有这种资源
       else
       {
           // 实例化一个，然后传过去
           obj = Object.Instantiate(prefab);
       }
       // 传出去之前，让它显示
       obj.SetActive(true);
       // 让其没有父物体
       obj.transform.SetParent(null);
       return obj;
   }
   
   //把物体放进缓存池
   public void PushObj(GameObject prefab, GameObject obj)
   {
       // 判断有没有根目录
       if (poolObj == null) 
       {
           poolObj = new GameObject("PoolObj");
       }
       // 判断字典中没有这个预制体的数据
       if (poolDataDic.ContainsKey(prefab))
       {
           // 把物体放进去
           poolDataDic[prefab].Add(obj);
       }
       //字典中没有
       else
       {

           // 创建这个预制体的缓存池数据 1+2+3=5  1+2+4=5  
           // List<GameObject> pool = new(){obj}; //1
           //  pool.Add(obj); //2
           // poolDataDic[prefab] = pool; // 3
           // poolDataDic.Add(prefab,pool); //4
           //  prefab: [obj]
           //     key : value
           poolDataDic.Add(prefab, new List<GameObject> { obj });//5= poolDataDic.Add(prefab, new List<GameObject>{obj});
       }

       //如果根目录下没有这个预制体命名的子物体
       if (poolObj.transform.Find(prefab.name) == false)
       {
           new GameObject(prefab.name).transform.SetParent(poolObj.transform);
       }

       // 隐藏
       obj.SetActive(false);
       // 设置父物体
       obj.transform.SetParent(poolObj.transform.Find(prefab.name));
   }
   
   // 清除所有数据
   public void Clear()
   {
       poolDataDic.Clear();
   }
}
