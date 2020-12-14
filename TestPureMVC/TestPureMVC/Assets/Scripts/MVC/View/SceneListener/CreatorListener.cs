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
using BaseMVCFrame.Manager;
using BaseMVCFrame.Data;

namespace BaseMVCFrame
{
    /// <summary>
    /// 创造监听者
    /// </summary>
	public class CreatorListener:MonoBehaviour
	{
        public static CreatorListener _Instance;
        /// <summary>
        /// 要克隆的预设对象
        /// </summary>
        public CharacterControl _Clone_CharacterControl;

        void Awake()
        {
            _Instance = this;
        }

        private void Start()
        {
            CreatePlayerCharacters();
        }

        /// <summary>
        /// 创建玩家的角色
        /// </summary>
        public void CreatePlayerCharacters()
        {
            List<Character> characters = CreationGod.Instance.AllPlayerCharacter;
            for (int i = 0;i< characters.Count;i++)
            {
                CharacterControl cc = CloneCharacter();
                characters[i]._CharacterControl = cc;
            }
        }
        /// <summary>
        /// 克隆一个角色UI
        /// </summary>
        /// <returns></returns>
        private CharacterControl CloneCharacter()
        {
            CharacterControl characterControl = Instantiate(_Clone_CharacterControl);
            characterControl.Show();
            return characterControl;
        }
    }
}
