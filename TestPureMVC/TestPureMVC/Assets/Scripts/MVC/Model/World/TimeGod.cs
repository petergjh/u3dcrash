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

namespace BaseMVCFrame
{
	public class TimeGod
	{
        /// <summary>
        /// 世界的运行时间分
        /// </summary>
        public int m_WorldMinutes { private set; get; }
        /// <summary>
        /// 世界的运行小时
        /// </summary>
        public int m_WorldHours { private set; get; }
        /// <summary>
        /// 世界的运行天数
        /// </summary>
        public int m_WorldDays { private set; get; }
        /// <summary>
        /// 世界的运行月数
        /// </summary>
        public int m_WorldMonths { private set; get; }
        /// <summary>
        /// 世界的运行年数
        /// </summary>
        public int m_WorldYears { private set; get; }

        public TimeGod()
        {
            this.m_WorldMinutes = 0;
            this.m_WorldHours = 0;
            this.m_WorldDays = 0;
            this.m_WorldMonths = 0;
            this.m_WorldYears = 0;
        }

        public TimeGod(int m_WorldMinutes, int m_WorldHours, int m_WorldDays, int m_WorldMonths, int m_WorldYears)
        {
            this.m_WorldMinutes = m_WorldMinutes;
            this.m_WorldHours = m_WorldHours;
            this.m_WorldDays = m_WorldDays;
            this.m_WorldMonths = m_WorldMonths;
            this.m_WorldYears = m_WorldYears;
        }

        /// <summary>
        /// 更新世界时间
        /// </summary>
        public void UpdateWorldTime()
        {
            UpdateMinutes();
        }
        /// <summary>
        /// 更新分
        /// </summary>
        private void UpdateMinutes()
        {
            m_WorldMinutes+=60;
            if (m_WorldMinutes == 60)
            {
                m_WorldMinutes = 0;
                UpdateHours();
            }
        }
        /// <summary>
        /// 更新小时
        /// </summary>
        private void UpdateHours()
        {
            m_WorldHours++;
            if (m_WorldHours == 24)
            {
                m_WorldHours = 0;
                UpdateDays();
            }
        }
        /// <summary>
        /// 更新天数
        /// </summary>
        private void UpdateDays()
        {
            m_WorldDays++;
            if(m_WorldDays == 30)
            {
                m_WorldDays = 0;
                UpdateMonths();
            }
        }
        /// <summary>
        /// 更新月数
        /// </summary>
        private void UpdateMonths()
        {
            m_WorldMonths++;
            if (m_WorldMonths == 12)
            {
                m_WorldMonths = 0;
                UpdateYears();
            }
        }
        /// <summary>
        /// 更新年数
        /// </summary>
        private void UpdateYears()
        {
            m_WorldYears++;
        }

    }
}
