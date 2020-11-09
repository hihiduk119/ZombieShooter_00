using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 확인 최종 통지
    /// *MVP 모델
    /// </summary>
    public class UINotifyPopupLinker : MonoBehaviour
    {
        [Header("[모델 역활]")]
        public UIShopChargesAllEnergyPopupPresenter ToModel;

        [Header("[전달받는 역활 프리젠트를 추출 하는 주체]")]
        public PopupOpener openPopup;

        [Header("[[Auto->Awake()] 전달받는 역활 프리젠트]")]
        public UINotifyPopupPresenter FromPresenter;

        /// <summary>
        /// 연결된 두 프리젠트의 데이터를 전달
        /// </summary>
        public void Send()
        {
            //openPopup에서 가져옴
            FromPresenter = openPopup.popupPrefab.GetComponent<UINotifyPopupPresenter>();
            Debug.Log("전달 완료");
            if (ToModel.Model.data == null) { Debug.Log("카드 데이터가 NULL 이다"); }
            //카드 세팅 데이터 전달
            FromPresenter.data = ToModel.Model.data;
        }
    }
}
