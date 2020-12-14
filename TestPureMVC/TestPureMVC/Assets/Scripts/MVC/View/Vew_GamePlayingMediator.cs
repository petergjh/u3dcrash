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
using System.IO;
using System.Net.Mime;
using Boo.Lang;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace BaseMVCFrame.Manager
{
    public class Vew_GamePlayingMediator : Mediator
    {
        public new const string NAME = "Vew_GamePlayingMediator";
        //控件定义
        private Text _TxtGameTime;                          //游戏时间
        private Text _TxtShowGameTime;                      //显示游戏时间
        private Text _TxtGameScore;                         //游戏分数
        private Text _TxtShowGameScore;                     //显示最高分数
        private Text _TxtGameHighestScore;                  //游戏最高分数
        private Text _TxtShowGameHighestScore;              //显示游戏最高分数


        /// <summary>
        /// 构造函数
        /// </summary>
	    public Vew_GamePlayingMediator() : base(NAME)
        {
            //Log.Write("Mediator hashcode=" + this.GetHashCode());
        }


        /// <summary>
        /// 初始化字段
        /// </summary>
	    private void InitMediatorFiled()
        {
            //得到层级视图根节点
            GameObject goRootCanvase = GameObject.Find("Canvas(Clone)");
            //得到文字控件
            //_TxtGameTime = UnityHelper.GetTheChildNodeComponetScripts<Text>(goRootCanvase, "TxtTime");
            //_TxtGameScore = UnityHelper.GetTheChildNodeComponetScripts<Text>(goRootCanvase, "TxtScores");
            //_TxtGameHighestScore = UnityHelper.GetTheChildNodeComponetScripts<Text>(goRootCanvase, "TxtHighScores");
            _TxtGameTime.text = "时间:";
            _TxtGameScore.text = "分数:";
            _TxtGameHighestScore.text = "最高:";
            //得到文字控件数值显示
            //_TxtShowGameTime = UnityHelper.GetTheChildNodeComponetScripts<Text>(goRootCanvase, "TxtTimeShow");
            //_TxtShowGameScore = UnityHelper.GetTheChildNodeComponetScripts<Text>(goRootCanvase, "TxtScoresShow");
            //_TxtShowGameHighestScore = UnityHelper.GetTheChildNodeComponetScripts<Text>(goRootCanvase, "TxtHighScoresShow");

        }


        /// <summary>
        /// 允许可以接收的消息列表
        /// </summary>
        /// <returns></returns>
        public override IList<string> ListNotificationInterests()
        {
            IList<string> liResult = new System.Collections.Generic.List<string>();
            liResult.Add(ProjectConsts.Msg_DisplayGameInfo);
            liResult.Add(ProjectConsts.Msg_InitGamePlayingMediatorFiled);
            return liResult;
        }


        /// <summary>
        /// 处理接收到的消息列表
        /// </summary>
        /// <param name="notification"></param>
	    public override void HandleNotification(INotification notification)
        {
            //Model_GameData gameData = null;

            switch (notification.Name)
            {
                case ProjectConsts.Msg_InitGamePlayingMediatorFiled:
                    InitMediatorFiled();
                    break;

                case ProjectConsts.Msg_DisplayGameInfo:
                    //gameData = notification.Body as Model_GameData;
                    //if (gameData != null)
                    //{
                    //    if (_TxtShowGameTime && _TxtShowGameScore && _TxtShowGameHighestScore)
                    //    {
                    //        _TxtShowGameTime.text = gameData.GameTime.ToString();
                    //        _TxtShowGameScore.text = gameData.Scores.ToString();
                    //        _TxtShowGameHighestScore.text = gameData.HightestScores.ToString();
                    //    }
                    //}
                    break;
                default:
                    break;
            }
        }
    }
}