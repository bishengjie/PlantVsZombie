using UnityEngine;

public enum PlantType
{
     SunFlower,
     Peashooter,
     WallNut,  // 坚果
     Spike, // 地刺
     Cherry
}
public class PlantManager : MonoBehaviour
{
     public static PlantManager Instance;

     private void Awake()
     {
          Instance = this;
     }

     public GameObject GetPlantByType(PlantType type)
     {
          switch (type)
          {
               case PlantType.SunFlower:
                    return GameManager.Instance.GameConf.SunFlower;
               case PlantType.Peashooter:
                    return GameManager.Instance.GameConf.Peashooter;
               case PlantType.WallNut:
                    return GameManager.Instance.GameConf.WallNut;
               case PlantType.Spike:
                    return GameManager.Instance.GameConf.Spike; 
               case PlantType.Cherry:
                    return GameManager.Instance.GameConf.Cherry;
                    
          }
          return null;
     }
}
