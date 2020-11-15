using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 버튼 활성 비활성 컨트롤
    /// </summary>
    public class UIButtonActivator : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        [Header("[비활성화 알파값]")]
        public float Alpha = 0.25f;

        /// <summary>
        /// 버튼 활성화
        /// </summary>
        public void Activate(bool value)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = value;

            //연출 중지
            transform.DOKill();
            transform.localScale = Vector3.one;

            if (value)
            { 
                canvasGroup.alpha = 1f;
                
                //스케일 트윈 연출
                transform.DOScale(1.1f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            } else
            {
                canvasGroup.alpha = Alpha;
            }
        }
    }
}
