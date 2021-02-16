using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 분노 게이지 프리젠터
    /// *MVP 모델
    /// </summary>
    public class AngerGaugePresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public AngerGaugeView View;

        float progressValue = 0;

        private void Awake()
        {
            //View와 이벤트 연결
            View.ClickEvent.AddListener(Clicked);
            //프로그래스 값 업데이트
            View.UpdateProgress(progressValue);
        }

        /// <summary>
        /// 분노 게이지 활성 또는 비활성
        /// </summary>
        public void AddProgressValue(float value) 
        {
            progressValue += value;

            //값이 1 이하
            if(progressValue < 1)
            {
                //프로그래스 값 업데이트
                View.UpdateProgress(progressValue);
            } else if(1 <= progressValue )
            {
                //프로그래스 값 업데이트
                View.UpdateProgress(progressValue);
                //트윈 활성화
                View.ActivateTween(true);
            }
            //사운드 필요
        }

        /// <summary>
        /// View에서 발생한 클릭 이벤트 
        /// </summary>
        public void Clicked()
        {
            //분노 버튼 눌림 처리
            View.ActivateTween(false);
            //프로그래스 초기화
            progressValue = 0;
            //프로그래스 값 업데이트
            View.UpdateProgress(progressValue);
            //폭격 실행
            AirStrikeShadowController airStrikeShadowController = GameObject.FindObjectOfType<AirStrikeShadowController>();
            airStrikeShadowController.Run();
            //사운드 필요
        }


        /*
        #region [-TestCode]
        void Update()
        {
            //프로그래스 업데이트
            if (Input.GetKeyDown(KeyCode.Z))
            {
                AddProgressValue(0.2f);
            }
        }
        #endregion
        */
        
    }
}
