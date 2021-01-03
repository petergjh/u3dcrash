/***
 * 
 *    Title: "PureMVC" 框架项目
 *           主题： PureMVC 项目全局控制类
 *    Description: 
 *           功能： 
 *                  
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using PureMVC.Patterns.Facade;
using System;
using UnityEngine;

namespace TestPureMVC
{
    public class ApplicationFacade : Facade
    {


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="goRootNode">UI界面的根结点</param>
        public ApplicationFacade(GameObject goRootNode)
        {
            /* MVC 三层的关联绑定 */
            //控制层注册（"命令消息"与控制层类的对应关系，建立绑定）
            //RegisterCommand("Reg_StartDataCommand", typeof(DataCommand));
            var facade = Facade.GetInstance( () => new Facade() );
            facade.RegisterCommand("Reg_AddNumberCommand", () => new AddNumberCommand() );
            facade.RegisterCommand("Reg_SubNumberCommand", () => new SubNumberCommand() );

            //视图层注册
            RegisterMediator(new DataMediator(goRootNode));
            //模型层注册
            RegisterProxy(new DataProxy());
        }

    }
}
