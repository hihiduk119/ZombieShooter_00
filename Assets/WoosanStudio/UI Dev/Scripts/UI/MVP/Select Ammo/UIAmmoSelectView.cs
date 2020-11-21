using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// Ammo 선택
    /// *MVP패턴
    /// </summary>
    public class UIAmmoSelectView : MonoBehaviour
    {
        [Header("[MVP Presenter]")]
        public UIAmmoSelectPresenter Presenter;

        [Header("[선택 버튼]")]
        public Transform[] Btns;

        /// <summary>
        /// 왼쪽 이동 클릭
        /// </summary>
        public void LeftClick()
        {
            Presenter.Change(-1);
        }

        /// <summary>
        /// 오른쪽 이동 클릭
        /// </summary>
        public void RightClick()
        {
            Presenter.Change(1);
        }

        /// <summary>
        /// 선택 버튼 연출 이팩트
        /// </summary>
        public void UpdateButton(bool value)
        {
            Color tempColor;
            for (int i = 0; i < Btns.Length; i++)
            {
                //연출 중지
                Btns[i].DOKill();
                Btns[i].localScale = Vector3.one;

                if (value)//투명화
                {
                    tempColor = Btns[i].GetComponent<Image>().color;
                    tempColor.a = 0f;
                    Btns[i].GetComponent<Image>().color = tempColor;
                }
                else //빨간색 연출
                {
                    tempColor = Btns[i].GetComponent<Image>().color;
                    tempColor = new Color32(255, 255, 255, 100);
                    Btns[i].GetComponent<Image>().color = tempColor;

                    //스케일 트윈 연출
                    Btns[i].DOScale(1.25f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                }
            }
        }
    }
}
