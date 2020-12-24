
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;

public class AddNumberCommand : SimpleCommand
{

    /// <summary>
    /// 执行方法
    /// </summary>
    /// <param name="notification"></param>
    public override void Execute(INotification notification)
    {
        //调用数据层的“增加等级”的方法
        DataProxy datapro = (DataProxy)Facade.RetrieveProxy(DataProxy.NAME);
        datapro.AddLevel(10);
    }
}
