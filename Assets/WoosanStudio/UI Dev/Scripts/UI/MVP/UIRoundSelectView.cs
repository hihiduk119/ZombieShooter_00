using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 라운드 선택 뷰
    /// </summary>
    public class UIRoundSelectView : MonoBehaviour
    {
        [Header("[라운드 다운 이벤트]")]
        public UnityEvent RoundDownEvent = new UnityEvent();

        [Header("[라운드 업 이벤트]")]
        public UnityEvent RoundUpEvent = new UnityEvent();

        [Header("[라운드 강 다운 이벤트]")]
        public UnityEvent RoundStrongDownEvent = new UnityEvent();

        [Header("[라운드 강 업 이벤트]")]
        public UnityEvent RoundStrongUpEvent = new UnityEvent();

        [Header("[라운드 타이틀]")]
        public Text Title;

        [Header("[왼쪽 버튼들]")]
        public List<CanvasGroup> Lefts = new List<CanvasGroup>();

        [Header("[오른쪽 버튼들]")]
        public List<CanvasGroup> Rights = new List<CanvasGroup>();

        [Header("[라운드 카운트]")]
        public Text Round;

        public void LeftClick()
        {
            RoundDownEvent.Invoke();
        }

        public void RightClick()
        {
            RoundUpEvent.Invoke();
        }

        public void DoubleLeftClick()
        {
            RoundStrongDownEvent.Invoke();
        }

        public void DoubleRightClick()
        {
            RoundStrongUpEvent.Invoke();
        }

        /// <summary>
        /// 에너지 연출
        /// </summary>
        public void EffectRound()
        {
            //라운드 연출
            Round.transform.DOKill();
            Round.transform.localScale = Vector3.one;
            Round.transform.DOScale(1.4f, 0.1f).SetLoops(2, LoopType.Yoyo);

            //타이틀 연출
            Title.transform.DOKill();
            Title.transform.localScale = Vector3.one;
            Title.transform.DOScale(1.4f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }

        /// <summary>
        /// 라운드 카운트
        /// </summary>
        public void SetRound(string text)
        {
            Round.text = text;
        }

        /// <summary>
        /// 왼쪽 버튼들 활성 & 비활성
        /// </summary>
        /// <param name="value"></param>
        public void SetLeftButtons(bool value)
        {
            if (value)
            {

                for (int i = 0; i < Lefts.Count; i++)
                {
                    Lefts[i].DOKill();
                    Lefts[i].alpha = 0;
                    Lefts[i].DOFade(1, 0.1f);
                }
                
            }
            else
            {
                for (int i = 0; i < Lefts.Count; i++)
                {
                    Lefts[i].DOKill();
                    Lefts[i].alpha = 1;
                    Lefts[i].DOFade(0, 0.1f);
                }
               
            }
        }

        /// <summary>
        /// 오른쪽 버튼들 활성 & 비활성
        /// </summary>
        /// <param name="value"></param>
        public void SetRightButtons(bool value)
        {
            if(value)
            {
                for (int i = 0; i < Rights.Count; i++)
                {
                    Rights[i].DOKill();
                    Rights[i].alpha = 0;
                    Rights[i].DOFade(1, 0.1f);
                }
               
            } else
            {
                for (int i = 0; i < Rights.Count; i++)
                {
                    Rights[i].DOKill();
                    Rights[i].alpha = 1;
                    Rights[i].DOFade(0, 0.1f);
                }
                
            }
        }

        //#region [-TestCode]
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        SetLeftButtons(true);
        //        SetRightButtons(true);

        //    }
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        SetLeftButtons(false);
        //        SetRightButtons(false);
        //    }
        //}
        //#endregion 
    }
}
