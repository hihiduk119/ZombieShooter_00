using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 네임드 몬스터의 체력 패널
    /// </summary>
    [RequireComponent (typeof(CanvasGroup))]
    public class NamedMonsterHealthBarController : MonoBehaviour
    {
        //싱글톤 패턴
        static public NamedMonsterHealthBarController Instance;
        [Header ("[활성 비활성 토글]")]
        [SerializeField]
        private bool isActivate = false;
        public bool IsActivate => isActivate;

        [Header("[체력 UI]")]
        public Image Health;
        [Header("[뒤에서 체력 따라 다니는 백그라운드 UI]")]
        public Image Background;
        [Header("[이름]")]
        public Text Name;

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            Instance = this;

            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// 체력 양을 세팅
        /// </summary>
        /// <param name="amount">0-1사이 값</param>
        public void SetAmount(float amount)
        {
            Health.fillAmount = amount;
        }

        /// <summary>
        /// 이름 세팅
        /// </summary>
        /// <param name="name">몬스터 이름</param>
        public void SetName(string name)
        {
            Name.text = name;
        }

        /// <summary>
        /// 체력 바를 활성 or 비활성 시킴
        /// </summary>
        /// <param name="value"></param>
        public void SetActivate(bool value)
        {
            if(value)
            {
                isActivate = true;
                canvasGroup.DOFade(1, 0.3f);

            } else
            {
                isActivate = false;
                canvasGroup.DOFade(0, 0.3f);
            }
        }

        private void Update()
        {
            //비활성 상태에선 동작 하지 않음
            if (!isActivate) return;

            //체력바를 따라 다니는 게이지
            FollowGauge();
        }

        /// <summary>
        /// 체력 UI를 따라 백그라운드 UI가 움직임
        /// </summary>
        void FollowGauge()
        {
            Background.fillAmount = Mathf.Lerp(Health.fillAmount, Background.fillAmount, 0.9f);
        }
    }
}
