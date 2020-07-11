using System.Collections;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 건세팅 값을 기반으로하는 자동사격 인풋 시스템
    /// [행위]
    /// 1. 리로드 완료 시간 안려줌.
    /// </summary>
    public class AutoShooterInputBasedOnGunSetting : MonoBehaviour,IStart ,IEnd, IReloadAction
    {
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IInputEvents Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        private UnityEvent _startEvent = new UnityEvent();
        private UnityEvent _endEvent = new UnityEvent();
        public UnityEvent StartEvent { get { return _startEvent; } set { _startEvent = value; } }
        public UnityEvent EndEvent { get { return _endEvent; } set { _endEvent = value; } }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IReloadAction Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        public UnityAction<float> ReloadAction { get; set; }

        Coroutine refireChecker;

        void Awake()
        {
            //재장전 콜백 등록
            ReloadAction += Reloading;
        }

        /// <summary>
        /// 임시 코드로 1초후 자동으로 시작 이벤트 발생
        /// </summary>
        /// <returns></returns>
        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            StartEvent.Invoke();
        }


        /// <summary>
        /// 재장전
        /// </summary>
        /// <param name="_reloadTime"></param>
        void Reloading(float _reloadTime)
        {
            if (refireChecker != null) StopCoroutine(refireChecker);
            refireChecker = StartCoroutine(RefireChecker(_reloadTime));
        }

        /// <summary>
        /// 자동사격.
        /// </summary>
        /// <param name="reloadDelay"></param>
        /// <returns></returns>
        IEnumerator RefireChecker(float reloadDelay)
        {
            WaitForEndOfFrame WFF = new WaitForEndOfFrame();

            float deltaTime = 0;

            while (deltaTime < reloadDelay)
            {
                deltaTime += Time.deltaTime;

                yield return WFF;
            }

            //인풋 시작 이벤트 빌생
            StartEvent.Invoke();
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
