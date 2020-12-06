using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Player
{
    /// <summary>
    /// 플레이어 변경
    /// </summary>
    public class CharacterSwaper : MonoBehaviour
    {
        [Header("[모델 변경용 컨트롤러]")]
        public CharacterModelController modelController;

        [Header("[[Auto] 캐릭터 카드 확인용]")]
        public CardSetting cardSetting;

        /// <summary>
        /// 탄약 변경 명령
        /// </summary>
        public void Swap(CardSetting cardSetting)
        {
            //캐릭터 세팅
            modelController.ChangedCharacter(3);
        }

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //캐릭터 세팅
                modelController.ChangedCharacter(3);
            }
        }
        #endregion

    }
}
