using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 데미지 이펙트를 표현
    /// </summary>
    public class GainItemEffect : MonoBehaviour, IEffect
    {
        [Header("[스크린 이미지]")]
        public Image Screen;

        [Header("[스크린 컬러]")]
        public Color Color;

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
            //한번 요요 트윈
            Screen.DOFade(0.35f, 0.3f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
                //완료후 비활성화
                Screen.enabled = false;
            }
            ); ;
        }

        //#region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        Show();
        //    }
        //}
        //#endregion

    }
}
