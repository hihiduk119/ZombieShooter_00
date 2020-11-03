using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 슬롯 프리젠터
    /// 카드 슬롯 뷰에 어떤 것을 보여줄지 제어
    /// *MPV 모델
    /// </summary>
    public class UICardSlotInfoPresenter : MonoBehaviour
    {
        [Header("[[Auto->Awake()] 카드정보 뷰]")]
        public UICardSlotInfoView View;
        [Header("[[Auto->Awake()] 카드정보 프리젠터]")]
        public UICardModel Model;

        //카드 세팅 데이터
        private CardSetting cardSetting;
        public CardSetting CardSetting { get => cardSetting; set => cardSetting = value; }

        //1.업글 중이라면 연구 상황 업데이트 -> 취소버튼 활성화
        //2.업글 중이 아니라면 업글버튼 활성화
        //3.슬롯이 비어 있다면 비어있다 표시

        IEnumerator UpdateInfoCoroutine;

        /// <summary>
        /// 카드의 정보 상태
        /// </summary>
        public enum State
        {
            Empty,      //비어있음.
            SelectAndUpgrading, //선택됐고 업그레이드중
            Select,             //선택만 됐음.
        }

        private void Start()
        {
            View = GameObject.FindObjectOfType<UICardSlotInfoView>();
            Model = GameObject.FindObjectOfType<UICardModel>();
        }

        /// <summary>
        /// 해당 슬롯을 업데이트 명령
        /// </summary>
        /// <param name="cardSetting"></param>
        public void UpdateInfo(CardSetting cardSetting)
        {
            //업데이트 명령을 받으면 스스로 가지고 있는 카드 업데이트
            this.cardSetting = cardSetting;

            //업그레이드 중인지 아닌지 알아옴
            bool isUpgrading;

            State state = State.Empty;

            //현재 슬롯이 비어있는 상태
            if(cardSetting == null) { state = State.Empty;}
            else {//슬롯이 선택됐다면
                isUpgrading = cardSetting.UpgradeTimeset.bUpgrading;
                //Debug.Log("is Upgrade ?? => " + isUpgrading);

                if (isUpgrading) {//업그레이드 중임
                    state = State.SelectAndUpgrading;
                } else {//업그레이드 중이 아님
                    state = State.Select;
                }
            }

            //실제 뷰에서 보여줌
            View.UpdateInfoListener(cardSetting, state);
        }
    }
}
