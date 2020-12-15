using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 스테이지 결과 팝업
    /// *MVP 모델
    /// </summary>
    public class InGameStageResultPopupView : MonoBehaviour
    {
        [Header("[실패 오브젝트]")]
        public GameObject Fail;

        [Header("[성공 오브젝트]")]
        public GameObject Success;

        [Header("[활성 별 컬러]")]
        public Color ActiveColor;

        [Header("[비활성 별 컬러]")]
        public Color DeactiveColor;

        [Header("[성공시 별 카운트]")]
        public List<Image> Stars = new List<Image>();

        [Header("[스코어]")]
        public Text Score;

        [Header("[죽인 몬스터]")]
        public Text Killed;

        [Header("[획득 코인]")]
        public Text Coin;

        [Header("[획득 젬]")]
        public Text Gem;

        /// <summary>
        /// 화면 업데이트
        /// </summary>
        /// <param name="isSuccessed">결과 성공 실패 여부</param>
        /// <param name="starCount">별 갯수</param>
        /// <param name="score">스코어</param>
        /// <param name="killed">죽인 몬스터</param>
        /// <param name="coin">획득 코인</param>
        /// <param name="gem">획득 보석</param>
        public void UpdateView(bool isSuccessed,int starCount,string score,string killed,string coin,string gem)
        {
            if (isSuccessed)        //성공 했는가?
            {
                //실패 비활성, 성공 활성
                Fail.SetActive(false);
                Success.SetActive(true);

                //모든별 비활성 컬러 세팅
                Stars.ForEach(value => value.color = DeactiveColor);

                //별 카운트 만큼 활성 컬러 세팅
                for (int i = 0; i < starCount; i++) { Stars[i].color = DeactiveColor; }
            }
            else                    //실패 했는가?
            {
                //성공 비활성, 실패 활성
                Fail.SetActive(true);
                Success.SetActive(false);
            }

            //스코어 설정
            Score.text = score;
            //죽인몬스터 설정
            Killed.text = killed;
            //획득 코인 설정
            Coin.text = coin;
            //획득 젬 설정
            Gem.text = gem;
        }
    }
}