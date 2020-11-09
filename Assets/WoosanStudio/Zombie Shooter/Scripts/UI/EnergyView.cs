using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 정보를 표시함
    /// MVC 패턴
    /// </summary>
    public class EnergyView : MonoBehaviour
    {
        [Header("[충전 정보를 표시]")]
        public Text RechargeInfo;

        [Header("[충전 백분율 표시]")]
        public Text Progress;

        [Header("[남은 충전 시간]")]
        public Text Time;

        [Header("[남은 충전 시간]")]
        public Slider EnergySlider;

        [Header("[에너지 컨트롤러]")]
        public EnergyPresenter Presenter;

        //캐쉬용
        private WaitForSeconds WFS = new WaitForSeconds(0.33f);

        private IEnumerator Start()
        {
            //0.5s간격으로 에너지 정보 가져옴
            while(true)
            {
                //충전 정보 보여주기
                ShowRechargeTime();
                //충전 진행 상황 보여주기
                ShowRechargeInfo();
                //충전 시간 보여주기
                ShowProgress();
                yield return WFS;
            }
        }

        /// <summary>
        /// 충전 정보 보여주기
        /// </summary>
        void ShowRechargeInfo()
        {
            RechargeInfo.text = Presenter.RechargeInfoText;
        }

        /// <summary>
        /// 충전 진행 상황 보여주기
        /// </summary>
        void ShowProgress()
        {
            EnergySlider.value = Presenter.EnergySliderValue;
            Progress.text = Presenter.ProgressText;
        }

        /// <summary>
        /// 충전 시간 보여주기
        /// </summary>
        void ShowRechargeTime()
        {
            Time.text = Presenter.RechargeTimeText;
        }
    }
}
