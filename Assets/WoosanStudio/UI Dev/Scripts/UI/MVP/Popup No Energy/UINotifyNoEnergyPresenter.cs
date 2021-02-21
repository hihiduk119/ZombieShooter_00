using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 부족 표시 프리젠트
    /// *MVP 모델
    /// </summary>
    public class UINotifyNoEnergyPresenter : MonoBehaviour
    {
        [Header("[MVP Veiw]")]
        public UINotifyNoEnergyView View;

        private void Awake()
        {
            //예스 이벤트에 리스너 등록
            this.View.YesEvent.AddListener(ClickYesListener);
            //노 이벤트에 리스너 등록
            this.View.NoEvent.AddListener(ClickNoListener);
        }

        /// <summary>
        /// View 클릭 예스 이벤트에 연결
        /// </summary>
        public void ClickYesListener()
        {
            Debug.Log("에너지 부족 시작");
            //에너지 부족 시작 글로벌 데이터 변경
            GlobalDataController.NoEnergyStart = true;

            //씬강제 시작
            GameObject.FindObjectOfType<UIStartButtonPresenter>().GoToScene();
        }

        /// <summary>
        /// View 클릭 노 이벤트에 연결
        /// </summary>
        public void ClickNoListener()
        {
            Debug.Log("에너지 부족 시작 안함");
        }
    }
}
