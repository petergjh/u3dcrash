using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonWithParam : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        ButtonListen(0);
        
    }

    private void ButtonListen(int testNum)
    {
        myButton.onClick.AddListener(() =>
        {
            Debug.Log("测试带参的button事件 param = :" + testNum);
        }
        );
    }    

}
