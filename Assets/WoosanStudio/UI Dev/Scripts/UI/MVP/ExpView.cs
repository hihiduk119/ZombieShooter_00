using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 경험치 뷰
    /// *MPV 모델
    /// </summary>
    public class ExpView : MonoBehaviour
    {
        [Header("[경험치 슬라이더]")]
        public Slider ExpSlider;
        [Header("[레벨 텍스트]")]
        public Text Level;

        /// <summary>
        /// 경험치 바를 업데이트 한다.
        /// ExpPresenter.cs와 이벤트로 연결됨. 
        /// </summary>
        /// <param name="value"></param>
        public void ExpSliderListener(float value)
        {
            ExpSlider.value = value;
        }

        /// <summary>
        /// 레벨을 업데이트 한다.
        /// ExpPresenter.cs와 이벤트로 연결됨. 
        /// </summary>
        /// <param name="value"></param>
        public void LevelListener(int value)
        {
            Level.text = value.ToString();
        }
    }
}
