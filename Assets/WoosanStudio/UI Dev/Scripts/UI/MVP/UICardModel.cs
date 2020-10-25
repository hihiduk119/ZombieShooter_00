using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 카드 데이터
    /// *MPV 모델
    /// </summary>
    public class UICardModel : MonoBehaviour
    {
        [System.Serializable]
        public class CardData
        {
            //UI에서 데이터의 순서
            public int SortIndex;
            //마지막으로 장착 중이었던 장비나 캐릭터
            public bool UseAble;

            //연구 중이었다면 UI데이터의 순서
            public int ResearchSlotIndex = -1;

            //업글중 이었는지 아닌지
            public bool IsUpgrading = false;

            //남은 업글 시간
            public double RemainCompleteTime = 0;
        }

        public void Load()
        {
            
        }

        public void Save()
        {
            
        }
    }
}
