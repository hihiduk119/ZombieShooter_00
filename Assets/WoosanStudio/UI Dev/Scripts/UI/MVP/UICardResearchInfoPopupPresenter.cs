using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드연구 팝업의 컨텐츠를 컨트롤
    /// *MPV 모델
    /// </summary>
    public class UICardResearchInfoPopupPresenter : MonoBehaviour
    {
        [Header("[[Auto->Awake()] MVP Veiw]")]
        public UICardResearchInfoPopupView View;

        //캐쉬용
        private WaitForSeconds WFS = new WaitForSeconds(0.33f);
        private Coroutine updateUpgradeRemainTimeCoroutine;

        //카드 세팅 데이터
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

        /// <summary>
        /// 카드 정보를 업데이트 한다
        /// </summary>
        public void UpdateCardInfo()
        {
            //업그레이드 완료 레벨 -> 최고 레벨일때는 MAX 표시
            int iUpgradeComplateLevel = cardSetting.Level + 1;//기본 연구는 +1 업글
            string upgradeComplateLevel;
            if (iUpgradeComplateLevel >= cardSetting.MaxLevel) { upgradeComplateLevel = "MAX"; }
            else { upgradeComplateLevel = (iUpgradeComplateLevel + 1).ToString(); }//레벨은 표시상 +1

            //도박 성공 목표 레벨
            int iGambleSuccessLevel = cardSetting.Level + 2;//도박은 +2 업글
            string gambleSuccessLevel;
            if (iGambleSuccessLevel >= cardSetting.MaxLevel) { gambleSuccessLevel = "MAX"; }
            else { gambleSuccessLevel = (iGambleSuccessLevel+1).ToString(); }//레벨은 표시상 +1

            //요구 코인 알아오기
            int coin = NextValueCalculator.GetRequireCoinByLevel(cardSetting.MaxLevel, cardSetting.Level);
            string strCoin = string.Format("{0:0,0}", coin);
            //요구 젬 알아오기
            int gem = NextValueCalculator.GetRequireGemByLevel(cardSetting.MaxLevel, cardSetting.Level);
            string strGem = string.Format("{0:0,0}", gem);

            //뷰에 계산된 정보를 넣어줌
            View.UpdateCardInfo(cardSetting.Sprite,cardSetting.IconColor, cardSetting.Name, (cardSetting.Level + 1).ToString(),//레벨은 표시상 +1더해야함
                cardSetting.AllDescription(), upgradeComplateLevel, gambleSuccessLevel, strCoin, cardSetting.UpgradeTimeset.GetRemainTimeToString()
                , strGem, GlobalDataController.GambleGem.ToString(), NextValueCalculator.GetGambleSuccessRate(0)) ;
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
                yield return WFS;
            }
        }

        /// <summary>
        /// 코인을 사용한 업그레이드 이벤트 리시버
        /// </summary>
        /// <param name="value"></param>
        void UpgradeByCoinRecevier(int value)
        {

        }

        /// <summary>
        /// 젬을 사용한 업그레이드 이벤트 리시버
        /// </summary>
        /// <param name="value"></param>
        void UpgradeByGemRecevier(int value)
        {

        }

        /// <summary>
        /// 겜블을 사용한 업그레이드 이벤트 리시버
        /// </summary>
        /// <param name="value"></param>
        void UpgradeByGambleRecevier(int value)
        {

        }

        /// <summary>
        /// 업그레이드 취소 이벤트 리시버
        /// </summary>
        /// <param name="value"></param>
        void CancelUpgradeRecevier()
        {

        }
    }
}
