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

        //IReloadAction _reloadActions;


        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            FirstTrigger();
        }

        /// <summary>
        /// 해당 메서드 호출시 사격이 사작됨
        /// </summary>
        public void FirstTrigger() 
        {
            StartHandler.Invoke();
        }

        void Reloading(float _reloadTime)
        {
            if (refireChecker != null) StopCoroutine(refireChecker);
            refireChecker = StartCoroutine(RefireChecker(_reloadTime));
        }

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
        /// 리로드 인터페이스를 가져와서 내부 변수로 저장
        /// </summary>
        /// <param name="reloadEvent"></param>
        public void SetReloadEvent(IReloadEvent reloadEvent)
        {
            //_reloadActions = reloadActions;
            //_reloadActions.ReloadAction += Reloading;

            //리로드 액션 호출할때 리로딩 시퀀스 실행
            reloadEvent.ReloadEvent.AddListener(Reloading);
        }
    }
}
