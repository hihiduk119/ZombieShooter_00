using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 네임드 바 프리젠터
    /// *몬스터 체력 표시.
    /// *MVP 모델
    /// </summary>
    [RequireComponent (typeof(CanvasGroup))]
    public class UINamedMonsterBarPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UINamedMonsterBarView View;

        [Header("[저항 루트]")]
        public Transform Resistance;

        [Header("[저항 아이템 프리펩]")]
        public GameObject PrefabItem;

        [Header("[[Auto]만드려는 네임드 몬스터]")]
        public MonsterSettings monsterSetting;

        [Header("[[Auto]저항 아이템 리스트]")]
        public List<UIResistanceItemPresenter> ResistanceItemList = new List<UIResistanceItemPresenter>();

        //싱글톤 패턴
        static public UINamedMonsterBarPresenter Instance;
        [Header("[활성 비활성 토글]")]
        [SerializeField]
        private bool isActivate = false;
        public bool IsActivate => isActivate;

        [Header("[체력 UI]")]
        public Image Health;
        [Header("[뒤에서 체력 따라 다니는 백그라운드 UI]")]
        public Image Background;
        [Header("[이름]")]
        public Text Name;

        //캐쉬용
        private CanvasGroup canvasGroup;
        private GameObject clone;
        private UIResistanceItemPresenter resistanceItemPresenter;
        private WaitForSeconds WFS = new WaitForSeconds(0.5f);

        float amount = 1f;

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
        /// 초기화
        /// </summary>
        public void Reset()
        {
            Health.fillAmount = 1f;
            Background.fillAmount = 1f;
        }

        /// <summary>
        /// 카드 프로퍼티 정보를 바탕으로 아이템 생성
        /// </summary>
        /// <param name="cardProperty"></param>
        public void MakeItems()
        {
            //몬스터 프로퍼티 갯수 만큼 아이템 생성
            for (int i = 0; i < monsterSetting.Propertys.Count; i++)
            {
                //아이템 생성
                clone = Instantiate(PrefabItem) as GameObject;
                //아이템 부모 설정
                clone.transform.SetParent(Resistance);
                resistanceItemPresenter = clone.GetComponent<UIResistanceItemPresenter>();
                //나중에 삭제 위해 미리 빽업.
                this.ResistanceItemList.Add(resistanceItemPresenter);

                //아이템 UI 에 필수 정보 세팅
                resistanceItemPresenter.cardProperty = monsterSetting.Propertys[i];
                resistanceItemPresenter.UpdateInfo(monsterSetting.Level);

                //생성시에는 이미 비활성화 되어 있음
                //resistanceItemPresenter.SetActivate(false);
            }
        }

        /// <summary>
        /// 아이템 보여주기 코루틴 실행
        /// *순차 적으로 아이템 보여준 후 자동 삭제
        /// </summary>
        public void RunShowingItem()
        {
            StartCoroutine(RunShowingItemCoroutine());
        }

        /// <summary>
        /// 아이템 보여주기 실행부
        /// </summary>
        /// <returns></returns>
        IEnumerator RunShowingItemCoroutine()
        {
            //0.5초 대기
            yield return WFS;
            //저항 텍스트 표시 활성
            View.SetActivateByResistanceText(true);
            int count = 0;

            while (count <  this.ResistanceItemList.Count)
            {
                yield return WFS;
                //순차적으로 아이템 활성화
                this.ResistanceItemList[count].SetActivate(true);

                count++;
            }

            //7.5초 대기
            yield return new WaitForSeconds(7.5f);
            
            //저항 텍스트 표시 비활성
            View.SetActivateByResistanceText(false);
            //모든 아이템 비활성
            this.ResistanceItemList.ForEach(value => value.SetActivate(false));

            //0.5초 대기
            yield return WFS;
            //대기후 모든 아이템 삭제
            this.ResistanceItemList.ForEach(value => Destroy(value.gameObject));
            //리스트 비우기
            this.ResistanceItemList.Clear();
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
            if (value)
            {
                isActivate = true;
            }
            else
            {
                isActivate = false;
            }

            View.SetActivate(value);
        }

        private void Update()
        {
            //테스트용
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    amount -= 0.1f;
            //    this.SetAmount(amount);
            //}

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
            //Debug.Log("health = " + Health.fillAmount + "  Background = " + Background.fillAmount);
            Background.fillAmount = Mathf.Lerp(Health.fillAmount, Background.fillAmount, 0.95f);
        }
    }
}
