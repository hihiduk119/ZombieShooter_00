using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Field
{
    /// <summary>
    /// 필드 아이템
    /// </summary>
    public class Item : MonoBehaviour
    {
        public SickscoreGames.HUDNavigationSystem.HUDNavigationElement HUDNaviElement;

        [Header("[고유 값]")]
        public int Value;

        [Header("[아이템 타입]")]
        public ItemSetting.FieldItem Type;

        /// <summary>
        /// HUD 활성 & 비활성
        /// </summary>
        /// <param name="value"></param>
        private void SetActiveHUD(bool value)
        {
            HUDNaviElement.enabled = value;
        }

        /// <summary>
        /// HUD 비활성화
        /// </summary>
        public void DeactiveHUD()
        {
            this.SetActiveHUD(false);
        }

        /// <summary>
        /// 획득한 값 글로벌 데이터에 세팅
        /// </summary>
        public void SetGainedValue()
        {
            //해당 타입의 값 추가
            switch (this.Type)
            {
                case ItemSetting.FieldItem.Coin:
                    GlobalDataController.StageGainedCoin += this.Value;
                    break;
                case ItemSetting.FieldItem.Exp:
                    GlobalDataController.StageGainedXP += this.Value;
                    break;
            }
        }
    }
}
