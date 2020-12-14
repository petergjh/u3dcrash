/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using BaseMVCFrame.Data;
using BaseMVCFrame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BaseMVCFrame.UI
{
	public class SoulTempleUI : BaseUI
	{
        public new const string UIName = "SoulTempleUI";
        /// <summary>
        /// 召唤灵魂按钮
        /// </summary>
        private Button m_CallSoulBtn;
        /// <summary>
        /// 召唤的角色入队按钮
        /// </summary>
        private Button m_AddTeamBtn;
        /// <summary>
        /// 开始按钮
        /// </summary>
        private Button m_StartBtn;
        /// <summary>
        /// 名字盒子
        /// </summary>
        private Transform m_NameBox;

        /// <summary>
        /// 当前选择/召唤的角色
        /// </summary>
        private Character m_CurrentCT;

        protected override void Initialization()
        {
            m_CallSoulBtn = transform.GetChild<Button>("CallSoulBtn");
            m_AddTeamBtn = transform.GetChild<Button>("AddTeamBtn");
            m_StartBtn = transform.GetChild<Button>("StartBtn");
            //初始设置按钮不可点击
            m_AddTeamBtn.interactable = false;

            m_NameBox = transform.GetChild("NameBox");
            //修改名字的输入框
            InputField InputName = m_NameBox.GetChild<InputField>("InputName");
            InputName.onValueChanged.AddListener((string Name)=> 
            {
                if (Name.IsNotNullAndEmpty() && m_CurrentCT.IsNotNull())
                {
                    Character CT = m_CurrentCT;
                    if (CT.IsNotNull())
                    {
                        CT.SetCharacterName(Name);
                        InputName.text = Name;
                        Debug.Log("更新了角色名字:"+Name);
                    }
                }
            });
        }

        protected override void InitButtonListener()
        {
            m_CallSoulBtn.onClick.AddListener(delegate() 
            {
                m_CurrentCT = CreationGod.Instance.CreateOneCharacter(CampType.Player);
                ShowCallSoulInfo(m_CurrentCT);
                //设置入队按钮可点击
                m_AddTeamBtn.interactable = true;
            });

            m_AddTeamBtn.onClick.AddListener(delegate() 
            {
                if (CreationGod.Instance.MarkCharacter(m_CurrentCT))
                {
                    //如果入队成功了则设置按钮不可点击
                    m_AddTeamBtn.interactable = false;

                    CreationGod.Instance.TestShowAllCharcter();
                }
            });

            m_StartBtn.onClick.AddListener(delegate() 
            {
                SceneManager.LoadScene("StartScene");
            });
        }

        /// <summary>
        /// 显示召唤的灵魂的信息
        /// </summary>
        /// <param name="CT"></param>
        private void ShowCallSoulInfo(Character CT)
        {
            CharacterAttCenter attCenter = CT.GetCharacterAttCenter();
            Transform trans_SoulInfo = transform.GetChild("SoulInfo");
            trans_SoulInfo.GetChild<Text>("STRText").text = "力量:"+ attCenter.m_STR.ToString();
            trans_SoulInfo.GetChild<Text>("MAGText").text = "魔力:"+ attCenter.m_MAG.ToString();
            trans_SoulInfo.GetChild<Text>("CONText").text = "体力:"+attCenter.m_CON.ToString();
            trans_SoulInfo.GetChild<Text>("INTText").text = "智力:"+attCenter.m_INT.ToString();
            trans_SoulInfo.GetChild<Text>("AGLText").text = "敏捷:"+attCenter.m_AGL.ToString();
            trans_SoulInfo.GetChild<Text>("AGEText").text = "年龄:"+attCenter.m_AGE.ToString();
            trans_SoulInfo.GetChild<Text>("SANText").text = "San值:"+attCenter.m_MaxSAN.ToString();

            Transform trans_SoulSecondAttInfo = transform.GetChild("SoulSecondAttInfo");
            trans_SoulSecondAttInfo.GetChild<Text>("MaxHP").text = "血量:" + attCenter.m_MaxHP;
            trans_SoulSecondAttInfo.GetChild<Text>("MaxMP").text = "魔力值:" + attCenter.m_MaxMP;
            trans_SoulSecondAttInfo.GetChild<Text>("MoveSpeed").text = "移动速度:" + attCenter.m_MoveSpeed.ToString("0.00");
            trans_SoulSecondAttInfo.GetChild<Text>("AttackRate").text = "攻击速度:" + attCenter.m_AttackRate.ToString("0.00");
            trans_SoulSecondAttInfo.GetChild<Text>("PhyAttack").text = "物理攻击:" + attCenter.m_PhysicalAttack;
            trans_SoulSecondAttInfo.GetChild<Text>("MagAttack").text = "魔法攻击:" + attCenter.m_MagicAttack;
            trans_SoulSecondAttInfo.GetChild<Text>("MagSingRate").text = "吟唱速度:" + attCenter.m_MagicSingRate.ToString("0.00");
            trans_SoulSecondAttInfo.GetChild<Text>("PhyDef").text = "物理防御:" + attCenter.m_PhysicalDef;
            trans_SoulSecondAttInfo.GetChild<Text>("MagDef").text = "魔法防御:" + attCenter.m_MagicDef;
            trans_SoulSecondAttInfo.GetChild<Text>("PhyCrit").text = "物理暴击:" + attCenter.m_PhysicalCrit.ToString("0.00")+"%";
            trans_SoulSecondAttInfo.GetChild<Text>("MagCrit").text = "魔法暴击:" + attCenter.m_MagicCrit.ToString("0.00")+"%";
        }

    }
}
