using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.UI.MVP.InGameCardSelect
{
    /// <summary>
    /// 카드 선택 팝업
    /// *MVP 모델
    /// </summary>
    public class PopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public PopupView View;

        [Header("[[현재 임시로 넣어둠]변경하려는 카드 => 반듯이 3개이상 있어야 함]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();

        [Header("[현재 선택된 카드 인덱스]")]
        public int CardIndex = 0;
    }
}
