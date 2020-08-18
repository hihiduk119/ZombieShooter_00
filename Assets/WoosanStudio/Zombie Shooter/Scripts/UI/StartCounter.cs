using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 스테이지 시작시 3,2,1 UI 카운팅 
    /// </summary>
    public class StartCounter : MonoBehaviour , IStart , IEnd, ISound
    {
        //카운팅 시작과 끝을 알리는 이벤트
        
        public UnityEvent StartEvent => startEvent;
        public UnityEvent EndEvent => endEvent;
        public UnityEvent PlayEvent => playEvent;

        [Header("[카운팅 UI]")]
        public List<Sprite> images = new List<Sprite>();

        //페이드 시킬 캔버스 그룹
        private CanvasGroup canvasGroup;
        private Image image;

        [SerializeField]
        private UnityEvent startEvent = new UnityEvent();
        [SerializeField]
        private UnityEvent endEvent = new UnityEvent();
        [SerializeField]
        private UnityEvent playEvent = new UnityEvent();

        private int count = 0;
        private int maxCount = 4;
        
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            //자식의 이미지 가져오기
            image = GetComponentInChildren<Image>();
        }

        /// <summary>
        /// 카운팅 할때마다 스프라이트 변ㄱ
        /// </summary>
        /// <param name="sprite"></param>
        public void Count()
        {
            //시작 이벤트 발생
            StartEvent.Invoke();
            Debug.Log("카운트 시작 이벤트");

            //재귀 호출
            Swap(null);
        }

        /// <summary>
        /// 재귀호 
        /// </summary>
        /// <param name="sprite"></param>
        private void Swap(Sprite sprite)
        {
            //사운드 플레이
            playEvent.Invoke();

            //캔버스 알파 1로 초기화
            canvasGroup.alpha = 1f;

            //첫 이미지 숫자 1 이미지로 초기화
            image.sprite = images[count];

            //숫자 흔들기 위해 회전각 만듬
            image.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 20f));
            //숫자 흔듬
            image.transform.DORotate(new Vector3(0, 0, -20f), 0.2f).SetLoops(4, LoopType.Yoyo);

            canvasGroup.DOFade(0, 1f).OnComplete(() => {
                //최대 카운트 만큼 재귀 호출
                if (maxCount-1 > count)
                {
                    //카운트 증가
                    count++;
                    //재귀 호출
                    Swap(images[count]);

                    //마지막 카운트시에 스캐일 연출 
                    if((maxCount - 1) == count)
                    {
                        //숫자 크게 키우기
                        image.transform.DOScale(30, 1f).SetEase(Ease.InCubic).OnComplete(() =>
                            //연출 끝난 후에는 초기화
                            image.transform.localScale = Vector3.one
                        );
                    }
                }
                else
                {
                    //종료 이벤트 발생
                    EndEvent.Invoke();
                    Debug.Log("카운트 종료 이벤트");
                }
            });
        }

        #region [-TestCode]
        void Update()
        {
            //카운트 다운 시작
            if (Input.GetKeyDown(KeyCode.C))
            {
                Count();
            }
        }
        #endregion

    }
}
