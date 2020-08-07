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
    public class AutoShooterInputBasedOnGunSetting : MonoBehaviour,IStart ,IEnd , IReload 
    {
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IInputEvents Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        private UnityEvent _startEvent = new UnityEvent();
        private UnityEvent _endEvent = new UnityEvent();
        public UnityEvent StartEvent { get { return _startEvent; } set { _startEvent = value; } }
        public UnityEvent EndEvent { get { return _endEvent; } set { _endEvent = value; } }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IReload Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        public UnityEvent StartReloadEvent { get => startReloadEvent; }
        public UnityEvent EndReloadEvent { get => endReloadEvent; }
        private UnityEvent startReloadEvent = new UnityEvent();
        private UnityEvent endReloadEvent = new UnityEvent();


        //재장전 대기 시간
        private float reloadTime = 2.0f;

        Coroutine refireChecker;

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
        void Reloading()
        {
            if (refireChecker != null) StopCoroutine(refireChecker);
            refireChecker = StartCoroutine(RefireChecker(reloadTime));
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
