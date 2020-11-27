using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 슬롯 프리젠터
    /// 카드 슬롯 뷰에 어떤 것을 보여줄지 제어
    /// *MVP 모델
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

        //캐쉬용
        private WaitForSeconds WFS = new WaitForSeconds(0.33f);
        private Coroutine updateUpgradeRemainTimeCoroutine;

        /// <summary>
        /// 카드의 정보 상태
        /// </summary>
        public enum State
        {
            Empty,              //비어있음.
            SelectAndUpgrading, //선택됐고 업그레이드중
            Select,             //선택만 됐음.
            Lock,               //현재 사용 못함
        }

        private void Start()
        {
            View = GameObject.FindObjectOfType<UICardSlotInfoView>();
            Model = GameObject.FindObjectOfType<UICardModel>();
        }

        private void OnDisable()
        {
            //코루틴 동작 중이라면 즉시 제거 -> 코루틴 중복을 막기위해
            if (updateUpgradeRemainTimeCoroutine != null) { StopCoroutine(updateUpgradeRemainTimeCoroutine); }
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
                isUpgrading = cardSetting.IsUpgrading;
                //Debug.Log("is Upgrade ?? => " + isUpgrading);

                if (isUpgrading) {//업그레이드 중임
                    state = State.SelectAndUpgrading;
                    //업그레이드 중이라면 남은시간 및 게이지 표시 코루틴 동작 실행
                    UpdateUpgradeRemainTime();
                } else {//업그레이드 중이 아님
                    state = State.Select;
                }
            }

            //사용 할수 있는지 없는 확인
            if(!cardSetting.UseAble) { state = State.Lock;}

            //실제 뷰에서 보여줌
            View.UpdateInfoListener(cardSetting, state);
        }


        /// <summary>
        /// 업그레이드 남은 정보 업데이트
        /// *빠른 간격으로 표시해야함
        /// </summary>
        void UpdateUpgradeRemainTime()
        {
            //코루틴 동작 중이라면 즉시 제거 -> 코루틴 중복을 막기위해
            if (updateUpgradeRemainTimeCoroutine != null) { StopCoroutine(updateUpgradeRemainTimeCoroutine); }
            //코루틴 새로생성
            updateUpgradeRemainTimeCoroutine = StartCoroutine(UpdateUpgradeRemainTimeCoroutine());
        }

        /// <summary>
        /// 0.33f간격
        /// 업그레이드 남은 정보 업데이트 코루틴
        /// </summary>
        /// <returns></returns>
        IEnumerator UpdateUpgradeRemainTimeCoroutine()
        {
            while (true)
            {
                //남은 연구 시간만 0.33f단위로 업데이트
                View.UpdateTime(cardSetting.UpgradeTimeset.GetRemainTimeToString(),cardSetting.UpgradeTimeset.GetRemainValue());
                yield return WFS;
            }
        }

        /// <summary>
        /// 업그레이드 취소 호출
        /// * 업글 취소 링커에 전달
        /// </summary>
        public void CancelToUpgrade()
        {
            //업글 취소를 위해 캔슬 링커 찾음
            //*Cancel 버튼에 위치
            UICardSlotInfoLinkerAboutCancelBtn cancelToUpgradePopup = GameObject.FindObjectOfType<UICardSlotInfoLinkerAboutCancelBtn>();
            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append("Cancels the [");
            //stringBuilder.Append(cardSetting.Name);
            //stringBuilder.Append("] Lv ");
            //stringBuilder.Append(cardSetting.Level + 2);//업그레이드 할 레벨이기 때문에 +2.
            //stringBuilder.Append(" upgrade.");

            stringBuilder.Append("Cancel the upgrade.");

            //링커에 해당 정보로 업그레이드 취소할수있게 전달
            cancelToUpgradePopup.Send(UINotifyYesOrNoPopupPresenter.Type.CancelUpgrade, stringBuilder.ToString(), cardSetting);
        }
    }
}
