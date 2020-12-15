using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI.MVP
{
    /// <summary>
    /// 카드 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class InGameCardSelectItemView : MonoBehaviour
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

        public void UpdateView()
        {

        }
    }
}
