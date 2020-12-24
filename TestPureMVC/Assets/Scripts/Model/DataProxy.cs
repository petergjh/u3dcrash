using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProxy : Proxy
{
    //声明本类的名称
    public new const string NAME = "DataProxy";
    //引用“实体类”
    private MyData _MyData = null;


    /// <summary>
    /// 构造函数,构建数据模型的一个实例，对数据(模型)进行各种操作
    /// </summary>
    public DataProxy() : base(NAME)
    {
        _MyData = new MyData();
    }

    /// <summary>
    /// 增加玩家的等级
    /// </summary>
    /// <param name="addNumber">增加的等级数量</param>
    public void AddLevel(int addNumber)
    {
        //把参数累加到“实体类”中
        _MyData.Level += addNumber;
        //把变化了的数据，发送给“视图层”
        SendNotification("Msg_AddLevel", _MyData);
    }

    public void SubLevel(int subNumber)
    {
        _MyData.Level -= subNumber;
        SendNotification("Msg_SubLevel",_MyData);
    }

}
