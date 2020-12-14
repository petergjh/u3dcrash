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
	public class ProtoDataModelPoxy : Proxy
    {
        //声明本类的名称
        public new const string NAME = "ProtoDataModelPoxy";

        //引用实体类
        private ProtoDataModel _ProtoDataModel = null;

        public ProtoDataModelPoxy() : base(NAME)
        {
            _ProtoDataModel = new ProtoDataModel();
        }
    }
}
