/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
namespace BaseMVCFrame.Manager
{
    public class Ctrl_ReStartGameCommond : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            //主角重新运行
            //GameObject.FindGameObjectWithTag(ProjectConsts.Player).GetComponent<Ctrl_HeroControl>().StartGame();
            //得到层级视图根节点。
            GameObject goEnviromentRoot = GameObject.Find(ProjectConsts.MainGameScenes);
            //管道组启动运行
            //UnityHelper.FindTheChildNode(goEnviromentRoot, "GamePipes").GetComponent<Ctrl_PipesMoving>().StartGame();
            //获取时间脚本
            //goEnviromentRoot.GetComponent<Ctrl_GetTime>().StartGame();
            //“金币”道具启动
            for (int i = 1; i <= 3; i++)
            {
                //道具开始运行               
                //UnityHelper.FindTheChildNode(goEnviromentRoot, "Pipe" + i + "_Trigger").GetComponent<Ctrl_Golds>().StartGame();
            }
        }
    }
}