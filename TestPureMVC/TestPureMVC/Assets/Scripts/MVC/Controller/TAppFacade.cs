/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseMVCFrame
{
	public class TAppFacade : Facade
	{
        public TAppFacade()
        {
            RegisterCommand("Reg_Test",typeof(TDataCommand));

            RegisterMediator(new TUI());

            RegisterProxy(new TDataProxy());
        }
    }
}
