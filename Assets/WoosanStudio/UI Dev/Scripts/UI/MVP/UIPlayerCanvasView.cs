using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;


namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 플레이어를 따라다는 UI 캔버스
    ///
    /// *MVP 모델
    /// </summary>
    public class UIPlayerCanvasView : MonoBehaviour
    {
        //캐쉬용
        private CanvasGroup canvasGroup;

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
