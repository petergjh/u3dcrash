using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDelegate : MonoBehaviour
{
    //定义委托类
    public delegate void MyDelegateClass();

    //实例化委托类，即声明一个委托类型的变量
    public MyDelegateClass myDelegateTest;

    //声明一个将来要调用的普通方法，注意必须跟委托的参数和返回值签名相同
    //private void GoTest()
    //{
    //    Debug.Log("111");
    //}

    private void Start()
    {
        //把普通方法赋值给委托变量
        //myDelegateTest = GoTest;
        myDelegateTest = () =>
        {
            Debug.Log("111");
        }; 

        //调用存有方法的委托变量，也就等同与调用了前面定义的那个普通方法
        myDelegateTest();
    }
}
