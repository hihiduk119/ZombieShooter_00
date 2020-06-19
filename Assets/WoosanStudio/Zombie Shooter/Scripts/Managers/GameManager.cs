using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 게임 전체를 통합 관리 한다.
    ///
    /// 1.캐릭터는 몇명 생성 할지
    /// 2.몇번째 스테이지로 씬을 보낼지
    /// 
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public LevelSwapController LevelSwapController;

        /// <summary>
        /// 해당 레벨로 초기화 시킴
        /// </summary>
        /// <param name="level"></param>
        public void Initialize(int level)
        {
            
        }

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

            }

            if (Input.GetKeyDown(KeyCode.S))
            {
            }
        }
        #endregion

    }
}
