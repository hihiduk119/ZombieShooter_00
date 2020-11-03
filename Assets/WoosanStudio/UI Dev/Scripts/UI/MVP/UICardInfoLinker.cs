using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 슬롯과 카드 정보 
    /// </summary>
    public class UICardInfoLinker : MonoBehaviour
    {
        [Header("[전달하는 역활 프리젠트]")]
        public UICardSlotInfoPresenter ToPresenter;

        [Header("[전달받는 역활 프리젠트를 추출 하는 주체]")]
        public PopupOpener openPopup;

        [Header("[[Auto->Awake()] 전달받는 역활 프리젠트]")]
        public UICardResearchInfoPopupPresenter FromPresenter;

        private void Awake()
        {
            //openPopup에서 가져옴
            FromPresenter = openPopup.popupPrefab.GetComponent<UICardResearchInfoPopupPresenter>();
        }

        /// <summary>
        /// 연결된 두 프리젠트의 데이터를 전달
        /// </summary>
        public void Send()
        {
            Debug.Log("전달 완료");
            if (ToPresenter.CardSetting == null) { Debug.Log("카드 데이터가 NULL 이다"); }
            //카드 세팅 데이터 전달
            FromPresenter.CardSetting = ToPresenter.CardSetting;
        }
    }
}
