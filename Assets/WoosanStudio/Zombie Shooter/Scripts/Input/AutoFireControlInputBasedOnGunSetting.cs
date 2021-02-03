using System.Collections;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public enum FireState
    {
        Firing,
        Reloading,
        Stoped,
    }
    /// <summary>
    /// 건세팅 값을 기반으로하는 자동사격 인풋 시스템
    /// 플레이어가 조준상태를 알려주면 알아서 사ㄱ
    /// 발사체 컨트롤러에 실제 사격 및 재장전 등을 통제
    /// </summary>
    public class AutoFireControlInputBasedOnGunSetting : MonoBehaviour, IAim,IStart ,IEnd , IReload
    {
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IAim Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //조준 됐다 -> 사격을 해도 좋다 
        public UnityEvent AimEvent => aimEvent;
        //조준이 해제 됐다 -> 사격을 중지하라
        public UnityEvent ReleaseEvent => releaseEvent;

        private UnityEvent aimEvent = new UnityEvent();
        private UnityEvent releaseEvent = new UnityEvent();

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IStart ,IEnd Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //사격을 시작
        public UnityEvent StartEvent { get { return _startEvent; } set { _startEvent = value; } }
        //사격 중지
        public UnityEvent EndEvent { get { return _endEvent; } set { _endEvent = value; } }
        private UnityEvent _startEvent = new UnityEvent();
        private UnityEvent _endEvent = new UnityEvent();

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IReload Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //재장전 시작
        public UnityEvent StartReloadEvent { get => startReloadEvent; }
        //재장전 끝
        public UnityEvent EndReloadEvent { get => endReloadEvent; }

        private UnityEvent startReloadEvent = new UnityEvent();
        private UnityEvent endReloadEvent = new UnityEvent();

        public UnityAction ReloadAction;

        private FireState fireState = FireState.Stoped;

        //재장전 대기 시간
        [Header("[무기의 재장전 시간]")]
        public float ReloadTime = 2.0f;

        //조준선이 정렬됬다
        [HideInInspector]
        public bool IsSightAlimentComplete = false;

        private Coroutine sightAlimentCheckCoroutine;

        private Coroutine refireCheckCoroutine;

        private WaitForEndOfFrame WEF = new WaitForEndOfFrame();

        void Awake()
        {
            //조준 및 해제시 동작할 메서드 등록
            AimEvent.AddListener(FireControl);
            ReleaseEvent.AddListener(StopFire);
            //리로딩 액션에 리로드 세ㄹ
            ReloadAction += Reloading;
        }


        /// <summary>
        /// 임시 코드로 1초후 자동으로 시작 이벤트 발생
        /// </summary>
        /// <returns></returns>
        //IEnumerator Start()
        //{
        //    yield return new WaitForSeconds(1f);
        //}

        /// <summary>
        //사격 가능한지 여부 확인 후 동작수행
        /// </summary>
        public void FireControl()
        {
            switch(fireState)
            {
                case FireState.Reloading:
                    //Debug.Log("재장전 중!! 기다리세요");
                    break;
                case FireState.Firing:
                    //Debug.Log("이미 사격중!!");
                    break;
                case FireState.Stoped:
                    //Debug.Log("사격을 시작 합니다.");
                    //StartEvent.Invoke();

                    //조준선이 정렬 됐는지 확인 후 사격 시작
                    if (sightAlimentCheckCoroutine != null) StopCoroutine(sightAlimentCheckCoroutine);
                    sightAlimentCheckCoroutine = StartCoroutine(SightAlimentCheckCoroutine());
                    break;
            }
        }

        /// <summary>
        /// 조준선이 정렬 됐는지 확인 후 사격 시작
        /// </summary>
        /// <returns></returns>
        IEnumerator SightAlimentCheckCoroutine()
        {
            while (true)
            {
                //조준선 정렬 확인
                if (IsSightAlimentComplete)
                {
                    //사격 시작
                    StartEvent.Invoke();
                    //코루틴 탈출
                    yield break;
                }
                //코루틴 반복
                yield return WEF;
            }
        }

        /// <summary>
        /// 조준이 해제 됐다
        /// </summary>
        public void StopFire()
        {
            //사격을 중지 합니다.
            EndEvent.Invoke();
            //Debug.Log("사격을 중지 합니다.");
        }

        /// <summary>
        /// 재장전S
        /// </summary>
        public void Reloading()
        {
            //재장전 시작 이벤트 호출
            StartReloadEvent.Invoke();

            //Debug.Log("=>>>>>>>>>>>>>>>>. 재장전 시작");

            if (refireCheckCoroutine != null) StopCoroutine(refireCheckCoroutine);
            refireCheckCoroutine = StartCoroutine(RefireCheckCoroutine(ReloadTime));
        }

        /// <summary>
        /// 재장전 완료 이벤트 받는 부분
        /// *Animation 에 달린 이벤트로 Rifle_Reload.anim 확인
        /// </summary>
        public void ReloadEnd()
        {
            //Debug.Log("재장전 완료");
            //사격 재호출
            FireControl();
        }

        /// <summary>
        /// 자동사격.
        /// </summary>
        /// <param name="reloadDelay"></param>
        /// <returns></returns>
        IEnumerator RefireCheckCoroutine(float reloadDelay)
        {
            //재장전 상태 변경
            fireState = FireState.Reloading;

            WaitForEndOfFrame WFF = new WaitForEndOfFrame();

            float deltaTime = 0;

            while (deltaTime < reloadDelay)
            {
                deltaTime += Time.deltaTime;

                yield return WFF;
            }

            //재장전 끝 이벤트 호출
            EndReloadEvent.Invoke();
            Debug.Log("=>>>>>>>>>>>>>>>>. 재장전 끝");

            //사격 가능한지 여부 확인 후 동작수행
            FireControl();

            //사격 아닌 상태로 변경
            fireState = FireState.Stoped;
        }


        #region [-TestCode]
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        StartEvent.Invoke();
        //    }
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        EndEvent.Invoke();
        //    }
        //}
        #endregion

    }
}
