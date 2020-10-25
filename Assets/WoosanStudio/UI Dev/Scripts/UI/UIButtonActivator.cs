using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

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

            if (value)
            { 
                canvasGroup.alpha = 1f;
            } else
            {
                canvasGroup.alpha = Alpha;
            }
        }
    }
}
