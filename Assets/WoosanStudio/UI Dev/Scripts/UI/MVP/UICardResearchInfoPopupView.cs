using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드연구 팝업의 컨텐츠
    /// 코인으로 연구,젬으로 연구,겜블
    /// </summary>
    public class UICardResearchInfoPopupView : MonoBehaviour
    {
        [Header("[카드 이미지]")]
        public Image Image;
        [Header("[카드 이름]")]
        public Text Name;
        [Header("[카드 레벨]")]
        public Text Level;
        [Header("[카드 설명]")]
        public Text Description;

        //=================== 공통 ===================
        [Header("[카드 업그레이드 완료 레벨]")]
        public Text[] UpgradeComplateLevels;
        [Header("[카드 도박 후 완료 레벨]")]
        public Text GambleSuccessLevel;

        //=================== 코인으로 연구 ===================
        [Header("[카드 요구 코인]")]
        public Text UpgradeRequierCoin;
        [Header("[카드 요구 시간]")]
        public Text UpgradeRemainTime;

        //=================== 젬으로 연구 ===================
        [Header("[카드 요구 코인]")]
        public Text UpgradeRequierGem;

        //=================== 겜블 =================== 
        [Header("[카드 요구 보석]")]
        public Text GambleRequierGem;
        [Header("[카드 성공 확률]")]
        public Text GambleSuccessRate;

        //=================== 버튼 모음 =================== 

        [Header("[코인 사용 업그레이드 버튼]")]
        public GameObject BtnUpgradeByCoin; 
        [Header("[업그레이드 취소 버튼]")]
        public GameObject BtnCancel;
        [Header("[젬 사용 업그레이드 버튼]")]
        public GameObject BtnUpgradeByGem;
        [Header("[겜블 사용 업그레이드 버튼]")]
        public GameObject BtnUpgradeByGamble;

        //사용자 정의 이벤트
        [System.Serializable]
        public class UpgradeEvent : UnityEvent<int> { }
        [Header("[코인 사용 업그레이드 이벤트]")]
        public UpgradeEvent UpgradeByCoinEvent = new UpgradeEvent();
        [Header("[젬 사용 업그레이드 이벤트]")]
        public UpgradeEvent UpgradeByGemEvent = new UpgradeEvent();
        [Header("[겜블 이벤트]")]
        public UpgradeEvent UpgradeByGambleEvent = new UpgradeEvent();
        [Header("[업그레이드중 취소 이벤트]")]
        public UnityEvent CancelUpgradeEvent = new UnityEvent();

        
        /// <summary>
        /// 기본 모든 정보 업데이트
        /// *변경시에만 업데이트 하면 됨
        /// </summary>
        public void UpdateCardInfo(Sprite sprite, string name, string level, string description,
            string upgradeComplateLevel, string gambleSuccessLevel, int upgradeRequierCoin ,
            string upgradeRemainTime, int upgradeRequierGem,int gambleRequierGem,int gambleSuccessRate) {
            Image.sprite = sprite;
            Name.text = name;
            Level.text = level.ToString();
            Description.text = description;
        }

        /// <summary>
        /// 업그레이드 상태에 따라 UI 변경
        /// *한번만 호출해도 됨
        /// </summary>
        /// <param name="isUpgrading"></param>
        public void UpdateUpgradeInfoByState(bool isUpgrading = false)
        {
            //잠시 사용할 캔버스 그룹
            CanvasGroup canvasGroup = null;

            //업글 중이 아닌상태로 UI 변경
            if(!isUpgrading)
            {
                //코인 업그레이드 버튼 비활성화
                BtnUpgradeByCoin.SetActive(false);
                //취소버튼 활성화
                BtnCancel.SetActive(true);

                //젬사용 업그레이드 버튼 반투명 및 비활성화
                canvasGroup = BtnUpgradeByGem.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 0.4f;
                canvasGroup.interactable = false;

                //겜블 사용 업그레이드 버튼 반투명 및 비활성화
                canvasGroup = BtnUpgradeByGamble.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 0.4f;
                canvasGroup.interactable = false;
            } else {//업글중인 상태로 UI 변경
                //코인 업그레이드 버튼 활성
                BtnUpgradeByCoin.SetActive(true);
                //취소버튼 비활성화
                BtnCancel.SetActive(false);

                //젬사용 업그레이드 버튼 반투명 및 비활성화
                canvasGroup = BtnUpgradeByGem.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;

                //겜블 사용 업그레이드 버튼 반투명 및 비활성화
                canvasGroup = BtnUpgradeByGamble.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
            }
        }


        /// <summary>
        /// 업그레이드 남은 정보 업데이트
        /// Presenter에 의해 0.33f초로 호출됨
        /// </summary>
        public void UpdateUpgradeRemainTime(string value)
        {
            UpgradeRemainTime.text = value;
        }

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                UpdateUpgradeInfoByState(true);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                UpdateUpgradeInfoByState(false);
            }
        }
        #endregion

    }
}
