using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 플레이어 피곤 이펙트를 표현
    /// </summary>
    public class TiredEffect : MonoBehaviour, IEffect
    {
        static public TiredEffect Instance;

        [Header("[스크린 이미지]")]
        public Image Screen;

        [Header("[스크린 컬러]")]
        public Color Color;

        [Header("[Fade End 값]")]
        public float endValue = 0.6f;

        [Header("[지속시간]")]
        public float duration = 0.2f;

        void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 인터페이스 구현
        /// - 이펙트 보여줌
        /// </summary>
        public void Show()
        {
            //기존 트윈 제거 -> 중복문제 해결
            Screen.DOKill();
            //활성화
            Screen.enabled = true;
            //색깔 세팅
            Screen.color = this.Color;
            //0.5f초 , 알파 0.8f로 한번 요요 트윈
            Screen.DOFade(endValue, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }

        public void Stop()
        {
            //기존 트윈 제거 -> 중복문제 해결
            Screen.DOKill();

            //0.5f초 , 알파 0.8f로 한번 요요 트윈
            Screen.DOFade(0, duration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                //활성화
                Screen.enabled = false;
            });
        }

        /*
        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Show();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Stop();
            }
        }
        #endregion
        */
        
    }
}
