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
using UnityEngine.UI;

namespace BaseMVCFrame.UI
{
	public class GameStartUI : BaseUI
	{
        public new const string UIName = ProjectConsts.GameStartUI;

        private Button m_StartBtn;
        private Button m_ExitBtn;

        protected override void Initialization()
        {
            m_StartBtn = transform.GetChild<Button>("StartBtn");
            m_ExitBtn = transform.GetChild<Button>("ExitBtn");
        }

        protected override void InitButtonListener()
        {
            m_StartBtn.onClick.AddListener(delegate() 
            {
                UIManager.Instance.OpenUIVeiw(ProjectConsts.TempleUI);
            });

            m_ExitBtn.onClick.AddListener(delegate() 
            {

            });
        }

        private void Start()
        {
            //启动MVC框架
            new Manager.FacadeManager();
        }
    }
}
