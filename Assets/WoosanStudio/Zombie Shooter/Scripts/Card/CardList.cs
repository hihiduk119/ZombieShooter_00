using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드들이 담긴 리스트
    /// </summary>
    public class CardList : MonoBehaviour
    {
        [Header("[로비 선택 카드 => 기본 세팅은 테스트용]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();
        //실제 사용은 이걸로 해야함.
        //*테스트할 때는 눈으로 봐야하기 때문에 CardSettings 사용
        public List<ICard> cards;
    }
}
