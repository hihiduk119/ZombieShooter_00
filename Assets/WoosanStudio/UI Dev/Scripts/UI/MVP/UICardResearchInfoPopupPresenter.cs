using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드연구 팝업의 컨텐츠를 컨트롤
    /// *MVP 모델
    /// </summary>
    public class UICardResearchInfoPopupPresenter : MonoBehaviour
    {
        [Header("[[Auto->Awake()] MVP Veiw]")]
        public UICardResearchInfoPopupView View;

        //캐쉬용
        private WaitForSeconds WFS = new WaitForSeconds(0.33f);
        private Coroutine updateUpgradeRemainTimeCoroutine;

        //카드 세팅 데이터
        //*private으로 변경하니 널에러 발생
        public CardSetting cardSetting;
        public CardSetting CardSetting { get => cardSetting; set => cardSetting = value; }

        private void Awake()
        {
            //시작시 자동으로 뷰 찾아오기
            View = GameObject.FindObjectOfType<UICardResearchInfoPopupView>();
        }


        /// <summary>
        /// 팝업 활성화시 바로 실행
        /// </summary>
        private void OnEnable()
        {
            Debug.Log("업데이트 하세요!!");

            //카드 정보를 업데이트 한다.
            UpdateCardInfo();
            //Invoke("UpdateCardInfo", 0.1f);
        }

        private void OnDisable()
        {
            //코루틴 동작 중이라면 즉시 제거 -> 코루틴 중복을 막기위해
            if (updateUpgradeRemainTimeCoroutine != null) { StopCoroutine(updateUpgradeRemainTimeCoroutine); }
        }

        /// <summary>
        /// 카드 정보를 업데이트 한다
        /// </summary>
        public void UpdateCardInfo()
        {
            //업그레이드 완료 레벨
            string upgradeComplateLevel = CardSetting.PredictUpgradeComplateLevelToString(cardSetting);
            //도박 성공 목표 레벨
            string gambleSuccessLevel = CardSetting.PredictGambleSuccessLevelToString(cardSetting);
            //요구 코인 알아오기
            string strCoin = CardSetting.RequireCoinToString(cardSetting);
            //요구 젬 알아오기
            string strGem = CardSetting.RequireGemToString(cardSetting);
            //코인 사용 남은시간 
            string upgradeRemainTime = CardSetting.UpgradeRemainTimeToString(cardSetting);


            //뷰에 계산된 정보를 넣어줌
            View.UpdateCardInfo(cardSetting.Sprite,cardSetting.IconColor, cardSetting.Name, (cardSetting.Level + 1).ToString(),//레벨은 표시상 +1더해야함
                cardSetting.AllDescriptionForUpgradeInfo(), upgradeComplateLevel, gambleSuccessLevel, strCoin, upgradeRemainTime
                , strGem, GlobalDataController.GambleGem.ToString(), GlobalDataController.Instance.GambleCurrentSuccessRate) ;

            //뷰의 버튼 상태 변경
            View.UpdateUpgradeInfoByState(cardSetting.IsUpgrading);

            //업그레이드 중일때
            if (cardSetting.IsUpgrading)
            {
                Debug.Log("업그레이드 중이기 때문에 코루틴 작동");
                //업그레이드중 일때 시간업데이
                UpdateUpgradeRemainTime();
            }else
            {
                Debug.Log("업그레이드 중이기 아님 코루틴 작동 안함");
            }
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
                View.UpdateTime(cardSetting.UpgradeTimeset.GetRemainTimeToString());
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
            UINotifyPopupCancelUpgradeLinker cancelToUpgradePopup = GameObject.FindObjectOfType<UINotifyPopupCancelUpgradeLinker>();
            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append("Cancels the [");
            //stringBuilder.Append(cardSetting.Name);
            //stringBuilder.Append("] Lv ");
            //stringBuilder.Append(cardSetting.Level + 2);//업그레이드 할 레벨이기 때문에 +2.
            //stringBuilder.Append(" upgrade.");

            stringBuilder.Append("Cancel the upgrade.");

            //링커에 해당 정보로 업그레이드 취소할수있게 전달
            cancelToUpgradePopup.Send(UINotifyYesOrNoPopupPresenter.Type.CancelUpgrade, stringBuilder.ToString(),cardSetting);
        }
    }
}
