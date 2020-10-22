using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// *MPV 모델
    /// </summary>
    public class UIPlayerPurchaseModel : MonoBehaviour
    {
        public int RequireGem = 100;

        public int RequireLevel = 10;

        //리스트 순서는 캐릭터 순서와 같다 -> CardSetting.CardTypeByCharacter
        [Header("[캐릭터 카드세팅 리스트 = 0번은 사용 안함]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();

        
    }
}
