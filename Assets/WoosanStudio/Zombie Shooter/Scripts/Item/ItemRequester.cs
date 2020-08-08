using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 아이템 팩토리에서 아이템을 요청
    /// </summary>
    public class ItemRequester : MonoBehaviour
    {
        //아이템 팩토리
        public ItemFactory ItemFactory;

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                ItemFactory.Make(0);
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
            }
        }
        #endregion

    }
}
