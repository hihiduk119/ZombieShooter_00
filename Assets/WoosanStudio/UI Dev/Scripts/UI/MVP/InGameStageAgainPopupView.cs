using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 스테이지 결과 한번더 팝업
    /// *MVP 모델
    /// </summary>
    public class InGameStageAgainPopupView : MonoBehaviour
    {
        [Header("[스코어]")]
        public Text Gem;

        [Header("[결과버튼 캔버스]")]
        public CanvasGroup ADS_Button;

        [Header("[결과버튼 캔버스]")]
        public CanvasGroup GemButton;

        [Header("[결과버튼 캔버스]")]
        public CanvasGroup ResultButton;

        //광고 버튼 클릭 이벤트
        [HideInInspector]
        public UnityEvent ClickADS_Event;

        //보석 버튼 클릭 이벤트
        [HideInInspector]
        public UnityEvent ClickGemEvent;

        //보석 버튼 클릭 이벤트
        [HideInInspector]
        public UnityEvent ClickResultEvent;

        private void Awake()
        {
            //광고 버튼 .2f초 후에 등장
            //ADS_Button.DOFade(1, 0.2f).SetDelay(.2f);
            //보석 버튼 .7f초 후에 등장
            //GemButton.DOFade(1, 0.2f).SetDelay(.7f);
            //결과 버튼 1.5f초 후에 등장
            //ResultButton.DOFade(1, 0.2f).SetDelay(2.5f);
        }

        /// <summary>
        /// 보석 업데이트
        /// </summary>
        /// <param name="gem"></param>
        public void UpdateGem(int gem)
        {
            Gem.text = "x " + gem.ToString();
        }

        /// <summary>
        /// 광고 버튼 클릭
        /// </summary>
        public void ClickADS()
        {
            ClickADS_Event.Invoke();
        }

        /// <summary>
        /// 보석 버튼 클릭
        /// </summary>
        public void ClickGem()
        {
            ClickGemEvent.Invoke();
        }

        /// <summary>
        /// 결과 버튼 클릭
        /// </summary>
        public void ClickResult()
        {
            ClickResultEvent.Invoke();
        }
    }
}
