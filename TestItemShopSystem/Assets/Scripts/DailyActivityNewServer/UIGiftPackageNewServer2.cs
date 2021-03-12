using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Client;
//using Common;
//using GameData;
//using Server;
using UnityEngine;
using UnityEngine.UI;

//public class UIGiftPackageNewServer2 : UIWindow
public class UIGiftPackageNewServer2 : MonoBehaviour
{
    public List<Toggle> ui_TabBtnList;

    public Button ui_CloseBtn;

    public Button ui_PrevBtn;
    public Button ui_NextBtn;

    public Button ui_PayBtn;
    public Text ui_TotalPrice;
    public Text ui_CurrentPrice;

    public GameObject ui_NPCShow;
    public GameObject ui_CommonShow;
    public GameObject ui_ItemShow;
    public GameObject NameFame;
    public Image ui_Item;
    public Image ui_NPC;
    public Text ui_DisplayName;
    public Text ui_HeroName;
    public Text ui_SkinName;
    public Button ui_NPCDetails;
    //public UIGiftSkill ui_Skill;

    public GameObject ui_CenterNode;
    public Text ui_GiftName;
    public Text ui_GiftTips;
    public GameObject ui_GiftCostPerformenceTxt;
    public Text ui_newGiftCostPerformence;
    public GameObject ui_GiftTime;
    public Text ui_GiftTimeTxt;
    public Text ui_GiftCostPerformence;
    public Text ui_GiftCostPerformencepercentTxt;
    public Text ui_GiftCostPerformenceFree;
    public Image ui_GiftTipsIcon;

    public GameObject ui_BottomNode;
    public Text ui_ActivityTimeLimit;
    public Text ui_CommonTips;

    public GameObject ui_ResetTimeNode;
    public Text ui_ResetTime;

    public GameObject ui_GiftContentNode;
    public GridLayoutGroup ui_ListViewPanel;
    public GameObject ui_listView;
    public GridLayoutGroup ui_ListViewPanel2;
    public Transform ui_listView2;
    /// <summary>
    /// 装备礼包
    /// </summary>
    public GameObject equipPackage;

    public bool isOpened = false;//统计界面是否主动打开
    
    public int payType = -1;//支付类型
    // 
    public static UIGiftPackageNewServer2 instance;

    public Action hideRedPoint;
    //private Dictionary<Common.GIFT_PACKAGE_TYPE, List<GiftPackageEventsItem>> m_GiftPackageDict = new Dictionary<Common.GIFT_PACKAGE_TYPE, List<GiftPackageEventsItem>>();
    //private List<GiftPackageEventsItem> m_CurrentTabGiftBagList = new List<GiftPackageEventsItem>();
    private int m_CurrentTabIndex = 0;
    //private Common.GIFT_PACKAGE_TYPE m_InitChooseTabType = Common.GIFT_PACKAGE_TYPE.GPT_NONE;

    private int m_CurrentPage = 1;
    /// <summary>
    /// 所有礼包的数量
    /// </summary>
    private int m_TotalPage = 0;

    // Tab的key和index映射
    //private Dictionary<Common.GIFT_PACKAGE_TYPE, int> m_TabTypeDict = new Dictionary<Common.GIFT_PACKAGE_TYPE, int>();

    private bool m_SendReqAlready = false;

    // 当前购买礼包的ID
    public string m_CurrentPurchaseGiftID = "";

    private long m_NextRefreshTime;

    private bool isRefreshing = false;

    //private string limitTimeStr = LocalizationManager.Instance.GetString("KEY.8237");
    private bool hasVIP;
    private int vipExp;
    public Button willChosenItemsBtn2;
    public Button willChosenItemsBtn3;
    public Dropdown giftItemsDropdown;
    public GameObject choosePanel;

    //物品列表多选一
    //public List<GiftPackageSpecialSelectData> willChosenItemsReward3;
    private int currentGiftPackageID;

    //物品种类标识
    public int ItemListType;
    private bool isThirdChosen;
    private bool isSecondChosen;

    //可选物品面板标识
    public bool isSecondRewardsShowed;
    public bool isChoosePanelOpened;

    //用来打开物品列表面板的按钮
    public GameObject secondItemBtn;
    public GameObject thirdItemBtn;
    public GameObject itemChosenCell;

    private bool isThirdRewardsShowed;
    public GameObject cell;
    private bool isItemChosenShowed;
    public Image ui_GiftDiscountImg;
    public Text ui_GiftDiscountTxt;
    private float discount;

    /// <summary>
    ///挑选好物品后的购买清单 
    /// </summary>
    //public List<Reward> selectedRewards;

    /// <summary>
    /// 下发礼包道具分组列表
    /// </summary>
    //private List<GiftPackageEventsItemReward> customRewardGroups = new List<GiftPackageEventsItemReward>();

    /// <summary>
    /// 客户端请求消息:可选礼包
    /// </summary>
    //private CustomPackage customPackageMsg = new CustomPackage();

    /// <summary>
    /// Key:可选道具分组ID, Value:可选道具分组Clone体
    /// </summary>
    private Dictionary<int, GameObject> ItemChosenCloneDic = new Dictionary<int, GameObject>();

    /// <summary>
    /// 用来标识可选礼包是否全部选好
    /// </summary>
    private bool isAllChosen;
    public GameObject buyFx;

    /// <summary>
    /// 可选择道具
    /// </summary>
    private const int ITEM_SELECTED = -29;

    /// <summary>
    /// 单双号测试开关，1.隐藏翻页，礼包只能逐级购买 2.可任意挑选礼包购买
    /// </summary>
    private const int GIFT_EXTRA = 1;

    //    void Awake()
    //    {
    //        instance = this;
    //        // 初始化UI
    //        this.InitUI();
    //    }


    //    public void Init(Server.GiftPackageEventsReply reply, Common.GIFT_PACKAGE_TYPE tabType, bool sendAlready = false, int page = 0)
    //    {
    //        this.m_InitChooseTabType = tabType;

    //        // 不发请求，直接用现在的数据
    //        this.m_SendReqAlready = sendAlready;
    //        this.initGiftPackageData(reply);
    //        // 强制切换标签页
    //        this.ViewPageInfo(page);
    //    }

    //    /// <summary>
    //    /// 根据礼包类型显示标签
    //    /// </summary>
    //    /// <param name="tabType"></param>
    //    public void Init(Common.GIFT_PACKAGE_TYPE tabType)
    //    {
    //        this.m_InitChooseTabType = tabType;
    //        ShowTabBtnAndOpen();
    //    }

    //    /// <summary>
    //    /// 刷新
    //    /// </summary>
    //    public void Restart()
    //    {
    //        ui_TabBtnList[0].isOn = true;
    //    }

    //     protected override void OnStart()
    //    {
    //        this.SetTypeName("UIGiftPackage2");

    //        MessagePublisher.Instance.Subscribe<Server.GiftPackageEventsReply>(this.initGiftPackageData);

    //        // 发送数据请求协议
    //        this.SendGiftPackageReq();
    //    }

    //    void InitUI()
    //    {
    //        // NPC
    //        //UICommon.Instance.LoadTexture(this.ui_NPC, "UI/Common/Package_Npc");
    //        // 
    //        this.ui_GiftContentNode.gameObject.SetActive(false);
    //        this.ui_BottomNode.gameObject.SetActive(false);
    //        //
    //        foreach (var tab in this.ui_TabBtnList)
    //        {
    //            tab.gameObject.SetActive(false);
    //        }

    //        #region 注册按钮事件
    //        // 按钮事件注册
    //        if (ui_CloseBtn != null)
    //        {
    //            this.ui_CloseBtn.onClick.AddListener(() =>
    //            {
    //                UIManager.Instance.OnBack();
    //            });
    //        }

    //        // 
    //        this.ui_PrevBtn.onClick.AddListener(() =>
    //        {
    //            this.m_CurrentPage--;
    //            if (this.m_CurrentPage < 1)
    //                this.m_CurrentPage = 1;
    //            this.ViewPageInfo(this.m_CurrentPage);
    //        });

    //        this.ui_NextBtn.onClick.AddListener(() =>
    //        {
    //            this.m_CurrentPage++;
    //            if (this.m_CurrentPage > this.m_TotalPage)
    //                this.m_CurrentPage = this.m_TotalPage;
    //            this.ViewPageInfo(this.m_CurrentPage);
    //        });

    //        this.ui_NPCDetails.onClick.AddListener(ShowSkill);
    //        #endregion
    //    }

    //    //英雄详情展示界面
    //    void ShowSkill()
    //    {
    //        ui_Skill.gameObject.SetActive(true);
    //        ui_CenterNode.SetActive(false);
    //    }

    //    void HideSkill()
    //    {
    //        ui_Skill.gameObject.SetActive(false);
    //        ui_CenterNode.SetActive(true);
    //    }

    //    void SendGiftPackageReq()
    //    {
    //        if (!this.m_SendReqAlready)
    //        {
    //            Client.ClientMessage msg = new Client.ClientMessage();
    //            msg.giftPackageEvents = new Client.GiftPackageEvents();
    //            NetworkManager.Instance.SendProtobufMessage(msg);
    //        }

    //        // reset：下一次可以发送请求了
    //        this.m_SendReqAlready = false;
    //    }

    //    /// <summary>
    //    /// 数据处理：处理完后处理界面信息
    //    /// </summary>
    //    /// <param name="reply"></param>
    //    void initGiftPackageData(Server.GiftPackageEventsReply reply)
    //    {
    //        // 记录下次刷新的零点时间
    //        #region
    //        // 获得当前时区的当前时间
    //        DateTime nextDayTime = Local.ConvertIntDateTime(Local.TimeToRegisterTimezone(Local.GetServerTime() + 86400));

    //        DateTime endTime = new DateTime(nextDayTime.Year, nextDayTime.Month, nextDayTime.Day, 0, 0, 0, DateTimeKind.Utc);

    //        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
    //        // 判断一下是否是夏令时
    //        long offset = 0L;
    //        if (TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now))
    //        {
    //            offset = -3600;
    //        }
    //        this.m_NextRefreshTime = (long)(endTime - startTime).TotalSeconds + offset;
    //        #endregion

    //        // 取消协议注册
    //        MessagePublisher.Instance.Unsubscribe<Server.GiftPackageEventsReply>(this.initGiftPackageData);
    //        if (reply.result != Common.RESULT.RESULT_SUCCESS)
    //        {
    //            Debug.LogError("Server.GiftPackageEventsReply ERROR");
    //            return;
    //        }

    //        // 领取 
    //        if (reply.claim != null)
    //        {
    //            //if (reply.claim.packageId == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT)
    //            //if(DataManager.Instance.PaymentSettings.ContainsKey(reply.claim.packageId))
    //            //if(reply.claim.packageId == 10701 || 
    //            //   reply.claim.packageId == 10702 ||
    //            //   reply.claim.packageId == 10703 ||
    //            //   reply.claim.packageId == 10704 ||
    //            //   reply.claim.packageId == 10705)
    //            //{
    //            //    Debug.Log("活动礼包ID = "+reply.claim.packageId);
    //            //    this.claimSelectedGiftPackage(reply.claim, selectedRewards);
    //            //}

    //            this.claimGiftPackage(reply.claim);
    //            return;
    //        }

    //        // 数据保存
    //        TaskBuffer.Instance.GiftPackageData = reply;
    //        this.m_GiftPackageDict.Clear();
    //        bool isShowHot = false; // 限时热卖活动
    //        payType = reply.payType;
    //        if (payType == 1)
    //        {
    //            ui_CommonTips.gameObject.SetActive(false);
    //        }
    //        //foreach (var item in reply.giftPackageEventsItem)
    //        //{
    //        //    if (item.packageGPT == GIFT_PACKAGE_TYPE.GPT_ZODIAC_BOX ||
    //        //        item.packageGPT == GIFT_PACKAGE_TYPE.GPT_ZODIAC_INFUSE ||
    //        //        item.packageGPT == GIFT_PACKAGE_TYPE.GPT_ZODIAC_STARLIGHT)
    //        //    {
    //        //        continue;
    //        //    }
    //        //    if (!this.m_GiftPackageDict.ContainsKey(item.packageGPT))
    //        //    {
    //        //        this.m_GiftPackageDict.Add(item.packageGPT, new List<GiftPackageEventsItem>());
    //        //    }

    //        //    this.m_GiftPackageDict[item.packageGPT].Add(item);

    //        //    if (item.packageGPT == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT)
    //        //    {
    //        //        if (MathUtil.ParseToInt(item.packageTimes) > 0)
    //        //        {
    //        //            isShowHot = true;
    //        //        }
    //        //    }
    //        //}
    //        //新服庆典礼包数据
    //        foreach (var item in reply.newServerEventPackage)
    //        {

    //            //if(MathUtil.ParseToInt(item.extra) == 1)
    //            if(MathUtil.ParseToInt(item.extra) == GIFT_EXTRA)
    //            {
    //                ui_PrevBtn.gameObject.SetActive(false);
    //                ui_NextBtn.gameObject.SetActive(false);
    //            }
    //            if (item.packageGPT == GIFT_PACKAGE_TYPE.GPT_ZODIAC_BOX ||
    //                item.packageGPT == GIFT_PACKAGE_TYPE.GPT_ZODIAC_INFUSE ||
    //                item.packageGPT == GIFT_PACKAGE_TYPE.GPT_ZODIAC_STARLIGHT ||
    //                item.packageGPT == GIFT_PACKAGE_TYPE.GPT_BM_RESOURCE ||
    //                item.packageGPT == GIFT_PACKAGE_TYPE.GPT_BM_SPEED ||
    //                item.packageGPT == GIFT_PACKAGE_TYPE.GPT_BM_WAR)
    //            {
    //                continue;
    //            }
    //            if (!this.m_GiftPackageDict.ContainsKey(item.packageGPT))
    //            {
    //                this.m_GiftPackageDict.Add(item.packageGPT, new List<GiftPackageEventsItem>());
    //            }

    //            this.m_GiftPackageDict[item.packageGPT].Add(item);

    //            if (item.packageGPT == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT)
    //            {
    //                if (MathUtil.ParseToInt(item.packageTimes) > 0)
    //                {
    //                    isShowHot = true;
    //                }
    //            }
    //        }
    //        Debug.Log("下发的礼包列表数量="+m_GiftPackageDict.Count);

    //        if (UIStatus.Instance)
    //        {
    //            UIStatus.Instance.setIshowHot(isShowHot);
    //        }

    //        if (isShowHot == false)
    //        {
    //            if (this.m_GiftPackageDict.ContainsKey(Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT))
    //            {
    //                this.m_GiftPackageDict.Remove(Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT);
    //            }
    //        }

    //        if (UIStatus.Instance && UIStatus.Instance.giftPackage)
    //        {
    //            UIStatus.Instance.giftPackage.transform.Find("FX_bxkai/Hot").gameObject.SetActive(isShowHot);
    //        }

    //        this.ShowTabBtnAndOpen();
    //    }

    //    /// <summary>
    //    /// 获取礼包
    //    /// </summary>
    //    /// <param name="reply"></param>
    //    void claimGiftPackage(Server.GiftPackageEventsClaimReply reply)
    //    {
    //        // 刷新giftpackage界面
    //        this.PurchaseSuccessCallBack();
    //        //UITips.Instance.ShowMsgTips("Please claim your gems and items from the Mailbox.", 2);
    //        if (reply.claimRewards != null && reply.claimRewards.rewards.Count > 0)
    //        {
    //            UIManager.Instance.Show(UIManager.UIType.UIGiftRewardPanel, false, onLoadHandler: (obj) =>
    //            {
    //                if (obj != null)
    //                {
    //                    UIRewardPanel rewardPanel = obj.GetComponent<UIRewardPanel>();
    //                    if(rewardPanel != null)
    //                    {
    //                        rewardPanel.InitUIData(reply.claimRewards.rewards, NumberText: String.Format(
    //                            LocalizationManager.Instance.GetString("KEY.13460"),ui_GiftName.text.ToString()),
    //                        onClickCallBack:
    //                        () =>
    //                        {
    //                            if (reply.claimRewards.exceedRewards != null && reply.claimRewards.exceedRewards.Count > 0)
    //                            {
    //                                UICommon.Instance.ItemOverStep(reply.claimRewards.exceedRewards, null, null);
    //                            }
    //                            UIManager.Instance.Close(UIManager.UIType.UIGiftRewardPanel);
    //                        }, showTitle: true);
    //                    }
    //                }
    //            });
    //            Player.Instance.AddRewards(reply.claimRewards.rewards);

    //            //GameObject openWin = UIManager.Instance.FindOpenedWindow(UIManager.UIType.UIPayShop);
    //            //if (openWin != null)
    //            //{
    //            //    UIPayShop scriptPay = openWin.GetComponent<UIPayShop>();
    //            //    if (scriptPay != null)
    //            //    {
    //            //        scriptPay.refreshWindow();
    //            //    }
    //            //}

    //            GameObject openWin = UIManager.Instance.FindOpenedWindow(UIManager.UIType.UIGiftPackageNewServer);
    //            if (openWin != null)
    //            {
    //                UIGiftPackageNewServer scriptPay = openWin.GetComponent<UIGiftPackageNewServer>();
    //                if (scriptPay != null)
    //                {
    //                    scriptPay.refreshWindow();
    //                }
    //            }
    //            /*// 同步物品
    //            if (reply.syncItems.Count > 0)
    //            {
    //                foreach (var item in reply.syncItems)
    //                {
    //                    var orgItems = Player.Instance.FindItemAmount(item.itemid);
    //                    if (orgItems == item.itemAmount)
    //                        continue;
    //                    if (orgItems > item.itemAmount)
    //                    {
    //                        Player.Instance.RemoveItem(item.itemid, orgItems - item.itemAmount);
    //                    }
    //                    else
    //                    {
    //                        Player.Instance.InsertItem(item.itemid, item.itemAmount - orgItems);
    //                    }
    //                }
    //            }*/
    //            foreach (var item in Player.Instance.data.giftPackages.giftPackageEventsItem)
    //            {
    //                if (item.packageGPT == GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT
    //                || item.packageGPT == GIFT_PACKAGE_TYPE.GPT_NEW_SERVER_EVENT)
    //                {
    //                    if (MathUtil.ParseToInt(item.packageDiscount) == 0 && MathUtil.ParseToInt(item.packageKey) == reply.packageId)
    //                    {
    //                        item.packageTimes = "0";
    //                    }
    //                }
    //            }
    //            if (hideRedPoint != null)
    //            {
    //                hideRedPoint();
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// 获取可选礼包
    //    /// </summary>
    //    /// <param name="reply"></param>
    //    void claimSelectedGiftPackage(Server.GiftPackageEventsClaimReply reply, List<Reward> selectedRewards)
    //    {
    //        // 刷新giftpackage界面
    //        this.PurchaseSuccessCallBack();
    //        //UITips.Instance.ShowMsgTips("Please claim your gems and items from the Mailbox.", 2);
    //        if (reply.claimRewards != null && reply.claimRewards.rewards.Count > 0 &&
    //            selectedRewards != null && selectedRewards.Count > 0)
    //        {
    //            UIManager.Instance.Show(UIManager.UIType.UIGiftRewardPanel, false, onLoadHandler: (obj) =>
    //            {
    //                if (obj != null)
    //                {
    //                    UIRewardPanel rewardPanel = obj.GetComponent<UIRewardPanel>();
    //                    if (rewardPanel != null)
    //                    {
    //                        //rewardPanel.InitUIData(reply.claimRewards.rewards, NumberText: String.Format(
    //                        //    LocalizationManager.Instance.GetString("KEY.13460"), ui_GiftName.text.ToString()),
    //                        rewardPanel.InitUIData(selectedRewards, NumberText: String.Format(
    //                            LocalizationManager.Instance.GetString("KEY.13460"), ui_GiftName.text.ToString()),

    //                        onClickCallBack:
    //                        () =>
    //                        {
    //                            if (reply.claimRewards.exceedRewards != null && reply.claimRewards.exceedRewards.Count > 0)
    //                            {
    //                                UICommon.Instance.ItemOverStep(reply.claimRewards.exceedRewards, null, null);
    //                            }
    //                            UIManager.Instance.Close(UIManager.UIType.UIGiftRewardPanel);
    //                        }, showTitle: true);
    //                    }
    //                }
    //            });
    //            //Player.Instance.AddRewards(reply.claimRewards.rewards);
    //            Player.Instance.AddRewards(selectedRewards);

    //            GameObject openWin = UIManager.Instance.FindOpenedWindow(UIManager.UIType.UIPayShop);
    //            if (openWin != null)
    //            {
    //                UIPayShop scriptPay = openWin.GetComponent<UIPayShop>();
    //                if (scriptPay != null)
    //                {
    //                    scriptPay.refreshWindow();
    //                }
    //            }
    //            /*// 同步物品
    //            if (reply.syncItems.Count > 0)
    //            {
    //                foreach (var item in reply.syncItems)
    //                {
    //                    var orgItems = Player.Instance.FindItemAmount(item.itemid);
    //                    if (orgItems == item.itemAmount)
    //                        continue;
    //                    if (orgItems > item.itemAmount)
    //                    {
    //                        Player.Instance.RemoveItem(item.itemid, orgItems - item.itemAmount);
    //                    }
    //                    else
    //                    {
    //                        Player.Instance.InsertItem(item.itemid, item.itemAmount - orgItems);
    //                    }
    //                }
    //            }*/
    //            foreach (var item in Player.Instance.data.giftPackages.giftPackageEventsItem)
    //            {
    //                if (item.packageGPT == GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT)
    //                {
    //                    if (MathUtil.ParseToInt(item.packageDiscount) == 0 && MathUtil.ParseToInt(item.packageKey) == reply.packageId)
    //                    {
    //                        item.packageTimes = "0";
    //                    }
    //                }
    //            }
    //            if (hideRedPoint != null)
    //            {
    //                hideRedPoint();
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// 显示Tab标签
    //    /// </summary>
    //    void ShowTabBtnAndOpen()
    //    {
    //        this.m_TabTypeDict.Clear();
    //        int tabNums = this.ui_TabBtnList.Count;
    //        bool hasSpecail = false;
    //        // 初始化Tab
    //        List<Common.GIFT_PACKAGE_TYPE> tabTypeList = this.m_GiftPackageDict.Keys.ToList();
    //        if(tabTypeList.Contains(GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT))
    //        {
    //            tabTypeList.Remove(GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT);
    //            if(tabTypeList.Contains(GIFT_PACKAGE_TYPE.GPT_FIRST))
    //            {
    //                tabTypeList.Insert(1, GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT);
    //            }
    //            else
    //            {
    //                tabTypeList.Insert(0, GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT);
    //            }
    //        }
    //        if(tabTypeList.Contains(GIFT_PACKAGE_TYPE.GPT_EQUIP))
    //        {
    //            tabTypeList.Remove(GIFT_PACKAGE_TYPE.GPT_EQUIP);
    //            if(tabTypeList.Contains(GIFT_PACKAGE_TYPE.GPT_FIRST))
    //            {
    //                tabTypeList.Insert(1, GIFT_PACKAGE_TYPE.GPT_EQUIP);
    //            }
    //            else
    //            {
    //                tabTypeList.Insert(0, GIFT_PACKAGE_TYPE.GPT_EQUIP);
    //            }
    //        }
    //        for (int i = 0; i < tabTypeList.Count; i++)
    //        {
    //            if (i < tabNums)
    //            {
    //                /*
    //                if (tabTypeList[i] == GIFT_PACKAGE_TYPE.GPT_HERO_GROWTH)
    //                {
    //                   continue; 
    //                }
    //                */
    //                Common.GIFT_PACKAGE_TYPE tabType = tabTypeList[i];

    //                Debug.Log("下发的礼包列表类型="+tabType);

    //                this.ui_TabBtnList[i].gameObject.SetActive(true);
    //                this.ui_TabBtnList[i].GetComponentsInChildren<Text>(true)[0].text = 
    //                    LocalizationManager.Instance.GetString(this.m_GiftPackageDict[tabType][0].packageTabType);

    //                this.ui_TabBtnList[i].transform.Find("hot").gameObject.SetActive(
    //                    tabType == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT ||
    //                    tabType == Common.GIFT_PACKAGE_TYPE.GPT_LIMITED_HERO_LOTTERY ||
    //                    tabType == Common.GIFT_PACKAGE_TYPE.GPT_EQUIP);
    //                this.ui_TabBtnList[i].transform.Find("threeImg").gameObject.SetActive(false);
    //                this.ui_TabBtnList[i].transform.Find("sixImg").gameObject.SetActive(false);
    //                Server.UpcomingEventsItem Activity =
    //                    UpComingBuffer.GetInstance().FindActivity(UpComingBuffer.UPCOMING_HIGH_DISCOUNT);
    //                if (Activity != null)
    //                {
    //                    //高折扣礼包
    //                    this.ui_TabBtnList[i].transform.Find("threeImg").gameObject.SetActive(
    //                        Activity.itemStates == "3" && (tabType == Common.GIFT_PACKAGE_TYPE.GPT_HERO ||
    //                        tabType == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT ||
    //                        tabType == Common.GIFT_PACKAGE_TYPE.GPT_SOUL));
    //                    this.ui_TabBtnList[i].transform.Find("sixImg").gameObject.SetActive(
    //                        Activity.itemStates == "6" && (tabType == Common.GIFT_PACKAGE_TYPE.GPT_HERO ||
    //                                                       tabType == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT ||
    //                                                       tabType == Common.GIFT_PACKAGE_TYPE.GPT_SOUL));
    //                }



    //                //奖励物品的格外折扣
    //                Server.UpcomingEventsItem blackFriday =
    //                 UpComingBuffer.GetInstance().FindActivity(UpComingBuffer.UPCOMING_BLACK_FRIDAY);
    //                if (blackFriday != null)
    //                {
    //                    float result = 0;
    //                    foreach (var item in m_GiftPackageDict)
    //                    {
    //                        if (tabType == GIFT_PACKAGE_TYPE.GPT_HERO_SKIN_ESSENCE && item.Key == GIFT_PACKAGE_TYPE.GPT_HERO_SKIN_ESSENCE)
    //                        {
    //                            foreach (var value in item.Value)
    //                            {
    //                                foreach (var reward in value.itemReward)
    //                                {
    //                                    if (reward.multiplier != "")
    //                                    {
    //                                        float number = MathUtil.ParseToFloat(reward.multiplier);
    //                                        if (result < (number - 1) * 100)
    //                                        {
    //                                            result = (number - 1) * 100;
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                            break;
    //                        }
    //                        else if (tabType == GIFT_PACKAGE_TYPE.GPT_AWAKEN && item.Key == GIFT_PACKAGE_TYPE.GPT_AWAKEN)
    //                        {
    //                            foreach (var value in item.Value)
    //                            {
    //                                foreach (var reward in value.itemReward)
    //                                {
    //                                    if (reward.multiplier != "")
    //                                    {
    //                                        float number = MathUtil.ParseToFloat(reward.multiplier);
    //                                        if (result < (number - 1) * 100)
    //                                        {
    //                                            result = (number - 1) * 100;
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                            break;
    //                        }
    //                        else if (tabType == GIFT_PACKAGE_TYPE.GPT_SOUL&& item.Key == GIFT_PACKAGE_TYPE.GPT_SOUL)
    //                        {
    //                            foreach (var value in item.Value)
    //                            {
    //                                foreach (var reward in value.itemReward)
    //                                {
    //                                    if (reward.multiplier != "")
    //                                    {
    //                                        float number = MathUtil.ParseToFloat(reward.multiplier);
    //                                        if (result < (number - 1) * 100)
    //                                        {
    //                                            result = (number - 1) * 100;
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                            break;
    //                        }
    //                    }
    //                    if (result != 0)
    //                    {
    //                        this.ui_TabBtnList[i].transform.Find("stateImg").gameObject.SetActive(true);
    //                        this.ui_TabBtnList[i].transform.Find("stateImg").gameObject.GetComponentInChildren<Text>().text = "+ " + result + "%";
    //                    }
    //                    else
    //                    {
    //                        this.ui_TabBtnList[i].transform.Find("stateImg").gameObject.SetActive(false);
    //                    }
    //                }



    //                // 闭包
    //                int index = i;
    //                this.ui_TabBtnList[i].onValueChanged.AddListener((isOn) =>
    //                {
    //                    if (isOn)
    //                    {
    //                        Debug.Log("标签类型"+tabType.ToString());
    //                        this.ClickTab(index, tabType);
    //                        PaymentUpLoadManager.Instance.OpenPayment(isOpened, SetPaymentType(tabType));
    //                        if (tabType != GIFT_PACKAGE_TYPE.GPT_EQUIP)
    //                        {
    //                            HideSkill(); //隐藏英雄详情界面
    //                        }
    //                    }
    //                });

    //                // index与key：映射字典
    //                this.m_TabTypeDict.Add(tabType, i);
    //            }
    //        }

    //        // 判断是否设置了m_InitChooseTabKey
    //        if (this.m_TabTypeDict.ContainsKey(this.m_InitChooseTabType))
    //        {
    //            this.m_CurrentTabIndex = this.m_TabTypeDict[this.m_InitChooseTabType];
    //        }
    //        // 默认
    //        if (this.m_CurrentTabIndex < this.m_TabTypeDict.Count)
    //        {
    //            if (this.ui_TabBtnList[this.m_CurrentTabIndex].isOn)
    //            {
    //                this.ui_TabBtnList[this.m_CurrentTabIndex].isOn = false;
    //            }
    //            this.ui_TabBtnList[this.m_CurrentTabIndex].isOn = true;
    //        }
    //    }

    //    //发送打开礼包信息
    //    PAYMENT_TYPE SetPaymentType(GIFT_PACKAGE_TYPE type)
    //    {
    //        switch(type)
    //        {
    //            case GIFT_PACKAGE_TYPE.GPT_NONE:
    //                return PAYMENT_TYPE.PT_DEFAULT;
    //            case GIFT_PACKAGE_TYPE.GPT_HERO:
    //                return PAYMENT_TYPE.PT_HERO_PACKAGE;
    //            case GIFT_PACKAGE_TYPE.GPT_SKIN:
    //                return PAYMENT_TYPE.PT_HERO_SKIN_PACKAGE;
    //            case GIFT_PACKAGE_TYPE.GPT_SOUL:
    //                return PAYMENT_TYPE.PT_ITEM_PACKAGE;
    //            case GIFT_PACKAGE_TYPE.GPT_FIRST:
    //                return PAYMENT_TYPE.PT_FIRST_LOGIN_PACKAGE;
    //            case GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT:
    //                return PAYMENT_TYPE.PT_SPECIAL_EVENT;
    //            case GIFT_PACKAGE_TYPE.GPT_LIMITED_HERO_LOTTERY:
    //                return PAYMENT_TYPE.PT_LIMITED_HERO_LOTTERY;
    //            case GIFT_PACKAGE_TYPE.GPT_HERO_GROWTH:
    //                return PAYMENT_TYPE.PT_HERO_GROWTH_PACKAGE;
    //            case GIFT_PACKAGE_TYPE.GPT_BATTLE_PASS_LEVEL:
    //                return PAYMENT_TYPE.PT_BATTLE_PASS_LEVEL_PACKAGE;
    //            case GIFT_PACKAGE_TYPE.GPT_BATTLE_PASS_MEDAL:
    //                return PAYMENT_TYPE.PT_BATTLE_PASS_HONOR_MEDAL_PACKAGE;
    //            case GIFT_PACKAGE_TYPE.GPT_AWAKEN:
    //                return PAYMENT_TYPE.PT_AWAKEN_STONE;
    //            case GIFT_PACKAGE_TYPE.GPT_EPIC_BOX_TICKET:
    //                return PAYMENT_TYPE.PT_EPIC_BOX_TICKET_PACKAGE;
    //            case GIFT_PACKAGE_TYPE.GPT_HERO_SKIN_ESSENCE:
    //                return PAYMENT_TYPE.PT_PAYMENT_HERO_SKIN_ESSENCE;
    //            case GIFT_PACKAGE_TYPE.GPT_NEW_SERVER_EVENT:
    //                return PAYMENT_TYPE.PT_NEW_SERVER_EVENT;
    //        }
    //        return PAYMENT_TYPE.PT_DEFAULT;
    //    }

    //    /// <summary>
    //    /// Tab事件触发
    //    /// </summary>
    //    /// <param name="index"></param>
    //    /// <param name="key"></param>
    //    void ClickTab(int index, Common.GIFT_PACKAGE_TYPE key)
    //    {
    //        this.ui_GiftContentNode.gameObject.SetActive(false);
    //        //
    //        this.m_CurrentTabIndex = index;
    //        this.m_CurrentPage = 1;
    //        if (key == GIFT_PACKAGE_TYPE.GPT_EQUIP && equipPackage != null)
    //        {
    //            //装备礼包
    //            NameFame.SetActive(false);
    //            this.equipPackage.SetActive(true);
    //            List<GiftPackageEventsItem> equipPackageList = this.m_GiftPackageDict[key];
    //            equipPackage.GetComponent<UIEquipPackage>().InitUI(equipPackageList);
    //            return;
    //        }
    //        this.equipPackage.SetActive(false);

    //        // 获得Tab对应礼包数据
    //        this.m_CurrentTabGiftBagList.Clear();
    //        //点击标签时清一下可选礼包的格子数，防止阻挡购买非可选礼包
    //        ItemChosenCloneDic.Clear();

    //        if (this.m_GiftPackageDict.ContainsKey(key))
    //        {
    //            //把所有下发的礼包放入一个临时列表  
    //            List<GiftPackageEventsItem> tmpGiftList = this.m_GiftPackageDict[key];
    //            // 排序：已购买靠后
    //            List<GiftPackageEventsItem> buyedgifts = new List<GiftPackageEventsItem>();
    //            for (int i = 0; i < tmpGiftList.Count; i++)
    //            {
    //                //if (tmpGiftList[i].packageTimes.Length > 0 )
    //                {
    //                    if (MathUtil.ParseToInt(tmpGiftList[i].packageTimes) <= 0)
    //                    {
    //                        //把所有已经购买过的礼包存入一个单独的列表
    //                        buyedgifts.Add(tmpGiftList[i]);
    //                        //倒序.当所有礼包买完后显示价格最高的那个置灰礼包
    //                        buyedgifts.Reverse();
    //                    }
    //                    else
    //                    {
    //                        this.m_CurrentTabGiftBagList.Add(tmpGiftList[i]);
    //                    }
    //                }
    //            }
    //            //把已经购买过的礼包放到所有礼包列表的后排
    //            this.m_CurrentTabGiftBagList.AddRange(buyedgifts);
    //            this.m_TotalPage = this.m_CurrentTabGiftBagList.Count;

    //            bool isHero = this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_HERO;
    //                        //this.m_CurrentTabGiftBagList[0].packageGPT != GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT && 
    //                        //this.m_CurrentTabGiftBagList[0].packageGPT != GIFT_PACKAGE_TYPE.GPT_FIRST;
    //            bool isSpecial = this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_HERO ||
    //                             this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_SKIN ||
    //                             this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT;
    //            int heroID = 0;
    //            string heroCard = null;
    //            if (!string.IsNullOrEmpty(this.m_CurrentTabGiftBagList[0].packageNewHeroID))
    //            {
    //                heroID = int.Parse(this.m_CurrentTabGiftBagList[0].packageNewHeroID);
    //                heroCard = DataManager.Instance.Roles[heroID].Art;
    //                ui_DisplayName.text = DataManager.Instance.Roles[heroID].DisplayName;
    //                ui_HeroName.text = DataManager.Instance.Roles[heroID].ArtName;
    //                if (!isHero && isSpecial)
    //                {
    //                    heroCard = DataManager.Instance.RoleSkinData[heroID][1].Art;
    //                    ui_DisplayName.text = DataManager.Instance.RoleSkinData[heroID][1].DisplayName;
    //                    ui_HeroName.text = DataManager.Instance.RoleSkinData[heroID][1].ArtName;
    //                    ui_SkinName.text = DataManager.Instance.RoleSkinData[heroID][1].SkinName;
    //                }
    //                ui_SkinName.gameObject.SetActive(!isHero);
    //            }

    //            // 显示NPC
    //            if (this.m_TotalPage > 0 && (this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_HERO 
    //                                         || this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_SKIN
    //                                         || this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT ))
    //            {
    //                //UICommon.Instance.LoadTexture(this.ui_NPC, this.m_CurrentTabGiftBagList[0].packageNPCIcon);
    //                UICommon.Instance.LoadTexture(this.ui_NPC, heroCard);//当前礼包英雄卡片
    //                ui_Skill.Init(isHero, heroID);
    //                //ui_NPCShow.SetActive(true);
    //                NameFame.SetActive(true);
    //                //ui_NPCDetails.gameObject.SetActive(true);
    //                ui_CommonShow.SetActive(false);
    //                ui_ItemShow.SetActive(false);
    //            }
    //            else if (this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_SOUL || 
    //                this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_AWAKEN
    //            || this.m_CurrentTabGiftBagList[0].packageGPT == GIFT_PACKAGE_TYPE.GPT_EPIC_BOX_TICKET)
    //            {
    //                UICommon.Instance.LoadTexture(this.ui_Item, this.m_CurrentTabGiftBagList[0].packageNPCIcon);
    //                ui_ItemShow.SetActive(true);
    //                ui_CommonShow.SetActive(false);
    //                ui_NPCShow.SetActive(false);
    //            }
    //            else
    //            {
    //                //ui_CommonShow.SetActive(true);
    //                ui_CommonShow.SetActive(false);
    //                ui_NPCShow.SetActive(false);
    //                ui_ItemShow.SetActive(false);
    //            }

    //            // 是否显示活动限时(英雄+首登),倒计时重置（首登+皮肤）
    //            Common.GIFT_PACKAGE_TYPE tabType = this.GetTabTypeByIndex(this.m_CurrentTabIndex);
    //            switch (tabType)
    //            {
    //                case Common.GIFT_PACKAGE_TYPE.GPT_NEW_SERVER_EVENT:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(true);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(false);
    //                    break;
    //                case Common.GIFT_PACKAGE_TYPE.GPT_HERO:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(true);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(false);
    //                    break;
    //                case Common.GIFT_PACKAGE_TYPE.GPT_SKIN:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(true);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(true);
    //                    break;
    //                case Common.GIFT_PACKAGE_TYPE.GPT_FIRST:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(true);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(true);
    //                    break;
    //                case Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(true);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(false);
    //                    break;
    //                case Common.GIFT_PACKAGE_TYPE.GPT_LIMITED_HERO_LOTTERY:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(true);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(false);
    //                    break;
    //                case Common.GIFT_PACKAGE_TYPE.GPT_AWAKEN:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(true);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(false);
    //                    break;
    //                case Common.GIFT_PACKAGE_TYPE.GPT_EQUIP:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(false);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(true);
    //                    break;
    //                default:
    //                    this.ui_ActivityTimeLimit.gameObject.SetActive(false);
    //                    this.ui_ResetTimeNode.gameObject.SetActive(false);
    //                    break;
    //            }

    //            this.ViewPageInfo(this.m_CurrentPage);
    //        }
    //    }

    //    /// <summary>
    //    /// 显示页面内容
    //    /// </summary>
    //    /// <param name="page"></param>
    //    void ViewPageInfo(int page)
    //    {
    //        this.m_CurrentPage = page;
    //        //
    //        this.ui_GiftContentNode.gameObject.SetActive(true);
    //        this.ui_BottomNode.gameObject.SetActive(true);

    //        //
    //        if (this.m_CurrentPage <= this.m_TotalPage)
    //        {
    //            // 显示信息
    //            GiftPackageEventsItem gift = this.m_CurrentTabGiftBagList[this.m_CurrentPage - 1];
    //            //
    //            this.ui_GiftName.text = LocalizationManager.Instance.GetString(gift.packageType);
    //            ui_GiftCostPerformencepercentTxt.gameObject.SetActive(false);
    //            if (MathUtil.ParseToInt(gift.packageDiscount) > 0)
    //            {
    //                ui_GiftCostPerformenceTxt.SetActive(true);
    //                //this.ui_GiftCostPerformencepercentTxt.gameObject.SetActive(true);
    //                this.ui_GiftCostPerformenceFree.gameObject.SetActive(false);
    //                this.ui_newGiftCostPerformence.text = gift.packageDiscount;
    //            }
    //            else
    //            {
    //                ui_GiftCostPerformenceTxt.SetActive(false);
    //                //this.ui_GiftCostPerformencepercentTxt.gameObject.SetActive(false);
    //                this.ui_GiftCostPerformenceFree.gameObject.SetActive(true);
    //                this.ui_GiftCostPerformenceFree.text = LocalizationManager.Instance.GetString("KEY.2895");
    //            }

    //            if (gift.packageGPT == Common.GIFT_PACKAGE_TYPE.GPT_EPIC_BOX_TICKET
    //                || gift.packageGPT == GIFT_PACKAGE_TYPE.GPT_AWAKEN)
    //            {
    //                ui_GiftTime.SetActive(true);
    //                ui_GiftTimeTxt.text = gift.packageTimes;
    //                if (MathUtil.ParseToInt(gift.packageTimes) > 0)
    //                {
    //                    ui_GiftTimeTxt.color = new Color(132f, 226f, 46f, 255f);
    //                }
    //                else
    //                {
    //                    ui_GiftTimeTxt.color = new Color(245f, 68f, 43f, 255f);
    //                }
    //            }
    //            else
    //            {
    //                ui_GiftTime.SetActive(false);
    //            }

    //            this.ui_GiftTips.text = gift.packageTips;
    //            UICommon.Instance.LoadTexture(this.ui_GiftTipsIcon, gift.packageTipsIcon);
    //            ui_GiftTipsIcon.gameObject.SetActive(false);
    //            this.ui_TotalPrice.text = "<color=#ff0000>" + "USD" + gift.packageOrderPrice + "</color>";
    //            if (MathUtil.ParseToFloat(gift.packagePrice) > 0)
    //            {
    //                this.ui_CurrentPrice.text = IAPManager.Instance.GetFormatPrice(MathUtil.ParseToInt(gift.packageKey),
    //                    MathUtil.ParseToFloat(gift.packagePrice));
    //            }
    //            else
    //            {
    //                this.ui_CurrentPrice.text = LocalizationManager.Instance.GetString("KEY.2895");
    //            }

    //            // ResourcesLoader.Load<GameObject>("UI/prefab/UIGiftRewardCell", (obj) =>
    //            //  {
    //            //     // 显示Item列表
    //                this.ShowItemList(gift);
    //            // });


    //            //
    //            this.ui_PayBtn.onClick.RemoveAllListeners();
    //            Debug.Log("礼包剩余购买次数 gift.packageTimes = "+gift.packageGPT+gift.packageKey + gift.packageTimes);

    //            this.SetPurchaseButtonState(this.ui_PayBtn, MathUtil.ParseToInt(gift.packageTimes) <= 0, "USD" + gift.packageOrderPrice);

    //            if (MathUtil.ParseToInt(gift.packageTimes) > 0)
    //            {
    //                this.ui_PayBtn.onClick.AddListener(() =>
    //                {
    //                    //两个可选礼包须全选上才能购买
    //                    //if (!isAllChosen && gift.packageGPT == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT )
    //                    Debug.Log("是否全部选好了=" + isAllChosen);
    //                    Debug.Log("礼包剩余购买次数 gift.packageTimes = "+gift.packageGPT+gift.packageKey + gift.packageTimes);
    //                    if (!isAllChosen && ItemChosenCloneDic.Count >= 0)
    //                    {
    //                        //FindChild(secondItemBtn.gameObject, "fx_ChooseGuide").gameObject.SetActive(true);
    //                        //FindChild(thirdItemBtn.gameObject, "fx_ChooseGuide").gameObject.SetActive(true);
    //                        foreach (var willChosenItem in ItemChosenCloneDic.Values)
    //                        {
    //                            willChosenItem.transform.Find("fx_ChooseGuide").gameObject.SetActive(true);
    //                        }

    //                        return;
    //                    }

    //                    if (GateKeeper.IsLATEST())
    //                    {    // 充值
    //                        string str = LocalizationManager.Instance.GetString("KEY.7783") + " "       // 测试服
    //                            + LocalizationManager.Instance.GetString("KEY.2729") + " "              // 关闭
    //                            + LocalizationManager.Instance.GetString("KEY.2519");                   // 此功能
    //                        PopupManager.Instance.ShowMainTips(str, 3);
    //                        return;
    //                    }

    //                    Common.GIFT_PACKAGE_TYPE tabType = this.GetTabTypeByIndex(this.m_CurrentTabIndex);
    //                    // 1、判断礼包时间是否过期(英雄礼包+首登礼包)
    //                    if (tabType == Common.GIFT_PACKAGE_TYPE.GPT_HERO || tabType == Common.GIFT_PACKAGE_TYPE.GPT_FIRST)
    //                    {
    //                        if (MathUtil.ParseToLong(gift.packageRemind) <= Local.GetServerTime())
    //                        {
    //                            PopupManager.Instance.ShowMainTips("KEY.3490", 2);
    //                            return;
    //                        }
    //                    }


    //                    // 2、除了英雄礼包, 其他礼包必须购买前一个，才允许购买当前, 20180301 皮肤礼包不再限制  
    //                    //20180319 新加的soul礼包也不限制   20191212 无畏典藏也不限制  20200306觉醒礼包也不在限制  20200426史诗礼包也不限制
    //                    //20201201 新服庆典不限制
    //                    // 20201013 皮肤精华也限制
    //                    if (tabType != Common.GIFT_PACKAGE_TYPE.GPT_HERO && tabType != Common.GIFT_PACKAGE_TYPE.GPT_SKIN
    //                        && tabType != Common.GIFT_PACKAGE_TYPE.GPT_SOUL && tabType != Common.GIFT_PACKAGE_TYPE.GPT_LIMITED_HERO_LOTTERY 
    //                        && tabType != Common.GIFT_PACKAGE_TYPE.GPT_AWAKEN && tabType != Common.GIFT_PACKAGE_TYPE.GPT_EPIC_BOX_TICKET
    //                        && tabType != GIFT_PACKAGE_TYPE.GPT_HERO_SKIN_ESSENCE && tabType != GIFT_PACKAGE_TYPE.GPT_NEW_SERVER_EVENT )
    //                    {
    //                        // 
    //                        if (this.m_CurrentPage >= 2)
    //                        {
    //                            GiftPackageEventsItem preGift = this.m_CurrentTabGiftBagList[this.m_CurrentPage - 2];
    //                            if (MathUtil.ParseToInt(preGift.packageTimes) > 0)
    //                            {
    //                                PopupManager.Instance.ShowMainTips(string.Format(LocalizationManager.Instance.GetString("KEY.8670"), LocalizationManager.Instance.GetString(preGift.packageType)), 2);
    //                                return;
    //                            }
    //                        }
    //                    }

    //                    if (tabType != Common.GIFT_PACKAGE_TYPE.GPT_LIMITED_HERO_LOTTERY) //20191212无畏典藏可多次购买
    //                    {
    //                        if (MathUtil.ParseToInt(gift.packageTimes) - 1 <= 0)
    //                        {
    //                            this.ui_PayBtn.onClick.RemoveAllListeners();
    //                            this.SetPurchaseButtonState(this.ui_PayBtn, true, "USD" + gift.packageOrderPrice);
    //                        }
    //                    }

    //                    if (DataManager.Instance.PaymentSettings.ContainsKey(MathUtil.ParseToInt(gift.packageKey)))
    //                    {
    //                        PaymentData payData = DataManager.Instance.PaymentSettings[MathUtil.ParseToInt(gift.packageKey)];
    //                        Player.Instance.isMigrationServer = false;

    //                        // 记录下买的是哪个礼包，方便在购买成功后刷新数据和按钮
    //                        this.m_CurrentPurchaseGiftID = gift.packageKey;
    //                        IAPManager.Instance.DoPurchase(LocalizationManager.Instance.GetString(payData.DisplayName),
    //                            payData.Cost.ToString(), payData.ID.ToString(), Config.ServerId,
    //                            Player.Instance.data.userid.ToString(), Config.Uin);
    //                    }
    //                    else
    //                    {
    //                        // 记录下买的是哪个礼包，方便在购买成功后刷新数据和按钮
    //                        this.m_CurrentPurchaseGiftID = gift.packageKey;


    //                        // 无对应支付项并且售出价格确定为0的, 发送直接领取协议
    //                        MessagePublisher.Instance.Subscribe<Server.GiftPackageEventsReply>(this.initGiftPackageData);

    //                        Client.ClientMessage msg = new Client.ClientMessage();
    //                        msg.giftPackageEvents = new Client.GiftPackageEvents();
    //                        msg.giftPackageEvents.claim = new Client.GiftPackageEventsClaim();
    //                        msg.giftPackageEvents.claim.packageId = MathUtil.ParseToInt(gift.packageKey);
    //                        NetworkManager.Instance.SendProtobufMessage(msg);
    //                    }

    //                });
    //            }

    //            //

    //            //if (MathUtil.ParseToInt(gift.extra) != 1)
    //            if (MathUtil.ParseToInt(gift.extra) != GIFT_EXTRA)
    //            {
    //                this.RefreshViewButtonState();
    //            }
    //        }
    //    }


    //    /// <summary>
    //    /// 显示所有道具列表
    //    /// </summary>
    //    /// <param name="gift"></param>
    //    void ShowItemList(GiftPackageEventsItem gift)
    //    {
    //        // Clear 列表
    //        for (int i = ui_ListViewPanel2.transform.childCount - 1; i >= 0; i--)
    //        {
    //            Destroy(ui_ListViewPanel2.transform.GetChild(i).gameObject);
    //        }

    //        // 加载列表
    //        ui_listView.gameObject.SetActive(false);
    //        ui_listView2.gameObject.SetActive(true);


    //        //原来的加载方式（已重构并添加可选礼包）
    //        ////钻石
    //        //GameObject GemsCell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIRewardCell"));
    //        //GemsCell.transform.Find("Line").gameObject.SetActive(true);
    //        //GemsCell.transform.SetParent(ui_ListViewPanel2.transform, false);
    //        //Reward gemreward = new Reward();
    //        //gemreward.id = -1;
    //        //gemreward.amount = int.Parse(gift.packageGetGems);
    //        //gemreward.type = REWARD_TYPE.REWARD_TYPE_DIAMOND;
    //        //UIGiftRewardCell gemrewards = GemsCell.GetComponent<UIGiftRewardCell>();
    //        //gemrewards.SetRewardData(gemreward, showType: RewardShowType.HaveDescStyle, openTip: true,multiplier:gift.packageGetGemsMultiplier);

    //        ////VIP
    //        //int vipExp = DataManager.Instance.PaymentSettings.ContainsKey(MathUtil.ParseToInt(gift.packageKey)) ? DataManager.Instance.PaymentSettings[MathUtil.ParseToInt(gift.packageKey)].VIPExp : 0;

    //        //if(vipExp>0)
    //        //{
    //        //    GameObject VIPCell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIRewardCell"));
    //        //    VIPCell.transform.SetParent(ui_ListViewPanel2.transform, false);
    //        //    Reward vipreward = new Reward();
    //        //    vipreward.id = -int.Parse(gift.packageKey);
    //        //    vipreward.amount = vipExp;
    //        //    vipreward.type = REWARD_TYPE.REWARD_TYPE_DEFAULT;
    //        //    UIGiftRewardCell viprewards = VIPCell.GetComponent<UIGiftRewardCell>();
    //        //    viprewards.SetRewardData(vipreward, showType: RewardShowType.HaveDescStyle, openTip: true);
    //        //}

    //        //bool hasVIP = false;
    //        ////物品
    //        //if (vipExp > 0)
    //        //{
    //        //    hasVIP = true;
    //        //}

    //        //for (int i = 0; i < gift.itemReward.Count; i++)
    //        //{
    //        //    bool showLine = false;
    //        //    if (hasVIP && i % 2 == 0)
    //        //    {
    //        //        showLine = true;
    //        //    }
    //        //    else if (!hasVIP && i % 2 == 1)
    //        //    {
    //        //        showLine = true;
    //        //    }
    //        //    GameObject cell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIRewardCell"));
    //        //    cell.transform.Find("Line").gameObject.SetActive(showLine);
    //        //    cell.transform.SetParent(ui_ListViewPanel2.transform, false);
    //        //    GameData.GearData itemData = DataManager.Instance.Gears[MathUtil.ParseToInt(gift.itemReward[i].itemID)];
    //        //    Reward reward = new Reward();
    //        //    reward.id = itemData.Item_ID;
    //        //    reward.amount = int.Parse(gift.itemReward[i].itemNum);
    //        //    reward.type = REWARD_TYPE.REWARD_TYPE_ITEM;
    //        //    UIGiftRewardCell rewards = cell.GetComponent<UIGiftRewardCell>();
    //        //    rewards.SetRewardData(reward, showType: RewardShowType.HaveDescStyle, openTip: true,multiplier:gift.itemReward[i].multiplier);
    //        //    UICommon.ITEM_TYPE type = UICommon.Instance.GetItemType(itemData.Category);
    //        //    if (type == UICommon.ITEM_TYPE.FRAGMENT || type == UICommon.ITEM_TYPE.SOUL_STONE || type == UICommon.ITEM_TYPE.AWAKESTONE || itemData.Item_ID == 2326)
    //        //    {
    //        //        GameObject highFx = ResourcesLoader.CreateFromPrefab("FX/highlightFX1");
    //        //        UICommon.Instance.ChangeFxShader(highFx);
    //        //        UISpriteAnim anim = highFx.AddComponent<UISpriteAnim>();
    //        //        anim.sortLayer = "PopUI";
    //        //        anim.sortOrder = this.transform.root.gameObject.GetComponent<Canvas>().sortingOrder + 2;

    //        //        highFx.transform.SetParent(cell.transform);
    //        //        highFx.transform.localScale = Vector2.one * 100f;
    //        //        highFx.transform.localPosition = Vector3.zero;
    //        //    }
    //        //}

    //        //钻石数
    //        ShowDimondCell(gift);

    //        //VIP经验点数
    //        ShowVipExp(gift);

    //        //折扣归0
    //        discount = 0;

    //        ////物品
    //        ////DS需求ABTest
    //        ////Test - B测试组
    //        //if (gift.packageGPT == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT)
    //        //{


    //        //    Debug.Log("tab1");
    //        ShowAllItems(gift);
    //        //}
    //        //else
    //        //{
    //        //    Debug.Log("tabOther");
    //        //    //Test - A参照组:原来的直接全部加载显示
    //        //    ShowAllGiftItems(gift);
    //        //}

    //        //黑五折扣
    //        ShowGiftDiscount();

    //        #region 旧的加载方式
    //        /*
    //        // 钻石
    //        GameObject firstCell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIGiftRewardCell"));
    //        firstCell.transform.SetParent(ui_ListViewPanel.transform, false);
    //        GameObject gemsFrame = firstCell.transform.Find("LeftFrame").gameObject;
    //        gemsFrame.transform.Find("ItemName").GetComponent<Text>().text = LocalizationManager.Instance.GetString("KEY.2807");
    //        gemsFrame.transform.Find("ItemNum").GetComponent<Text>().text = gift.packageGetGems;
    //        gemsFrame.transform.Find("ItemIcon").gameObject.SetActive(false);
    //        gemsFrame.transform.Find("VipIcon").gameObject.SetActive(false);
    //        gemsFrame.transform.Find("gemsIcon").gameObject.SetActive(true);
    //        ////VIP经验奖励
    //        //PaymentData payD = DataManager.Instance.PaymentSettings[MathUtil.ParseToInt(gift.packageKey)];
    //        int vipExp = DataManager.Instance.PaymentSettings.ContainsKey(MathUtil.ParseToInt(gift.packageKey))? DataManager.Instance.PaymentSettings[MathUtil.ParseToInt(gift.packageKey)].VIPExp: 0;
    //        if (vipExp > 0)
    //        {
    //            GameObject vipExpCell = firstCell.transform.Find("RightFrame").gameObject;
    //            vipExpCell.transform.Find("ItemName").GetComponent<Text>().text = LocalizationManager.Instance.GetString("KEY.6755");
    //            vipExpCell.transform.Find("ItemNum").GetComponent<Text>().text = vipExp.ToString();
    //            vipExpCell.transform.Find("ItemIcon").gameObject.SetActive(false);
    //            vipExpCell.transform.Find("gemsIcon").gameObject.SetActive(false);
    //            vipExpCell.transform.Find("VipIcon").gameObject.SetActive(true);
    //        }


    //        // Item奖励
    //        GameObject cell = null;
    //        GameObject itemCell = null;
    //        if (vipExp <= 0)
    //        {
    //            cell = firstCell;
    //        }

    //        for (int i = 0; i < gift.itemReward.Count; i++)
    //        {
    //            if (i % 2 == (vipExp > 0 ? 0 : 1))
    //            {
    //                cell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIGiftRewardCell"));
    //                cell.transform.SetParent(ui_ListViewPanel.transform, false);
    //                itemCell = cell.transform.Find("LeftFrame").gameObject;
    //                cell.transform.Find("RightFrame").gameObject.SetActive(i < gift.itemReward.Count - 1);
    //            }
    //            else
    //            {
    //                itemCell = cell.transform.Find("RightFrame").gameObject;
    //            }
    //            if (!DataManager.Instance.Gears.ContainsKey(MathUtil.ParseToInt(gift.itemReward[i].itemID)))
    //            {
    //                Debug.LogError("Can not find itemID:" + gift.itemReward[i].itemID);
    //                continue;
    //            }
    //            GameData.GearData itemData = DataManager.Instance.Gears[MathUtil.ParseToInt(gift.itemReward[i].itemID)];
    //            itemCell.transform.Find("ItemName").GetComponent<Text>().text = itemData.Name;
    //            if (itemData.PlusParam > 0 && itemData.PlusTime > 0 && itemData.PlusType > 0)
    //            {
    //                // 增益道具
    //                if (itemData.PlusType == 1)
    //                {
    //                    // 营地加速道具
    //                    itemCell.transform.Find("ItemName").GetComponent<Text>().text = string.Format(LocalizationManager.Instance.GetString(itemData.Name), itemData.PlusTime);
    //                }
    //            }
    //            itemCell.transform.Find("ItemNum").GetComponent<Text>().text = gift.itemReward[i].itemNum;
    //            itemCell.transform.Find("ItemIcon").gameObject.SetActive(true);
    //            itemCell.transform.Find("gemsIcon").gameObject.SetActive(false);
    //            itemCell.transform.Find("VipIcon").gameObject.SetActive(false);
    //            UICommon.ITEM_TYPE type = UICommon.Instance.GetItemType(itemData.Category);
    //            if (type == UICommon.ITEM_TYPE.FRAGMENT || type == UICommon.ITEM_TYPE.SOUL_STONE || type == UICommon.ITEM_TYPE.AWAKESTONE)
    //            {
    //                GameObject highFx = itemCell.transform.Find("HighLight").gameObject;
    //                UISpriteAnim anim = highFx.AddComponent<UISpriteAnim>();
    //                anim.sortLayer = "PopUI";
    //                anim.sortOrder = this.transform.root.gameObject.GetComponent<Canvas>().sortingOrder + 2;
    //                highFx.SetActive(true);
    //            }


    //            HandleSkinTrain(itemData, itemCell.transform.Find("ItemIcon"));

    //            GameObject Tipcell = UICommon.Instance.LoadUIEquip(itemCell.transform.Find("ItemIcon/UIEquip").gameObject, itemData.Item_ID, 0);
    //            //
    //            this.AddTipsEvent(Tipcell, itemData.Item_ID);
    //        }*/
    //        #endregion
    //    }

    //    /// <summary>
    //    /// 黑五礼包折扣显示
    //    /// </summary>
    //    private void ShowGiftDiscount()
    //    {
    //        Debug.Log("需要显示的折扣是 = " +discount);
    //        bool isMaxActivity = UpComingBuffer.GetInstance().isActivityOpen(UpComingBuffer.UPCOMING_BLACK_FRIDAY);
    //        ui_GiftDiscountImg.gameObject.SetActive(isMaxActivity && discount != 0);
    //        ui_GiftDiscountTxt.text = " +" + (discount - 1) * 100 + "%";
    //    }

    //    /// <summary>
    //    ///原来的展示方式：所有物品都直接展示
    //    /// </summary>
    //    /// <param name="gift"></param>
    //    private void ShowAllGiftItems(GiftPackageEventsItem gift)
    //    {
    //        for (int i = 0; i < gift.itemReward.Count; i++)
    //        {
    //            if (gift.itemReward[i].multiplier != "" && discount < MathUtil.ParseToFloat(gift.itemReward[i].multiplier))
    //            {
    //                discount = MathUtil.ParseToFloat(gift.itemReward[i].multiplier);
    //                Debug.Log("折扣是 = " +discount);
    //            }
    //            bool showLine = false;
    //            if (hasVIP && i % 2 == 0)
    //            {
    //                showLine = true;
    //            }
    //            else if (!hasVIP && i % 2 == 1)
    //            {
    //                showLine = true;
    //            }
    //            GameObject cell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIRewardCellNewServer"));
    //            cell.transform.Find("Line").gameObject.SetActive(showLine);
    //            cell.transform.SetParent(ui_ListViewPanel2.transform, false);

    //            //除可选道具外其它的道具去Gear客户端道具表查找
    //            GameData.GearData itemData = DataManager.Instance.Gears[MathUtil.ParseToInt(gift.itemReward[i].itemID)];
    //            Reward reward = new Reward();
    //            reward.id = itemData.Item_ID;
    //            reward.amount = int.Parse(gift.itemReward[i].itemNum);
    //            reward.type = REWARD_TYPE.REWARD_TYPE_ITEM;
    //            UIGiftRewardCell rewards = cell.GetComponent<UIGiftRewardCell>();
    //            rewards.SetRewardData(reward, showType: RewardShowType.HaveDescStyle, openTip: true,
    //                multiplier: gift.itemReward[i].multiplier);

    //            //添加光环特效
    //            UICommon.ITEM_TYPE type = UICommon.Instance.GetItemType(itemData.Category);
    //            if (type == UICommon.ITEM_TYPE.FRAGMENT || type == UICommon.ITEM_TYPE.SOUL_STONE
    //                || type == UICommon.ITEM_TYPE.AWAKESTONE || itemData.Item_ID == 2326)
    //            {
    //                ShowHighLightAnimFX(cell);
    //            }

    //        }

    //    }

    //    /// <summary>
    //    /// 可选礼包：加载显示所有物品
    //    /// </summary>
    //    /// <param name="gift"></param>
    //    /// <param name="hasVIP"></param>
    //    private void ShowAllItems(GiftPackageEventsItem gift)
    //    {
    //        hasVIP = false;
    //        if (vipExp > 0)
    //        {
    //            hasVIP = true;
    //        }

    //        //道具礼包分成三类加载显示
    //        //一：直接展示的核心物品
    //        List<Server.GiftPackageEventsItemReward> mainItemsReward = new List<GiftPackageEventsItemReward>();
    //        //二：多选一
    //        willChosenItemsReward3 = new List<GiftPackageSpecialSelectData>();

    //        currentGiftPackageID = int.Parse(gift.packageKey);
    //        //每个礼包的各列表需要清空下再打开下一个礼包
    //        customRewardGroups.Clear();
    //        ItemChosenCloneDic.Clear();
    //        customPackageMsg.packageId = currentGiftPackageID;
    //        customPackageMsg.items.Clear();
    //        for (int i = 0; i < gift.itemReward.Count; i++)
    //        {
    //            Debug.Log("下发的所有道具数量 = " + gift.itemReward.Count + " 物品" + i + "的"
    //                + " itemID= " + gift.itemReward[i].itemID + " itemNum=" + gift.itemReward[i].itemNum);
    //            //折扣
    //            if (gift.itemReward[i].multiplier != "" && discount < MathUtil.ParseToFloat(gift.itemReward[i].multiplier))
    //            {
    //                discount = MathUtil.ParseToFloat(gift.itemReward[i].multiplier);
    //                Debug.Log("折扣是 = " + discount);
    //            }

    //            //默认的几个道具
    //            //如果反信的道具ID大于0，则是必选道具，去客户端Gear道具表查找
    //            if (MathUtil.ParseToInt(gift.itemReward[i].itemID) > 0)
    //            {
    //                isAllChosen = true;
    //                bool showLine = false;
    //                //if (hasVIP && i % 2 == 0)
    //                //{
    //                //    showLine = true;
    //                //}
    //                //else if (!hasVIP && i % 2 == 1)
    //                //{
    //                //    showLine = true;
    //                //}
    //                ShowItemCell(showLine, gift.itemReward[i]);

    //                //协议已改，必选的不用上传，只需上传选中的
    //                //selectedRewards.Add(reward);
    //            }
    //            //多选一
    //            //如果反信的道具ID是-29，则说明是可选道具，需要去客户端可选道具表查找
    //            else if (MathUtil.ParseToInt(gift.itemReward[i].itemID) == ITEM_SELECTED)
    //            {
    //                isAllChosen = false;
    //                customRewardGroups.Add(gift.itemReward[i]);
    //                Dictionary<int, GiftPackageSpecialSelectData> giftRewardsCustomGroup =
    //                    DataManager.Instance.GiftPackageSpecialSelects[MathUtil.ParseToInt(gift.itemReward[i].itemNum)];
    //                willChosenItemsReward3.Clear();
    //                foreach (var rewardCustom in giftRewardsCustomGroup.Values)
    //                {
    //                    willChosenItemsReward3.Add(rewardCustom);
    //                }
    //                ShowThirdGiftItems(willChosenItemsReward3, int.Parse(gift.itemReward[i].itemNum), int.Parse(gift.packageKey));
    //            }

    //        }
    //    }

    //    /// <summary>
    //    /// 在下发的物品列表中，按照所选中物品的索引号把相应物品加入到已选清单
    //    /// </summary>
    //    /// <param name="indexValue"></param>
    //    internal void AddSelectedRewards(int indexChosen, int rewardGroupID, int giftPackageID)
    //    {
    //        //已选道具加入待发送列表前先确认下是否是当前礼包ID
    //        if (giftPackageID != currentGiftPackageID)
    //        {
    //            customPackageMsg.items.Clear();
    //        }
    //        //选好一个道具就关闭他的特效
    //        ItemChosenCloneDic[rewardGroupID].transform.Find("fx_ChooseGuide").gameObject.SetActive(false);
    //        Client.CustomPackageItemInfo customPackageItem = new Client.CustomPackageItemInfo();
    //        customPackageItem.itemId = rewardGroupID;
    //        customPackageItem.chooseId = indexChosen + 1;
    //        customPackageMsg.items.Add(customPackageItem);
    //        customPackageMsg.packageId = giftPackageID;
    //        //后面会读列表显示特效，已经选好的就不显示了
    //        if (ItemChosenCloneDic.ContainsKey(rewardGroupID))
    //        {
    //            ItemChosenCloneDic.Remove(rewardGroupID);
    //        }

    //        if (ItemChosenCloneDic.Count == 0)
    //        {
    //            SendGiftChosenReq(customPackageMsg);
    //        }

    //        Debug.Log("下发此礼包中包含分组的数量=" + customRewardGroups.Count + " 选好的道具数量= " +
    //            customPackageMsg.items.Count + " 是否全选好了并收到了反信确认 = " + isAllChosen);
    //    }
    //    /// <summary>
    //    /// 选完所有道具后发送请求
    //    /// </summary>
    //    private void SendGiftChosenReq(CustomPackage customPackageMsg)
    //    {
    //        Client.ClientMessage msg = new Client.ClientMessage();
    //        msg.customPackage = customPackageMsg;
    //        NetworkManager.Instance.SendProtobufMessage(msg);
    //        Debug.Log("发送的请求数据,礼包ID=" + msg.customPackage.packageId + " 礼包中第一组的组ID= " + msg.customPackage.items[0].itemId
    //            + " 第一组中选中道具的索引号=" + msg.customPackage.items[0].chooseId);
    //    }
    //    /// <summary>
    //    ///监听打开第三类可选道具列表面板
    //    /// </summary>
    //    /// <param name="willChosenItemsReward3"></param>
    //    private void ShowThirdGiftItems(List<GiftPackageSpecialSelectData> willChosenItemsReward3, int rewardGroupID, int giftPackageID)
    //    {
    //        //因必选道具不确定种类，可选列表要动态挂载
    //        thirdItemBtn = (GameObject)Instantiate(willChosenItemsBtn3.gameObject);
    //        //thirdItemBtn.transform.localScale = new Vector3(0.35f, 0.35f, 0f);
    //        //thirdItemBtn.transform.SetAsLastSibling();
    //        thirdItemBtn.SetActive(true);
    //        thirdItemBtn.transform.SetParent(ui_ListViewPanel2.transform, false);
    //        ////暂存到一个列表里后面方便用到
    //        //ItemChosenCloneList.Add(thirdItemBtn);
    //        //暂存到一个字典里后面方便用到
    //        Debug.Log("当前已选道具Clone体数量=" + ItemChosenCloneDic.Count);
    //        ItemChosenCloneDic.Add(rewardGroupID, thirdItemBtn);
    //        Debug.Log("当前已选道具Clone体数量=" + ItemChosenCloneDic.Count);

    //        customPackageMsg = new Client.CustomPackage();
    //        //当点击后值改变是触发 
    //        Debug.Log("加载显示Clone体数据 ");
    //        thirdItemBtn.GetComponent<GiftSelectDropdownNewServer>().ShowGiftItemsDropdown(willChosenItemsReward3, rewardGroupID, giftPackageID);
    //    }

    //    /// <summary>
    //    /// 直接展示核心物品: 
    //    /// </summary>
    //    /// <param name="mainItemsReward"></param>
    //    private void ShowMainGiftItems(List<GiftPackageEventsItemReward> mainItemsReward)
    //    {
    //        for (int i = 0; i < mainItemsReward.Count; i++)
    //        {
    //            bool showLine = false;
    //            if (hasVIP && i % 2 == 0)
    //            {
    //                showLine = true;
    //            }
    //            else if (!hasVIP && i % 2 == 1)
    //            {
    //                showLine = true;
    //            }

    //            ShowItemCell(showLine, mainItemsReward[i]);

    //        }
    //    }

    //    /// <summary>
    //    /// 加载展示单个核心物品
    //    /// </summary>
    //    /// <param name="showLine"></param>
    //    /// <param name="giftPackageEventsItemReward"></param>
    //    private void ShowItemCell(bool showLine, GiftPackageEventsItemReward giftPackageEventsItemReward)
    //    {
    //        Debug.Log("奖励列表:main"+giftPackageEventsItemReward.itemID + " " + " " +giftPackageEventsItemReward.itemNum);
    //        GameObject cell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIRewardCellNewServer"));
    //        cell.transform.Find("Line").gameObject.SetActive(showLine);
    //        cell.transform.SetParent(ui_ListViewPanel2.transform, false);
    //        GameData.GearData itemData = DataManager.Instance.Gears[MathUtil.ParseToInt(giftPackageEventsItemReward.itemID)];
    //        Reward reward = new Reward();
    //        reward.id = itemData.Item_ID;
    //        reward.amount = int.Parse(giftPackageEventsItemReward.itemNum);
    //        reward.type = REWARD_TYPE.REWARD_TYPE_ITEM;
    //        UIGiftRewardCell rewards = cell.GetComponent<UIGiftRewardCell>();
    //        //rewards.descNameTxt.color = new Color(0,0,0);
    //        //rewards.descCountTxt.color = new Color(0,150,0);
    //        rewards.SetRewardData(reward, showType: RewardShowType.HaveDescStyle, openTip: true,
    //            multiplier: giftPackageEventsItemReward.multiplier);
    //    }


    //    /// <summary>
    //    /// 显示光环特效
    //    /// </summary>
    //    /// <param name="cell"></param>
    //    private void ShowHighLightAnimFX(GameObject cell)
    //    {

    //        GameObject highFx = ResourcesLoader.CreateFromPrefab("FX/highlightFX1");
    //        UICommon.Instance.ChangeFxShader(highFx);
    //        UISpriteAnim anim = highFx.AddComponent<UISpriteAnim>();
    //        anim.sortLayer = "PopUI";
    //        anim.sortOrder = this.transform.root.gameObject.GetComponent<Canvas>().sortingOrder + 2;
    //        highFx.transform.SetParent(cell.transform);
    //        highFx.transform.localScale = Vector2.one * 100f;
    //        highFx.transform.localPosition = Vector3.zero;
    //    }

    //    /// <summary>
    //    /// 加载显示vip经验值
    //    /// </summary>
    //    /// <param name="gift"></param>
    //    private void ShowVipExp(GiftPackageEventsItem gift)
    //    {
    //        vipExp = DataManager.Instance.PaymentSettings.ContainsKey(MathUtil.ParseToInt(gift.packageKey)) ? DataManager.Instance.PaymentSettings[MathUtil.ParseToInt(gift.packageKey)].VIPExp : 0;

    //        if (vipExp > 0)
    //        {
    //            GameObject VIPCell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIRewardCellNewServer"));
    //            VIPCell.transform.SetParent(ui_ListViewPanel2.transform, false);
    //            Reward vipreward = new Reward();
    //            vipreward.id = -int.Parse(gift.packageKey);
    //            vipreward.amount = vipExp;
    //            vipreward.type = REWARD_TYPE.REWARD_TYPE_DEFAULT;
    //            UIGiftRewardCell viprewards = VIPCell.GetComponent<UIGiftRewardCell>();
    //            //viprewards.descNameTxt.color = new Color(0, 0, 0);
    //            //viprewards.descCountTxt.color = new Color(0, 150, 0);
    //            //viprewards.descCountTxt.GetComponent<Shadow>().effectColor = new Color(15, 115, 10);
    //            viprewards.SetRewardData(vipreward, showType: RewardShowType.HaveDescStyle, openTip: true);
    //            selectedRewards.Add(vipreward);
    //        }

    //    }

    //    /// <summary>
    //    /// 展示钻石
    //    /// </summary>
    //    /// <param name="gift"></param>
    //    private void ShowDimondCell(GiftPackageEventsItem gift)
    //    {
    //        if (int.Parse(gift.packageGetGems) <=0 )
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            GameObject GemsCell = (GameObject)Instantiate(ResourcesLoader.Load("UI/prefab/UIRewardCellNewServer"));
    //            //GemsCell.transform.Find("Line").gameObject.SetActive(true);
    //            GemsCell.transform.SetParent(ui_ListViewPanel2.transform, false);
    //            Reward gemreward = new Reward();
    //            gemreward.id = -1;
    //            gemreward.amount = int.Parse(gift.packageGetGems);
    //            gemreward.type = REWARD_TYPE.REWARD_TYPE_DIAMOND;
    //            UIGiftRewardCell gemrewards = GemsCell.GetComponent<UIGiftRewardCell>();
    //            //gemrewards.descNameTxt.color = new Color(0, 0, 0);
    //            //gemrewards.descCountTxt.color = new Color(0, 150, 0);
    //            gemrewards.SetRewardData(gemreward, showType: RewardShowType.HaveDescStyle, openTip: true, multiplier: gift.packageGetGemsMultiplier);
    //            //加入购买清单
    //            selectedRewards.Add(gemreward);
    //        }

    //    }

    //    //处理皮肤试用
    //    void HandleSkinTrain(GameData.GearData data, Transform tf)
    //    {
    //        UICommon.ITEM_TYPE type = UICommon.Instance.GetItemType(data.Category);
    //        Image Trial = tf.Find("UIEquip/Rank/TrialMark").GetComponent<Image>();

    //        if (type == UICommon.ITEM_TYPE.SKIN && data.PlusTime > 0 && Trial != null)
    //        {
    //            Sprite icon = UIIconLib.Instance.SkinTrialDict[data.Quality];
    //            UICommon.Instance.LoadTexture(Trial, icon);
    //            Trial.gameObject.SetActive(true);
    //        }
    //        else if (Trial != null)
    //        {
    //            Trial.gameObject.SetActive(false);
    //        }
    //    }

    //    //道具信息tips
    //    void AddTipsEvent(GameObject cell, int itemId)
    //    {
    //        Button[] buttonArray = cell.GetComponentsInChildren<Button>();
    //        foreach (Button button in buttonArray)
    //        {
    //            if (button.name == "Rank")
    //            {
    //                UITips.TipData tipData = new UITips.TipData();
    //                tipData.type = UITips.TIPS_TYPE.NOTHERO;
    //                tipData.itemTipData.id = itemId;
    //                UITips.Instance.addItemTipsListener(tipData, button);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 设置购买按钮的显示状态
    //    /// </summary>
    //    /// <param name="button"></param>
    //    /// <param name="isGray"></param>
    //    /// <param name="txt"></param>
    //    public void SetPurchaseButtonState(Button button, bool isGray, string txt)
    //    {

    //        //Shader shader = Shader.Find("UI/Gray");
    //        Image[] imageArray = button.GetComponentsInChildren<Image>(true);
    //        foreach (Image image in imageArray)
    //        {
    //            //image.material = (isGray) ? new Material(shader) : null;
    //            image.SetGrey(isGray);
    //        }

    //        if (isGray)
    //        {
    //            this.ui_TotalPrice.text = "<color=#1e1e1e>" + txt + "</color>";
    //            this.buyFx.gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            this.ui_TotalPrice.text = "<color=#451200>" + txt + "</color>";
    //            this.buyFx.gameObject.SetActive(true);
    //        }
    //    }

    //    private void RefreshViewButtonState()
    //    {
    //        this.ui_PrevBtn.gameObject.SetActive(this.m_CurrentPage > 1);
    //        this.ui_NextBtn.gameObject.SetActive(this.m_CurrentPage < this.m_TotalPage);
    //    }

    //    /// <summary>
    //    /// 根据按钮索引获取按钮key
    //    /// </summary>
    //    /// <param name="index"></param>
    //    /// <returns></returns>
    //    private Common.GIFT_PACKAGE_TYPE GetTabTypeByIndex(int index)
    //    {
    //        Common.GIFT_PACKAGE_TYPE btnKey = Common.GIFT_PACKAGE_TYPE.GPT_NONE;
    //        foreach (var item in this.m_TabTypeDict)
    //        {
    //            if (item.Value == index)
    //            {
    //                btnKey = item.Key;
    //                break;
    //            }
    //        }
    //        return btnKey;
    //    }

    //    /// <summary>
    //    /// 购买礼包成功回调
    //    /// </summary>
    //    public void PurchaseSuccessCallBack()
    //    {
    //        Common.GIFT_PACKAGE_TYPE tabType = this.GetTabTypeByIndex(this.m_CurrentTabIndex);

    //        Debug.LogWarning("++++++++++++++"+tabType);
    //        if (tabType == GIFT_PACKAGE_TYPE.GPT_EQUIP && this.equipPackage != null)
    //        {
    //            List<GiftPackageEventsItem> equipPackageList = this.m_GiftPackageDict[tabType];
    //            this.equipPackage.GetComponent<UIEquipPackage>().InitUI(equipPackageList,true);
    //            return;
    //        }

    //        // 数据更新
    //        foreach (var giftItem in this.m_CurrentTabGiftBagList)
    //        {
    //            Debug.Log("giftItem.packageKey = "+ giftItem.packageKey + " 当前购买礼包ID="+m_CurrentPurchaseGiftID);
    //            if (giftItem.packageKey == this.m_CurrentPurchaseGiftID)
    //            {
    //                giftItem.packageTimes = (MathUtil.ParseToInt(giftItem.packageTimes) - 1).ToString();
    //                Debug.Log(" 当前购买礼包剩余次数="+giftItem.packageTimes);

    //            }
    //        }
    //        // 按钮刷新
    //        GiftPackageEventsItem gift = this.m_CurrentTabGiftBagList[this.m_CurrentPage - 1];

    //        this.SetPurchaseButtonState(this.ui_PayBtn, MathUtil.ParseToInt(gift.packageTimes) <= 0, "USD" + gift.packageOrderPrice);

    //        ui_GiftTimeTxt.text = gift.packageTimes;
    //        if (MathUtil.ParseToInt(gift.packageTimes) > 0)
    //        {
    //            ui_GiftTimeTxt.color = new Color(132f, 226f, 46f, 255f);
    //        }
    //        else
    //        {
    //            ui_GiftTimeTxt.color = new Color(245f, 68f, 43f, 255f);
    //        }

    //        if (tabType == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT 
    //        || tabType == Common.GIFT_PACKAGE_TYPE.GPT_NEW_SERVER_EVENT)
    //        {
    //            // 所有可购买项目 已经售光
    //            bool isCanBuy = false;
    //            List<GiftPackageEventsItem> tmpGiftList = this.m_GiftPackageDict[tabType];
    //            for (int i = 0; i < tmpGiftList.Count; i++)
    //            {
    //                if (MathUtil.ParseToInt(tmpGiftList[i].packageTimes) > 0)
    //                {
    //                    isCanBuy = true;
    //                    break;
    //                }
    //            }

    //            if (isCanBuy == false)
    //            {
    //                if (UIStatus.Instance.giftPackage)
    //                {
    //                    UIStatus.Instance.giftPackage.transform.Find("FX_bxkai/Hot").gameObject.SetActive(false);
    //                }
    //            }
    //        }

    //        //设置单双号AB测试刷新页面
    //        if (MathUtil.ParseToInt(gift.extra) == GIFT_EXTRA)
    //        {
    //            this.m_CurrentPage++;
    //            if (this.m_CurrentPage <= this.m_TotalPage)
    //            {
    //                this.ViewPageInfo(this.m_CurrentPage);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 更新时间
    //    /// </summary>
    //    void Update()
    //    {
    //        if (this.m_CurrentTabGiftBagList == null || this.m_CurrentTabGiftBagList.Count < 1)
    //        {
    //            return;
    //        }

    //        Common.GIFT_PACKAGE_TYPE tabType = this.GetTabTypeByIndex(this.m_CurrentTabIndex);

    //        GiftPackageEventsItem gift = this.m_CurrentTabGiftBagList[this.m_CurrentPage - 1];
    //        long leftTime = MathUtil.ParseToLong(gift.packageRemind) - Local.GetServerTime();
    //        if (leftTime < 0)
    //        {
    //            leftTime = 0;

    //            // 活动周期结束---刷新(英雄礼包+首登礼包)
    //            if (tabType == Common.GIFT_PACKAGE_TYPE.GPT_HERO || tabType == Common.GIFT_PACKAGE_TYPE.GPT_FIRST || tabType == Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT)
    //            {
    //                if (!this.isRefreshing)
    //                {
    //                    GiftPackageManager.Instance.ReOpenGiftPackage(this.GetTabTypeByIndex(this.m_CurrentTabIndex));
    //                    this.isRefreshing = true;
    //                }
    //            }
    //        }

    //        // 注意：跟m_NextRefreshTime操作的要计算时区
    //        long currentTime = Local.TimeToRegisterTimezone(Local.GetServerTime());
    //        // 每日重置--刷新(皮肤礼包+首次登陆礼包)
    //        if (tabType == Common.GIFT_PACKAGE_TYPE.GPT_SKIN || tabType == Common.GIFT_PACKAGE_TYPE.GPT_FIRST)
    //        {
    //            if (currentTime >= this.m_NextRefreshTime)
    //            {
    //                if (!this.isRefreshing)
    //                {
    //                    GiftPackageManager.Instance.ReOpenGiftPackage(this.GetTabTypeByIndex(this.m_CurrentTabIndex));
    //                    this.isRefreshing = true;
    //                }
    //            }
    //        }

    //        // 
    //        long countDownTime = this.m_NextRefreshTime - currentTime;
    //        if (countDownTime < 0)
    //        {
    //            countDownTime = 0;
    //        }
    //        switch (tabType)
    //        {
    //            case Common.GIFT_PACKAGE_TYPE.GPT_HERO:
    //                // 活动时间
    //                this.ui_ActivityTimeLimit.text = limitTimeStr + Local.FormatSecondToTime(leftTime, 3);
    //                break;
    //            case Common.GIFT_PACKAGE_TYPE.GPT_SKIN:
    //                // 重置时间
    //                this.ui_ResetTime.text = Local.FormatTimeWithHour(countDownTime, 3);
    //                this.ui_ActivityTimeLimit.text = limitTimeStr + Local.FormatSecondToTime(leftTime, 3);
    //                break;
    //            case Common.GIFT_PACKAGE_TYPE.GPT_SPECIAL_EVENT:
    //                // 活动时间
    //                //this.ui_ActivityTimeLimit.text = limitTimeStr + Local.FormatSecondToTime(leftTime, 3);
    //                this.ui_ActivityTimeLimit.text =  Local.FormatSecondToTime(leftTime, 3);
    //                break;
    //            case Common.GIFT_PACKAGE_TYPE.GPT_FIRST:
    //                // 活动时间+重置时间
    //                this.ui_ActivityTimeLimit.text = limitTimeStr + Local.FormatSecondToTime(leftTime, 3);
    //                this.ui_ResetTime.text = Local.FormatTimeWithHour(countDownTime, 3);
    //                break;

    //            case Common.GIFT_PACKAGE_TYPE.GPT_LIMITED_HERO_LOTTERY:
    //                this.ui_ActivityTimeLimit.text = limitTimeStr + Local.FormatSecondToTime(leftTime, 3);
    //                break;
    //            case Common.GIFT_PACKAGE_TYPE.GPT_AWAKEN:
    //                this.ui_ActivityTimeLimit.text = limitTimeStr + Local.FormatSecondToTime(leftTime, 3);
    //                break;
    //            case Common.GIFT_PACKAGE_TYPE.GPT_NEW_SERVER_EVENT:
    //                //this.ui_ActivityTimeLimit.text = limitTimeStr + Local.FormatSecondToTime(leftTime, 3);
    //                this.ui_ActivityTimeLimit.text = Local.FormatSecondToTime(leftTime, 3);
    //                break;
    //            default:
    //                break;
    //        }
    //    }

    //    void OnDestroy()
    //    {
    //        MessagePublisher.Instance.Unsubscribe<Server.GiftPackageEventsReply>(this.initGiftPackageData);
    //    }
}

