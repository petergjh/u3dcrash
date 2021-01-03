using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DataMediator : Mediator
{
    //定义名称
    public new const string NAME = "DataMediator";
    //定义两个显示的控件
    private Text uiTxtLevel;                              //显示“等级”
    private Button BtnDisplayLevelNum;                  //点击的按钮
    private Button uiBtnAddLevel;                  //点击的按钮：加
    private Button uiBtnSubLevel;                  //点击的按钮：减


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="goRootNode">界面UI的根结点</param>
    public DataMediator(GameObject goRootNode):base(NAME,goRootNode)
    {
        //确定控件
        //TxtLevel = goRootNode.transform.Find("Txt_DisplayNum").gameObject.GetComponent<Text>();
        uiTxtLevel = goRootNode.transform.Find("TxtLevel").gameObject.GetComponent<Text>();
        //BtnDisplayLevelNum = goRootNode.transform.Find("BtnCount").gameObject.GetComponent<Button>();
        uiBtnAddLevel = goRootNode.transform.Find("BtnAdd").gameObject.GetComponent<Button>();
        uiBtnSubLevel = goRootNode.transform.Find("BtnSub").gameObject.GetComponent<Button>();
        //注册按钮
        //BtnDisplayLevelNum.onClick.AddListener(OnClickAddingLevelNumber);
        uiBtnAddLevel.onClick.AddListener(OnClickAddingLevelNumber);
        uiBtnSubLevel.onClick.AddListener(OnClickSubLevelNumber);
    }

 

    //用户点击事件
    private void OnClickAddingLevelNumber()
    {
        //发送“控制层”的命令消息需要在Facade注册
        SendNotification("Reg_AddNumberCommand");
    }
    private void OnClickSubLevelNumber()
    {
        SendNotification("Reg_SubNumberCommand");
    }

    /// <summary>
    /// 本视图层，允许接收的消息。
    /// </summary>
    /// <returns></returns>
    //public override IList<string> ListNotificationInterests()
    //{
    //    IList<string> listResult = new List<string>();

    //    //可以接收的消息（集合）
    //    listResult.Add("Msg_AddLevel");
    //    return listResult;
    //}
    public override string[] ListNotificationInterests()
    {
        IList<string> listResult = new List<string>();

        //可以接收的消息（集合）
        listResult.Add("Msg_AddLevel");
        listResult.Add("Msg_SubLevel");
        return listResult.ToArray();
    }

    /// <summary>
    /// 处理所有其他类，发给本类允许处理的消息
    /// </summary>
    /// <param name="notification"></param>
    public override void HandleNotification(INotification notification)
    {
        MyData myData = notification.Body as MyData;

        switch (notification.Name)
        {
            case "Msg_AddLevel":
                //把模型层发来的数据，显示给控件。
                uiTxtLevel.text = myData.Level.ToString();
                break;
            case "Msg_SubLevel":
                uiTxtLevel.text = myData.Level.ToString();
                break;
            default:
                break;
        }
    }
}
