using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 카드 모델 데이터를 필요에 따라 동기화 시키는 역활을 함.
    /// *MVP 모델
    /// </summary>
    public class UICardPresenter : MonoBehaviour
    {
        [Header("[MVP Model]")]
        public UICardModel Model;


        IEnumerator Start()
        {
            //0.1f초 대기 이유??
            //일단 에러 발생은 확인
            yield return new WaitForSeconds(0.1f);
            Initialize();
        }

        /// <summary>
        /// 최초 사용시 기존 데이터 모두 로드
        /// </summary>
        public void Initialize()
        {
            //싱크로 호출하여 데이터 동기화
            Model.Synchronization();
        }

        /// <summary>
        /// 카드 업그레이드 시작시 카드 데이터 반영 및 싱크 마추기
        /// </summary>
        public void CardUpgradeStart()
        {

        }

        /// <summary>
        /// 카드 업그레이드 완료시 실제 카드 데이터 반영 및 싱크마추기
        /// </summary>
        public void CardUpgradeComplate()
        {

        }
    }
}
