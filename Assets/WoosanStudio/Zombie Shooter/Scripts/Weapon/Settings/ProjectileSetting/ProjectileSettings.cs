using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/ProjectileSettings/Make Setting", fileName = "ProjectileData")]
    [System.Serializable]
    public class ProjectileSettings : ScriptableObject
    {
        /// <summary>
        /// 어떤 탄약 타입인지
        /// </summary>
        public enum AmmoType {
            Bullet = 0,
            Laser,
            Plasma,

            MonsterAmmo_0 = 100,
        }

        /// <summary>
        /// 어떤 총에 쓰이는지
        /// </summary>
        public enum GunType {
            Pistol = 0,
            Shotgun,
            AssaultRifle,
            SniperRifle,

            MonsterGun_0 = 100,
        }

        [Header("[어떤 탄약 타입]")]
        public AmmoType ammoType;

        [Header("[어떤 총에 쓰이는지]")]
        public GunType gunType;

        [Header("[기본 데미지 => 기준 데미지]")]
        public int Damage;

        [Header("[이름]")]
        public new string name;
        [Header("[탄 이펙트]")]
        public Rigidbody bombPrefab;
        [Header("[총구 화염 이펙트]")]
        public GameObject muzzleflare;
        [Header("[총구 화염 이펙트 2]")]
        public GameObject muzzleflareExtra;
        [Header("[발사체의 최소 속도]")]
        public float min;
        [Header("[발사체의 최대 속도]")]
        public float max;


        //=> 이동

        //지속 사격
        //public bool rapidFire;
        //사격간 딜레이
        //public float rapidFireCooldown;
        //샷건 사용시 
        //public bool shotgunBehavior;
        //샷건 탄 갯수
        //public int shotgunPellets;
        //=> 여기까지


        //탄피
        [Header("[탄피]")]
        public GameObject shellPrefab;
        //머즐 플레어 사용 할지 말지 결정.
        //몬스터가 공격시는 사용 안함.
        [Header("[머즐 플레어 사용 여부]")]
        public bool hasMuzzleFlare = true;
        //탄피 사용할지 말지 결정.
        [Header("[탄피 사용할지 말지 여부]")]
        public bool hasShells = true;

        //플레이어가 쏜건지 몬스터가 쏜건지 알기 위한 용도.
        [Header("[플레이어가 쏜건지 몬스터가 쏜건지 여부]")]
        public bool playerShoted = true;
    }
}