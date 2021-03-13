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

        /// <summary>
        /// HUD 활성 & 비활성
        /// </summary>
        /// <param name="value"></param>
        public void SetActiveHUD(bool value)
        {
            HUDNaviElement.enabled = value;
        }
    }
}
