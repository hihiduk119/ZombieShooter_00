using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// Ammo 선택
    /// *MVP패턴
    /// </summary>
    public class UIAmmoSelectPresenter : MonoBehaviour
    {
        [Header("[MVP 모델]")]
        public UICardModel Model;
        [Header("[돈이 없음 팝업 오프너]")]
        public PopupOpener NotifyPopupOpener;
        [Header("[돈이 있고 최종 확인용 오프너]")]
        public PopupOpener NotifyYesOrNoPopupOpener;

        [System.Serializable]
        public class UpdateAmmo : UnityEvent<int> { }
        [Header("[탄약 업데이트 이벤트]")]
        public UpdateAmmo ChangeAmmoEvent = new UpdateAmmo();

        [System.Serializable]
        public class UpdateData : UnityEvent<CardSetting> { }
        [Header("[탄약 가격 창 이벤트]")]
        public UpdateData AmmoPurchaseActivationEvent = new UpdateData();

        [Header("[건 정보 이벤트]")]
        public UpdateData UpdateInfoEvent = new UpdateData();

        [System.Serializable]
        public class UpdateUseAble : UnityEvent<bool> { }
        [Header("[스타트 버튼 사용가능 이벤트]")]
        public UpdateUseAble UpdateUseAbleEvent = new UpdateUseAble();

        private void Start()
        {
            //시작시 0번으로 최기화
            Change(0);
        }

        /// <summary>
        /// 모델을 변경함
        /// 데이터 Invoke
        /// 인포메이션 뷰에 데이터 전달
        /// </summary>
        /// <param name="type"></param>
        public void Change(int value)
        {
            Debug.Log("변경!! [" + value + "]");
        }
    }
}
