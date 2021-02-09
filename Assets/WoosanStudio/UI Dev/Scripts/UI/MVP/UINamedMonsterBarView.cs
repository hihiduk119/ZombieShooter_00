using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 네임드 바 뷰어
    /// *MVP 모델
    /// </summary>
    public class UINamedMonsterBarView : MonoBehaviour
    {
        [Header("[저항 텍스트]")]
        public CanvasGroup ResistanceCanvasGroup;

        //캐쉬용
        private CanvasGroup canvasGroup;

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

        /// <summary>
        /// 저항 글자 활성 또는 비활성
        /// </summary>
        /// <param name="value"></param>
        public void SetActivateByResistanceText(bool value)
        {
            if (value)
            {
                ResistanceCanvasGroup.alpha = 0f;
                ResistanceCanvasGroup.DOFade(1f, 0.25f);
            }
            else
            {
                ResistanceCanvasGroup.alpha = 1f;
                ResistanceCanvasGroup.DOFade(0f, 0.25f);
            }

        }
    }
}
