/***
 * 
 *    Title: 
 *           主题： 本项目所有常量定义
 *    Description: 
 *           功能：
 *                  
 *    Date: 2020
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame
{
	public class ProjectConsts {
        /* PureMVC 通信常量 */
        //命令通知
        public const string Reg_StartGameCommand = "Reg_StartGameCommand";
        public const string Reg_EndGameCommand = "Reg_EndGameCommand";
        //消息通知
        public const string Msg_DisplayGameInfo = "Msg_DisplayGameInfo";
	    public const string Msg_InitGamePlayingMediatorFiled = "Msg_InitGamePlayingMediatorFiled";
       
        /* UI窗体预设名称 */
        public const string GameStartUI= "GameStartUI";
        public const string GameGuideUIForm = "GameGuideUIForm";
        public const string GamePlayingUIForm = "GamePlayingUIForm";
        public const string TempleUI = "TempleUI";

        /* 项目常用Tag */
        public const string Player = "Player";

        /* 项目 PlayerPrefs 做持久化的常量 */
        public const string GameHighestScores = "GameHighestScores";

        /* 其他常量 */
        public const string MainGameScenes = "MainGameScenes";
        public const string Fire1 = "Fire1";		
	}
}