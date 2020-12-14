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
using PureMVC.Interfaces;

namespace BaseMVCFrame
{
	public class TDataCommand : SimpleCommand
	{
        public override void Execute(INotification notification)
        {
            TDataProxy dataProxy = Facade.RetrieveProxy(TDataProxy.NAME) as TDataProxy;
            dataProxy.ChangeText();
        }
    }
}
