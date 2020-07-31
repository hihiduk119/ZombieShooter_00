using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 알림 팝업
    /// </summary>
    public class NotifyPopupController : MonoBehaviour
    {
        public static NotifyPopupController Instance;

        public MyPopupOpener mPopup;

        private Coroutine mAutoCloseCoroutine;

        private void Awake()
        {
            Instance = this;
            mPopup = GetComponent<MyPopupOpener>();
        }

        public void Setup()
        {

        }

        public void Open()
        {
            mPopup.OpenPopup();

            AutoClose();
        }


        /// <summary>
        /// 1초 후 자동 닫음 
        /// </summary>
        public void AutoClose()
        {
            if (mAutoCloseCoroutine != null) { StopCoroutine(mAutoCloseCoroutine); }
            mAutoCloseCoroutine = StartCoroutine(AutoCloseCoroutine());
        }


        IEnumerator AutoCloseCoroutine()
        {
            yield return new WaitForSeconds(1f);

            mPopup.Instance.GetComponent<MyPopup>().Close();
        }
    }
}
