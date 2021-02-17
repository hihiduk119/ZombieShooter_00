using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 카드 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class UIWaveCountPresent : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIWaveCountView View;

        [Header("[카운트 하려는 값]")]
        public int CurrentCount;

        [Header("[최대 카운트 ]")]
        public int MaxCount;

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(int currentCount, int maxCount,int round)
        {
            CurrentCount = currentCount;
            MaxCount = maxCount;

            //카운트 하려는 값 업데이트
            View.UpdateView(CurrentCount.ToString() + "/"+ MaxCount.ToString());
            //웨이브 카운트 업데이트
            View.WaveCount(round);
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(int currentCount)
        {
            CurrentCount = currentCount;
            //카운트 하려는 값 업데이트
            View.UpdateView(CurrentCount.ToString() + "/" + MaxCount.ToString());
            //바운스 연출
            View.BounceCount();
        }

        /// <summary>
        /// 활성 비활성
        /// </summary>
        public void SetActivate(bool value)
        {
            View.SetActivate(value);
        }
    }
}
