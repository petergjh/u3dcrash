using System.Collections;
using System.Collections.Generic;
//using Common;
using UnityEngine;
using UnityEngine.UI;

public class UIGiftPackageNewServer : MonoBehaviour
{
    public List<Toggle> Toggles;
    public List<GameObject> togRedPoint;
    public Text gemTxt;
    public GameObject VIPNode;
    public Button VIPBtn;
    public Button closeBtn;

    //public UIVipTopNode topNode;

    public GameObject giftPackage;
    public GameObject giftPackage3;
    public GameObject skinStore;
    public GameObject gemStore;

    public Button testPay;

    void Awake()
    {
        //gemTxt.text = Player.Instance.data.gem.ToString();
        SetRedPoint();
        SetTog();
        SetData("GiftPackage");
        //主标签页3 OracleStar限制经验35级才显示
        //if (Player.Instance.data.level >= 35) //&&Player.Instance.data.tutorial[171] == 1
        //{
        //    Toggles[3].gameObject.SetActive(true);
        //}

        //if (GateKeeper.IsAlpha())
        //    testPay.gameObject.SetActive(true);
        //else
        //    testPay.gameObject.SetActive(false);

        //测试付款按钮隐藏
        testPay.gameObject.SetActive(false);
        //testPay.onClick.AddListener(
        //() => { KernelEvent.Instance.notifyPayResult(""); }
        //);

        giftPackage.GetComponent<UIGiftPackageNewServer2>().hideRedPoint += SetRedPoint;
        //giftPackage3.GetComponent<UIGiftPackage3>().hideRedPoint += SetRedPoint;

        if(gameObject.scene.name != "mainscene")
        {
            SetCloseBtn(true);
        }
    }

    /// <summary>
    /// 当窗口层级冲突导致无法返回时可启用这个关闭按钮
    /// </summary>
    /// <param name="isShowCloseBtn"></param>
    public void SetCloseBtn(bool isShowCloseBtn)
    {
        if (isShowCloseBtn)
        {
            this.closeBtn.gameObject.SetActive(true);
            closeBtn.onClick.RemoveAllListeners();
            closeBtn.onClick.AddListener(() =>
            {
                //UIManager.Instance.OnBack();
            });
        }

    }
    private void OnDestroy()
    {
        giftPackage.GetComponent<UIGiftPackageNewServer2>().hideRedPoint -= SetRedPoint;
        //giftPackage3.GetComponent<UIGiftPackage3>().hideRedPoint -= SetRedPoint;
    }

    /// <summary>
    /// 刷新窗口
    /// </summary>
    public void refreshWindow()
    {
        //gemTxt.text = Player.Instance.data.gem.ToString();
        //if (topNode != null)
        //{
        //    topNode.refreshWindow();
        //}

        //UIGemPackage scriptWin = gemStore.GetComponent<UIGemPackage>();
        //if (scriptWin != null && gemStore.activeSelf)
        //{
        //    scriptWin.refreshWindow();
        //}

        UIGiftPackageNewServer2 scriptGift = giftPackage.GetComponent<UIGiftPackageNewServer2>();
        if (scriptGift != null && giftPackage.activeSelf)
        {
            //scriptGift.PurchaseSuccessCallBack();
            if (UIGiftPackageNewServer2.instance.payType != 1)
            {
                //UITips.Instance.ShowMsgTips("Purchase success. Please claim your gems and items from the Mailbox.", 2);
            }
        }
        else
        {
            if(scriptGift == null)
            {
                Debug.LogWarning("scriptGift = null ！！！！！！");
            }
        }

        //UIGiftPackage3 scriptGift2 = giftPackage3.GetComponent<UIGiftPackage3>();
        //if (scriptGift2 != null && giftPackage3.activeSelf)
        //{
        //    scriptGift2.PurchaseSuccessCallBack();
            //if (UIGiftPackage3.instance.payType != 1)
            //{
            //    UITips.Instance.ShowMsgTips("Purchase success. Please claim your gems and items from the Mailbox.", 2);
            //}
        //}
        //else
        //{
        //    if(scriptGift2 == null)
        //    {
        //        Debug.LogWarning("scriptGift3 = null ！！！！！！");
        //    }
        //}
    }

    /// <summary>
    /// 设置标签勾选项激活状态
    /// </summary>
    void SetTog()
    {
        Toggles[0].onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                Toggles[0].transform.Find("LightIcon").gameObject.SetActive(true);
                Toggles[0].transform.Find("BlackIcon").gameObject.SetActive(false);
                SetData("GiftPackage");
                togRedPoint[0].SetActive(false);
            }
            else
            {
                Toggles[0].transform.Find("LightIcon").gameObject.SetActive(false);
                Toggles[0].transform.Find("BlackIcon").gameObject.SetActive(true);
            }
        });
        Toggles[1].onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                Toggles[1].transform.Find("LightIcon").gameObject.SetActive(true);
                Toggles[1].transform.Find("BlackIcon").gameObject.SetActive(false);
                SetData("SkinStore");
            }
            else
            {
                Toggles[1].transform.Find("LightIcon").gameObject.SetActive(false);
                Toggles[1].transform.Find("BlackIcon").gameObject.SetActive(true);
            }
        });
        Toggles[2].onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                Toggles[2].transform.Find("LightIcon").gameObject.SetActive(true);
                Toggles[2].transform.Find("BlackIcon").gameObject.SetActive(false);
                SetData("GemStore");
            }
            else
            {
                Toggles[2].transform.Find("LightIcon").gameObject.SetActive(false);
                Toggles[2].transform.Find("BlackIcon").gameObject.SetActive(true);
            }
        });
        
        Toggles[3].onValueChanged.AddListener((isOn) =>
        {
            if (isOn)
            {
                Toggles[3].transform.Find("LightIcon").gameObject.SetActive(true);
                Toggles[3].transform.Find("BlackIcon").gameObject.SetActive(false);
                SetData("GiftPackage3");
                togRedPoint[3].SetActive(false);
            }
            else
            {
                Toggles[3].transform.Find("LightIcon").gameObject.SetActive(false);
                Toggles[3].transform.Find("BlackIcon").gameObject.SetActive(true);
            }
        });
        VIPBtn.onClick.AddListener(benifitsBtnClicked);
    }

    /// <summary>
    /// 1、礼包界面 2、皮肤界面 3、钻石月卡界面 4、星盘礼包
    /// </summary>
    /// <param name="number"></param>
    public void setTogOn(int number)
    {
        switch (number)
        {
            case 1:
                Toggles[0].isOn = true;
                break;
            case 2:
                Toggles[1].isOn = true;
                break;
            case 3:
                Toggles[2].isOn = true;
                break;
            case 4:
                Toggles[3].isOn = true;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置钻石礼包卡牌
    /// </summary>
    //public void SetCardShow()
    //{
    //    gemStore.GetComponent<UIGemPackage>().SetCardShow();
    //}

    //public void SetSecondType(GIFT_PACKAGE_TYPE second = GIFT_PACKAGE_TYPE.GPT_NONE)
    //{
    //    giftPackage.GetComponent<UIGiftPackageNewServer2>().Init(second);
    //}

    //public void SetSecondType(Server.GiftPackageEventsReply reply, GIFT_PACKAGE_TYPE tabType, bool sendAlready = false, int page = 0)
    //{
    //    giftPackage.GetComponent<UIGiftPackageNewServer2>().Init(reply,tabType,sendAlready,page);
    //}

    /// <summary>
    /// 设置各主节点的激活状态
    /// </summary>
    /// <param name="first"></param>
    void SetData(string first)
    {
        switch (first)
        {
            case "GiftPackage":
                giftPackage.SetActive(true);
                skinStore.SetActive(false);
                gemStore.SetActive(false);
                giftPackage3.SetActive(false);
                giftPackage.GetComponent<UIGiftPackageNewServer2>().isOpened = true;
                //giftPackage.GetComponent<UIGiftPackageNewServer2>().Restart();
                VIPNode.SetActive(false);
                break;
            case "SkinStore":
                giftPackage.SetActive(false);
                //skinStore.SetActive(true);
                skinStore.SetActive(false);
                gemStore.SetActive(false);
                VIPNode.SetActive(false);
                giftPackage3.SetActive(false);
                //skinStore.GetComponent<UISkinStore>().Restart();
                break;
            case "GemStore":
                giftPackage.SetActive(false);
                skinStore.SetActive(false);
                //VIPNode.SetActive(true);
                VIPNode.SetActive(false);
                //gemStore.SetActive(true);
                gemStore.SetActive(false);
                giftPackage3.SetActive(false);
                //gemStore.GetComponent<UIGemPackage>().Restart();
                break;
            case "GiftPackage3":
                giftPackage.SetActive(false);
                skinStore.SetActive(false);
                gemStore.SetActive(false);
                //giftPackage3.SetActive(true);
                giftPackage3.SetActive(false);
                //giftPackage3.GetComponent<UIGiftPackage3>().isOpened = true;
                //giftPackage3.GetComponent<UIGiftPackage3>().Restart();
                VIPNode.SetActive(false);
                break;
            default:
                Toggles[0].isOn = true;
                giftPackage.SetActive(true);
                skinStore.SetActive(false);
                gemStore.SetActive(false);
                giftPackage3.SetActive(false);
                giftPackage.GetComponent<UIGiftPackageNewServer2>().isOpened = true;
                VIPNode.SetActive(false);
                break;
            
        }
    }



    /// <summary>
    /// 设置小红点提示
    /// </summary>
    void SetRedPoint()
    {
        bool showSpecial = false;
        bool showZodiac = false;
        //foreach (var item in Player.Instance.data.giftPackages.giftPackageEventsItem)
        //{
        //    if (item.packageGPT == GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT)
        //    {
        //        if (MathUtil.ParseToInt(item.packageDiscount) == 0 && MathUtil.ParseToInt(item.packageTimes) > 0)
        //        {
        //            togRedPoint[0].SetActive(true); 
        //            showSpecial = true;
        //        }
        //        else
        //        {
        //            if (!showSpecial)
        //            {
        //                togRedPoint[0].SetActive(false);
        //            }
        //        }
        //    }

        //    if (item.packageGPT == GIFT_PACKAGE_TYPE.GPT_ZODIAC_BOX)
        //    {
        //        if (MathUtil.ParseToInt(item.packageDiscount) == 0 && MathUtil.ParseToInt(item.packageTimes) > 0 
        //            && Player.Instance.data.level >= UICommon.Instance.getFeatureOpenLv("OracleStar"))
        //        {
        //            togRedPoint[3].SetActive(true);
        //            showZodiac = true;
        //        }
        //        else
        //        {
        //            if (!showZodiac)
        //            {
        //                togRedPoint[3].SetActive(false);
        //            }
        //        }
        //    }
        //}
    }
    
    public void benifitsBtnClicked()
    {
        //UIManager.Instance.Show(UIManager.UIType.UIBenifits, false);
        //UIBenifits.Instance.scrollToVip(Player.Instance.GetVIPData().VIPLevel);        
    }
}
