using System; //注意 Action是系统内置的类型
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{
    //声明一个Action类型的变量
    public Action myAction;

    //一个普通方法
    private void GoTest()
    {
        Debug.Log("222");
    }

    void Start()
    {
        //把普通方法赋值给前面定义好的Action变量
        myAction = GoTest;
        //调用这个存有方法的变量，也即调用了这个普通方法
        myAction();
    }
}


