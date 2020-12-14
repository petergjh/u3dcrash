/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BaseMVCFrame
{
	public class TUI : Mediator
	{
        public new const string NAME = "TUI";

        public Text _TestText;

        public Button _Button;

        public TUI()
        {
            _TestText = GameObject.Find("TestText").GetComponent<Text>();
            _Button = GameObject.Find("ChangeButton").GetComponent<Button>();
            _Button.onClick.AddListener(delegate() 
            {
                SendNotification("Reg_Test");
            });
        }

        //本视图层允许接收的方法
        public override IList<string> ListNotificationInterests()
        {
            IList<string> listResult = new List<string>();

            listResult.Add("ChangeTest");

            return listResult;
        }

        //处理所有其他类发过来的消息
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case "ChangeTest":
                    {
                        TData data = notification.Body as TData;
                        _TestText.text = data.TestData;

                        Transform transCanvas = GameObject.Find("Canvas").GetComponent<Transform>();
                        Transform[] Children = transCanvas.GetComponentsInChildren<Transform>();
                        if (Children.Length > 0)
                        {
                            string[] Infos = new string[Children.Length];
                            int Index = 0;
                            foreach (Transform child in Children)
                            {
                                string strType = "";
                                Component comt = null;
                                if (child.TryGetComponent(typeof(Text), out comt))
                                {

                                } else if (child.TryGetComponent(typeof(Image), out comt))
                                {

                                }
                                if (comt != null)
                                {
                                    //Debug.Log("节点类型：" + comt.GetType());
                                    Infos[Index] = child.name + "  " + comt.GetType().ToString().Replace("UnityEngine.UI.","");
                                    Index++;
                                   
                                }
                               
                            }
                            System.IO.File.WriteAllLines(Application.streamingAssetsPath+"/WriteLines.cs", Infos);

                        }
                   
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }
    }
}
