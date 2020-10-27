using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드연구 팝업의 컨텐츠
    /// </summary>
    public class UICardResearchInfoPopupView : MonoBehaviour
    {
        [Header("[카드 이미지]")]
        public Image Image;
        [Header("[카드 이름]")]
        public Text Name;
        [Header("[카드 레벨]")]
        public Text Level;
        [Header("[카드 설명]")]
        public Text Description;


        //=================== 공통 ===================
        [Header("[카드 이전 레벨]")]
        public Text[] PreviousLevels;
        [Header("[카드 다음 레벨]")]
        public Text[] NextsLevels;
        //=================== 연구 ===================
        [Header("[카드 요구 코인]")]
        public Text RequierCoin;
        [Header("[카드 요구 시간]")]
        public Text RequierTime;
        //=================== 겜블 =================== 
        [Header("[카드 요구 보석]")]
        public Text RequierGem;
        [Header("[카드 성공 확률]")]
        public Text SuccessRate;

        /// <summary>
        /// 겜블 저장 데이터
        /// </summary>
        public class GembleData
        {
            //카드 요구 보석
            public int RequierGem;
            //카드 성공 확률
            public int SuccessRate;
        }


        /// <summary>
        /// 정보를 업데이트 한다.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <param name="description"></param>
        public void UpdateInfo(Sprite sprite, string name, int level, string description)
        {
            Image.sprite = sprite;
            Name.text = name;
            Level.text = level.ToString();
            Description.text = description;
        }
    }
}
