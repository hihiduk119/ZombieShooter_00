using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 가이드 뷰 
    /// *MVP 모델
    /// </summary>
    public class UIGuideView : MonoBehaviour
    {
        //캐쉬용
        private CanvasGroup canvasGroup;

        [Header("[아이템이 이동할 피벗]")]
        public Transform[] Pivots;

        //public 

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// 활성 또는 비활성
        /// </summary>
        public void SetActivate(bool value)
        {
            if (value)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(1f, 0.25f);
            }
            else
            {
                canvasGroup.alpha = 1f;
                canvasGroup.DOFade(0f, 0.25f);
            }
        }
    }
}
