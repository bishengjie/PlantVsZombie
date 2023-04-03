using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text SunNumText;
  
    private void Awake()
    {
        Instance = this;
        //SunNumText = transform.Find("MainPanel/SunNumText").GetComponent<Text>();
    }

    void Start()
    {
       
    }


    public void UpdateSunNum(int num)
    {
        SunNumText.text = num.ToString();
    }
}
