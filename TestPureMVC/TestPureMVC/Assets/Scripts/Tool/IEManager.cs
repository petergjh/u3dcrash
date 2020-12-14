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

namespace BaseMVCFrame
{
	public class IEManager : MonoBehaviour
	{
        public static IEManager m_Instance;

        private void Awake()
        {
            m_Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void StartIE(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }


    }
}
