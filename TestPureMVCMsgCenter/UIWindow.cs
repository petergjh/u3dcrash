using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientMsgCenter;
using UnityEngine.Events;
using UnityEngine.UI;


public enum UIAnimType
{
    None,
    Move,
    Scale,
}
public abstract class UIWindow : MonoBehaviour
{
    public UIAnimType animationType;
    public GameObject animationLayer;
    public LeanTweenType easeType;
    public float time = 0.2f;
	string typename="";	//子类名字
    
    /// <summary>
    /// 该类的消息名
    /// </summary>
    public string MsgTypeName = "";

    private void Awake()
    {
        Initialization();
        InitMessage();
        RegisterMessage(MsgTypeName, new Observer("RecMsgHandle", this));
    }

    IEnumerator Start()
	{
        //if (animationLayer) {
        //    yield return null;
        //}
		OnStart ();
        //PlayOpenAnim(); // 暂时不用动画了 9/18
        OnShown();
        
		if (!animationLayer) {
			yield return null;
		}
        
    }

    protected abstract void OnStart();
    
    protected virtual void OnShown() 
    { 
		if (typename != "") 
        {
			Tutorial.Instance.UpdateTutoStatus(typename, true);
		}
	}
    
    /// <summary>
    /// 初始化基础信息
    /// </summary>
    protected virtual void Initialization() { }
    
    /// <summary>
    /// 处理接收到的消息
    /// </summary>
    /// <param name="messageData"></param>
    public virtual void RecMsgHandle(MessageData messageData) { }
    
    /// <summary>
    /// 初始发送的消息
    /// </summary>
    public virtual void InitMessage(){}

    
    void PlayOpenAnim()
    {
        if (animationType== UIAnimType.None || animationLayer == null)
        {
            OnShown();
            return;
        }

        switch (animationType)
        {
            case UIAnimType.Move:
                {
                    LeanTween.moveY(animationLayer, animationLayer.transform.position.y, time).setFrom(1000f).setEase(this.easeType).setOnComplete(OnShown);
                }
                break;
            case UIAnimType.Scale:
                {
                    animationLayer.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                    LeanTween.scale(animationLayer, Vector3.one, time).setFrom(Vector3.one * 0.5f).setEase(LeanTweenType.easeInOutBack).setOnComplete(OnShown);
                }
                break;
        }
    }

    public void Close()
    {
		if (typename != "") {
			Tutorial.Instance.UpdateTutoStatusNoCheck (typename, false);
		}
        if (animationType == UIAnimType.None || animationLayer == null || this.gameObject.activeInHierarchy == false)
        {
            Destroy(this.gameObject);
            return;
        }

		if (UIManager.Instance.GetCurWindowType() != UIManager.UIType.UIShopBuyItem
		    &&UIManager.Instance.GetCurWindowType() != UIManager.UIType.None
		    ) {
			SoundManager.Instance.PlaySound (DataManager.Instance.AudioConfigs.CloseWin);
		}
        Destroy(this.gameObject);
    }

    void PlayCloseAnim()
    {
        if (animationLayer == null)
            return;
    }

	public void SetTypeName(string name){
		typename = name;
	}

    /// <summary>
    /// 查找子节点
    /// </summary>
    /// <param name="Parent"></param>
    /// <param name="ChildNam"></param>
    /// <returns></returns>
    protected Transform FindChild(GameObject Parent, string ChildName)
    {
        return UIManager.Instance.FindChild(Parent, ChildName);
    }

    /// <summary>
    /// 查找子节点上的某个组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Parent"></param>
    /// <param name="ChildNam"></param>
    /// <returns></returns>
    protected T FindChild<T>(GameObject Parent, string ChildName)
    {
        T t = UIManager.Instance.FindChild(Parent, ChildName).GetComponent<T>();
        if (t == null)
        {
            Debug.LogError("查找失败!查找类型:" + t.GetType());
        }
        return t;
    }

    /// <summary>
    /// 从图集中加载sprite资源
    /// </summary>
    /// <param name="atlasType"></param>
    /// <param name="spriteName"></param>
    /// <returns></returns>
    protected void LoadSprite(Image image, string spritePath, 
        UnityAction<GameObject> onComplete = null)
    {
        UICommon.Instance.LoadTexture(image, spritePath, onComplete);
    }

    #region 消息方法
    /// <summary>
    /// 注册消息
    /// </summary>
    /// <param name="MessageName"></param>
    /// <param name="observer"></param>
    public void RegisterMessage(string MessageName, Observer observer)
    {
        MessageCenter.Instance.RegisterObserver(MessageName, observer);
    }
    /// <summary>
    /// 发送新的消息
    /// </summary>
    /// <param name="MessageName">消息名</param>
    public void SendNewMsg(string MessageName)
    {
        MessageCenter.Instance.DisposeObserver(new MessageData(MessageName));
        Debug.Log("SendNewMsg, Name = "+ MessageName);
    }
    /// <summary>
    /// 发送新的消息
    /// </summary>
    /// <param name="MessageName">消息名</param>
    /// <param name="MessageType">消息类型</param>
    public void SendNewMsg(string MessageName, string MessageType)
    {
        MessageCenter.Instance.DisposeObserver(new MessageData(MessageName, MessageType));
        Debug.Log("SendNewMsg, Type = "+MessageType);
    }
    /// <summary>
    /// 发送新消息
    /// </summary>
    /// <param name="MessageName">消息名</param>
    /// <param name="MessageType">消息类型</param>
    /// <param name="MessageData">消息数据</param>
    public void SendNewMsg(string MessageName, string MessageType, object MessageData)
    {
        MessageCenter.Instance.DisposeObserver(new MessageData(MessageName, MessageType, MessageData));
        Debug.Log("SendNewMsg, Type = "+MessageType + " /n DataContent = "+MessageData.ToString());
    }
    
    /// <summary>
    /// 移除消息
    /// </summary>
    /// <param name="MessageName"></param>
    /// <param name="MessageContent"></param>
    public void RemoveMessage(string MessageName, object MessageContent)
    {
        MessageCenter.Instance.RemoveObserver(MessageName, MessageContent);
    }
    #endregion

  

}
