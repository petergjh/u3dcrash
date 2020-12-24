using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerV1 : MonoBehaviour
{
    public Button uiBtnAdd;
    public Text uiTxtNumber;
    private int NumberCount;
    public Button uiBtnSub;

    // Start is called before the first frame update
    void Start()
    {
        ButtonListen();
    }

    private void ButtonListen()
    {
        uiBtnAdd.onClick.AddListener(()=>
        {
            NumberCount++;
            uiTxtNumber.text = NumberCount.ToString(); 
        });

        uiBtnSub.onClick.AddListener(()=>
        {
            NumberCount--;
            uiTxtNumber.text = NumberCount.ToString();
        });
    }
}
