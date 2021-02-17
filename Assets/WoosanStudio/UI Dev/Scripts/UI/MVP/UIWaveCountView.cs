using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 웨이브 카운팅
    /// *MVP 모델
    /// </summary>
    public class UIWaveCountView : MonoBehaviour
    {
        [Header("[웨이브 타이틀")]
        public Text Title;

        [Header("[웨이브 카운트")]
        public Text Count;

        //캐쉬용
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        /// <param name="count">카운팅 값</param>
        public void UpdateView(string count)
        {
            Count.text = count;
        }

        /// <summary>
        /// 활성 또는 비활성
        /// </summary>
        public void SetActivate(bool value)
        {
            if(value)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(1f, 0.25f);
            } else
            {
                canvasGroup.alpha = 1f;
                canvasGroup.DOFade(0f, 0.25f);
            }
        }

        /// <summary>
        /// 웨이브 카운트
        /// </summary>
        /// <param name="count"></param>
        public void WaveCount(int count)
        {
            Title.text = "WAVE " + count.ToString();
        }

        /// <summary>
        /// 웨이브 카운트 한번 튕김
        /// </summary>
        public void BounceCount()
        {
            //중복을 피하기 위해 트윅 죵료
            Count.transform.DOKill();
            //중복 문제 해결을 위해 스케일 초기화
            Count.transform.localScale = Vector3.one;
            //한번 퉁김 트윈
            Count.transform.DOScale(2f, 0.1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}
