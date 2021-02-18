using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 라운드 선택 프리젠터
    /// </summary>
    public class UIRoundSelectPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIRoundSelectView View;

        //올리고 내릴때 최대 라운드는 플레이어가 플레이한 라운드
        //사용되는 에너지 값 변경
        //모든 맵마다 최대 언락된 라운드 저장 해야함.
        private void Awake()
        {
            this.View.RoundUpEvent.AddListener(RoundUp);
            this.View.RoundDownEvent.AddListener(RoundDown);
            this.View.RoundStrongUpEvent.AddListener(RoundStrongUp);
            this.View.RoundStrongDownEvent.AddListener(RoundStrongDown);
        }

        /// <summary>
        /// 1씩 라운드 올리기
        /// </summary>
        private void RoundUp()
        {

        }

        /// <summary>
        /// 1씩 라운드 내리기
        /// </summary>
        private void RoundDown()
        {

        }

        /// <summary>
        /// 10씩 라운드 올리기
        /// </summary>
        private void RoundStrongUp()
        {

        }

        /// <summary>
        /// 10씩 라운드 내리기
        /// </summary>
        private void RoundStrongDown()
        {

        }
    }
}
