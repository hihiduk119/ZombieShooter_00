using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        private void Awake()
        {
            //시작시 자동으로 뷰 찾아오기
            View = GameObject.FindObjectOfType<UICardResearchInfoPopupView>();
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
