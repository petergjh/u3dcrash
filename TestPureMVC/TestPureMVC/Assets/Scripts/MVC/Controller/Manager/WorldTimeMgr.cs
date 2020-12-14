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
using UnityEngine;
using BaseMVCFrame;
using UnityEngine.UI;

namespace BaseMVCFrame.Manager
{
	public class WorldTimeMgr : BaseManager<WorldTimeMgr>
	{
        private TimeGod m_TimeGod;
        /// <summary>
        /// 世界运行的分钟
        /// </summary>
        public int World_Minutes
        {
            get
            {
                return m_TimeGod.m_WorldMinutes;
            }
        }
        /// <summary>
        /// 世界运行的小时
        /// </summary>
        public int World_Hours
        {
            get
            {
                return m_TimeGod.m_WorldHours;
            }
        }
        /// <summary>
        /// 世界运行的天数
        /// </summary>
        public int World_Days
        {
            get
            {
                return m_TimeGod.m_WorldDays;
            }
        }
        /// <summary>
        /// 世界运行的月数
        /// </summary>
        public int World_Months
        {
            get
            {
                return m_TimeGod.m_WorldMonths;
            }
        }
        /// <summary>
        /// 世界运行的年数
        /// </summary>
        public int World_Years
        {
            get
            {
                return m_TimeGod.m_WorldYears;
            }
        }


        protected override void Initialization()
        {
            m_TimeGod = new TimeGod();
        }


        public void TheWorldTimeStart()
        {
            IEManager.m_Instance.StartIE(TimeStartIE());
        }

        IEnumerator TimeStartIE()
        {
            Text TimeTExt = GameObject.Find("TimeText").GetComponent<Text>();
            while (true)
            {
                m_TimeGod.UpdateWorldTime();

                TimeTExt.text = World_Years + "年 " + World_Months + "月 " + World_Days + "天 " + World_Hours + "时 " + World_Minutes+  "分 ";
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
