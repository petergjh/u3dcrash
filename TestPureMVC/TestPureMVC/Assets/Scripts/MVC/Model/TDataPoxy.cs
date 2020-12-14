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
using PureMVC.Patterns;

namespace BaseMVCFrame
{
	public class TDataProxy : Proxy
	{
        //声明本类的名称
        public new const string NAME = "TDataProxy";

        //引用实体类
        private TData _Data = null;

        public TDataProxy() : base(NAME)
        {
            _Data = new TData();
        }

        public void ChangeText()
        {
            SendNotification("ChangeTest", _Data);
        }
    }
}
