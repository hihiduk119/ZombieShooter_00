using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Card.Set
{
    /// <summary>
    /// 스테이지 구성을 위한 세팅
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/Card/Set/Make", fileName = "[Card Set ]")]
    public class Setting : ScriptableObject
    {
        [Header("[캐릭터 카드]")]
        [SerializeField]
        private List<CardSetting> character = new List<CardSetting>();
        public List<CardSetting> Character { get => character; }

        [Header("[무기 카드]")]
        [SerializeField]
        private List<CardSetting> weapon = new List<CardSetting>();
        public List<CardSetting> Weapon { get => weapon; }

        [Header("[탄약 카드]")]
        [SerializeField]
        private List<CardSetting> ammo = new List<CardSetting>();
        public List<CardSetting> Ammo { get => ammo; }
    }
}
