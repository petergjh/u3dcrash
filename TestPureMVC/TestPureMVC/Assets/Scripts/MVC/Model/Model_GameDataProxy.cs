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
using PureMVC.Patterns;
using UnityEngine;
using PureMVC.Interfaces;

namespace BaseMVCFrame.Manager
{
	public class Model_GameDataProxy : Proxy
	{
		//类的名称
		public new const string NAME = "Model_GameDataProxy";
		//游戏数据实体类
		//private Model_GameData _GameData;
		//测试
		private int intTestNumber = 0;



		public Model_GameDataProxy() : base(NAME)
		{
			//Log.Write("代理类 Model_GameDataProxy GetHashCode=" + this.GetHashCode());
			//_GameData = new Model_GameData();
			//得到最高分数
			//_GameData.HightestScores = PlayerPrefs.GetInt(ProjectConsts.GameHighestScores);
		}

		//增加游戏的时间
		public void AddGameTime()
		{
			//intTestNumber++;
			//Log.Write("代理类 intTestNumber=" + intTestNumber);

			//++_GameData.GameTime;
			//数值发送到视图层。
			//SendNotification(ProjectConsts.Msg_DisplayGameInfo, _GameData);
		}

		/// <summary>
		/// 重置游戏时间
		/// </summary>
		public void ResetGameTime()
		{
			//_GameData.GameTime = 0;
		}

		//增加分数
		public void AddScores()
		{
			//++_GameData.Scores;
			//更新最高分数
			//GetHightestScores();
		}

		//得到最高分数
		//public int GetHightestScores()
		//{
            //if (_GameData.Scores > _GameData.HightestScores)
            //{
            //	_GameData.HightestScores = _GameData.Scores;
            //}
            //return _GameData.HightestScores;
        //}

		//保存最高分数
		public void SaveHightestScores()
		{
			//if (_GameData.HightestScores > PlayerPrefs.GetInt(ProjectConsts.GameHighestScores))
			//{
			//	PlayerPrefs.SetInt(ProjectConsts.GameHighestScores, _GameData.HightestScores);
			//}
		}

	}
}