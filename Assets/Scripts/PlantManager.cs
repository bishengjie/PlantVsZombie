using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlantType
{
     SunFlower,
     Peashooter
}
public class PlantManager : MonoBehaviour
{
     public static PlantManager Instance;

     private void Awake()
     {
          Instance = this;
     }

     public GameObject GetPlantForType(PlantType type)
     {
          switch (type)
          {
               case PlantType.SunFlower:
                    return GameManager.Instance.GameConf.SunFlower;
               case PlantType.Peashooter:
                    return GameManager.Instance.GameConf.Peashooter;
                    
          }
          return null;
     }
}
