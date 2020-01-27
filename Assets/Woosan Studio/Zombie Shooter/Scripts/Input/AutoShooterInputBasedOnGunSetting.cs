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
    public class AutoShooterInputBasedOnGunSetting : MonoBehaviour, IInputActions , IReloadEventSocket 
    {
        public UnityAction StartHandler { get; set; }
        public UnityAction EndHandler { get; set; }

        Coroutine refireChecker;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            StartHandler.Invoke();
            
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

            StartHandler.Invoke();
        }



        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IReloadActionSocket Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<



        /// <summary>
        /// 재장전 이벤트 등록
        /// </summary>
        /// <param name="reloadEvent"></param>
        public void SetReloadEvent(IReloadEvent reloadEvent)
        {
            //리로드 액션 호출할때 리로딩 시퀀스 실행
            reloadEvent.ReloadEvent.AddListener(Reloading);
        }
    }
}
