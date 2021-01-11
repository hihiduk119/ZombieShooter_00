using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 분노 게이지 뷰어
    /// *MVP 모델
    /// </summary>
    public class AngerGaugeView : MonoBehaviour
    {
        [Header("[게이지 캔버스 이펙트 역할]")]
        public List<CanvasGroup> Effects;

        [Header("[게이지 완충시 스케일 트윈 시킬 아이콘들]")]
        public List<GameObject> Icons = new List<GameObject>();

        [Header("[게이지 루트]")]
        public Transform ProgressBarRoot;

        [Header("[게이지 프로그래스]")]
        public Image ProgressBar;

        [Header("[버튼]")]
        public Button BtnClick;

        [Header("[클릭 이벤트 -> Preseter와 연결용]")]
        public UnityEvent ClickEvent = new UnityEvent();


        /// <summary>
        //값 추가시 마다 프로그래스 바 전체 트윈
        /// </summary>
        void DoTweenForProgressBar()
        {
            //트윈 초기화
            ProgressBarRoot.DOKill();
            ProgressBarRoot.localScale = Vector3.one;

            ProgressBarRoot.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }

        /// <summary>
        /// 프로그래스바의 값을 세팅한다 
        /// </summary>
        /// <param name="value"></param>
        public void UpdateProgress(float value)
        {
            ProgressBar.fillAmount = value;
            //값 추가시 마다 프로그래스 바 전체 트윈
            DoTweenForProgressBar();
        }

        /// <summary>
        /// 트윈 이펙트 활성화 & 비활성화
        /// </summary>
        /// <param name="value"></param>
        public void ActivateTween(bool active)
        {
            //버튼 활성화 여부
            BtnClick.interactable = active;
            CanvasGroup canvasGroup = BtnClick.GetComponent<CanvasGroup>();
            //버튼 보이게 하기
            if (active)
            {
                canvasGroup.alpha = 1;
            } else
            {
                canvasGroup.alpha = 0.02f;
            }

            //아이콘들 초기화
            Icons.ForEach(value => value.transform.DOKill());
            Icons.ForEach(value => value.transform.localScale = Vector3.one);
            //아이콘 트윈을 활성화 합니다
            if (active)
            {
                //아이콘 트윈 시작
                Icons.ForEach(value => value.transform.DOScale(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine));
            }


            //이펙트 초기화
            Effects.ForEach(value => value.DOKill());
            Effects.ForEach(value => value.alpha = 0);
            //캔버스 보이게 하기
            if (active)
            {
                //캔버스 깜빡임 효과
                Effects.ForEach(value => value.DOFade(1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine));
            }
        }

        /// <summary>
        /// 클릭
        /// </summary>
        public void Clicked()
        {
            ClickEvent.Invoke();
        }

        /*
        #region [-TestCode]
        void Update()
        {
            //프로그래스 업데이트
            if (Input.GetKeyDown(KeyCode.Z))
            {
                UpdateProgress(TestValue);
                TestValue += 0.1f;
            }

            //트윈 활성화
            if (Input.GetKeyDown(KeyCode.X))
            {
                ActivateTween(true);
            }

            //트윈 활성화
            if (Input.GetKeyDown(KeyCode.C))
            {
                ActivateTween(false);
            }
        }
        #endregion
        */
    }
}
