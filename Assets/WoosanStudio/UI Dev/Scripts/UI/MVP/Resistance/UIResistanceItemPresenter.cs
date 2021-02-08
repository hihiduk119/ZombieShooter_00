using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 저항 아이템 프리젠터
    /// *MVP 모델
    /// </summary>
    public class UIResistanceItemPresenter : MonoBehaviour
    {
        [Header("[아이템이 이동할 피벗]")]
        public UIResistanceItemView View;

        [Header("[[Auto] 정보 업데이트용 프로퍼티]")]
        public CardProperty cardProperty;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="icon"></param>
        public void UpdateInfo(int level)
        {
            //레벨이 반영된 실제 값 가져오기
            string description = cardProperty.GetValueByUpdatedLevel(level).ToString() + "%";

            View.UpdateInfo(cardProperty.Title, description, cardProperty.Icon);
        }
    }
}
