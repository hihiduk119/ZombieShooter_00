using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class AmmoBar : MonoBehaviour
    {
        [Header("[연결용 건 스텟]")]
        public IGunStat GunStat;

        //현재 탄약
        [SerializeField]
        private int ammo = 10;
        public int Ammo { get => ammo; set => ammo = value; }

        //최대 탄약
        [SerializeField]
        private int maxAmmo = 10;
        public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }

        /// <summary>
        /// 현재 탄약 정보 상태 업데이트
        /// </summary>
        public void UpdateInfo()
        {
            ammo = GunStat.CurrentAmmo;
            maxAmmo = GunStat.MaxAmmo;
        }
    }
}
