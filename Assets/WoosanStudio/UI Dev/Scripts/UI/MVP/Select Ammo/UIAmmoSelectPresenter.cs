﻿using System.Collections;
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
            int currentIndex = GlobalDataController.AmmoCardStartIndex;

            //카드 순서에의해 발생한 캐릭터 간 
            int ammoIndexInterval = GlobalDataController.AmmoCardStartIndex;

            //현재 모델의 인덱스 가져오기
            currentIndex = GlobalDataController.Instance.SelectedAmmo;

            //카드 데이터 순서에 더하기 때문에 +16필요.
            currentIndex += value;

            //Debug.Log("currentIndex = " + currentIndex + " SelectedCharacter =  " + GlobalDataController.Instance.SelectedCharacter + "   value = " + value);

            int maxIndex = System.Enum.GetValues(typeof(GlobalDataController.AmmoType)).Length;

            //카드 간격 만큼 계산해서 더해주는 작업 필요
            if (currentIndex < 0 + ammoIndexInterval) { currentIndex = 0 + ammoIndexInterval; }
            if (maxIndex + ammoIndexInterval <= currentIndex) { currentIndex = maxIndex - 1 + ammoIndexInterval; }

            //Debug.Log("currentIndex = " + currentIndex + "   characterIndexInterval = " + characterIndexInterval);

            //캐릭터 변경 통지 => 변경시 필요한 인덱스는 -16뺀 0 부터 13까지임.
            //Debug.Log("Call !!! => " + (currentIndex - characterIndexInterval));
            ChangeAmmoEvent.Invoke(currentIndex - ammoIndexInterval);

            //* 죽인 몬스터 및 플레이타임 가져오는 부분 필요
            UpdateInfoEvent.Invoke(Model.cardSettings[currentIndex]);

            //캐릭터 사용 가능 여부 발생
            UpdateUseAbleEvent.Invoke(Model.cardSettings[currentIndex].UseAble);

            //구매 뷰 업데이트 통지
            AmmoPurchaseActivationEvent.Invoke(Model.cardSettings[currentIndex]);

            //변경 된 인덱스 모델 데이터에 넣음
            GlobalDataController.Instance.SelectedAmmo = currentIndex;
        }
    }
}
