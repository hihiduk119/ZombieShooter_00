using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 버튼 활성 비활성 컨트롤
    /// </summary>
    public class UIStartButtonView : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        [Header("[비활성화 알파값]")]
        public float Alpha = 0.25f;

        [Header("[에너지 값]")]
        public Text Energy;

        [Header("[시작 버튼 클릭]")]
        public UnityEvent StartClickEvent = new UnityEvent();

        /// <summary>
        /// 버튼 활성화
        /// </summary>
        public void UpdateValue(bool value)
        {
            //Debug.Log("UIButtonActivator -> value = " + value);
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

        /// <summary>
        /// 사용 되는 에너지 업데이트
        /// </summary>
        /// <param name="text"></param>
        public void UpdateEnergy(string text)
        {
            Energy.text = "x " + text;
        }

        /// <summary>
        /// 에너지 연출
        /// </summary>
        public void EffectEnergy()
        {
            Energy.transform.DOKill();
            Energy.transform.localScale = Vector3.one;
            Energy.transform.DOScale(1.4f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }

        /// <summary>
        /// 시작 버튼 눌림
        /// </summary>
        public void StartClick()
        {
            StartClickEvent.Invoke();
        }

        /*#region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                value(true);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                value(false);
            }
        }
        #endregion*/

    }
}
