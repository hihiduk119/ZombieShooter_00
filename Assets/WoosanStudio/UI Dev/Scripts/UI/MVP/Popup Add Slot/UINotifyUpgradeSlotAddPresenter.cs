using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 업그레이드 슬롯 추가 팝업
    /// *MVP 모델
    /// </summary>
    public class UINotifyUpgradeSlotAddPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINotifyUpgradeSlotAddView View;

        //전달 받은 가격
        public int price;

        /// <summary>
        /// 활성화시 바로 업데이트
        /// </summary>
        private void OnEnable()
        {
            UpdateView();
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        private void UpdateView()
        {
            View.UpdateView(price.ToString());
        }
    }
}
