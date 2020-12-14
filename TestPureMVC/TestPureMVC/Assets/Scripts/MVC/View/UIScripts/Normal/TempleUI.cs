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
	public class TempleUI : BaseUI
	{
        public new const string UIName = "TempleUI";

        protected override void Initialization()
        {
            
        }

        protected override void InitButtonListener()
        {
            Transform trans_ButtonBox = transform.GetChild("ButtonBox");
            trans_ButtonBox.GetChild<Button>("NewAdventureBtn").onClick.AddListener(delegate() 
            {
                OpenUIView("SoulTempleUI");
            });
        }
    }
}
