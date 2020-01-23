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
    public class AutoShooterInputBasedOnGunSetting : MonoBehaviour, IInputActions
    {
        public UnityAction StartHandler { get; set; }
        public UnityAction EndHandler { get; set; }

        GunSettings GunSettings { get; set; }

        Coroutine refireChecker;

        IProjectileLauncherActions _projectileLauncherActions;

        IGunActions _gunActions;

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

        public void SetProjectileLauncherActions(IProjectileLauncherActions projectileLauncherActions)
        {
            _projectileLauncherActions = projectileLauncherActions;
        }

        void Reload()
        {
            if (refireChecker != null) StopCoroutine(refireChecker);
            refireChecker = StartCoroutine(RefireChecker(GunSettings.ReloadTime));
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
    }
}
