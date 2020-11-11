using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 알림을 실제 호출하는 컨트롤러
    /// </summary>
    public class NotifyPopupController : MonoBehaviour
    {
        //싱글톤 패턴
        static public NotifyPopupController Instance;
        [Header("[UI 팝업 오프너 -> 카드 업그레이드 결과 출력]")]
        public Ricimi.PopupOpener popupOpener;

        private void Awake()
        {
            //싱글톤 패턴
            Instance = this;
            //다른씬에서도 사용
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// 결과 출력 실행
        /// * 알림 타입에 따 다른 메시지 호출
        /// </summary>
        /// <param name="result"></param>
        public void OpenResult(UINotifyPopupModel.Type type)
        {
            UINotifyPopupPresenter presenter = popupOpener.popupPrefab.GetComponent<UINotifyPopupPresenter>();
            presenter.Type = type;

            popupOpener.OpenPopup();
        }
    }
}
