﻿using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/GunSettings/Make Setting", fileName = "GunData")]
    public class GunSettings : ScriptableObject , IHaveModel , IGunStat , IWeaponStat , IGunSettings
    {
        public enum WeaponType : int
        {
            Pistol = 0,
            AssaultRifle,
            ShotGun,
            LaserRifle,

            //몬스터 무기 타입
            Throw = 100,
        }
        /// <summary>
        /// 무기 타입 - 추가 될 경우를 대비해 int로 만듬
        /// </summary>
        [Header("[무기 타입 - 추가 될 경우를 대비해 int로 만듬]")]
        [SerializeField] private int _index = 0;
        public int Index { get => _index; }

        /// <summary>
        /// 무기 이름
        /// </summary>
        [Header("[무기 이름]")]
        [SerializeField] private string _name = "None";
        public string Name { get => _name; }

        /// <summary>
        /// 생성시 로컬 포지션
        /// </summary>
        [Header("[생성시 로컬 포지션]")]
        [SerializeField] private Vector3 _initPosition;
        public Vector3 InitPosition { get => _initPosition; }

        /// <summary>
        /// 레이저 포인터 생성시 로컬 포지션
        /// </summary>
        [Header("[레이저 포인터 생성시 로컬 포지션]")]
        [SerializeField] private Vector3 _initLaserPointerPosition;
        public Vector3 InitLaserPointerPosition { get => _initLaserPointerPosition; }

        /// <summary>
        /// projectileActor.cs 에서 가져온 클래스
        /// </summary>
        [Header("[발사체 세팅 - projectileActor.cs 에서 가져온 클래스]")]
        [SerializeField] private ProjectileSettings _projectileSettings;



        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IHaveModel Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        /// <summary>
        /// 총기 모델 프리팹
        /// </summary>
        [Header("[총기 모델 프리팹 - [IHaveModel]]")]
        [SerializeField] private GameObject _prefab;
        /// <summary>
        /// 총기 모델 프리팹 인스턴스
        /// </summary>
        [Header("[총기 모델 프리팹 인스턴스 - [IHaveModel]]")]
        [SerializeField] [HideInInspector] private GameObject _prefabInstance;

        public GameObject Prefab { get { return MakeModel(); } }
        public GameObject PrefabInstance { get => _prefabInstance; }
        public ProjectileSettings ProjectileSettings { get => _projectileSettings; }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IWeaponStat Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        [Header("[무기 레벨 - [IWeaponStat]]")]
        [SerializeField] private int _level = 1;
        public int Level { get => _level; }

        public int Type { get => _index; }

        [Header("[무기 데미지 - [IWeaponStat]]")]
        [SerializeField] private int _damage;
        public int Damage { get => _damage = DamageCalculator.GetDamage(this); }
        public DamageCalculator DamageCalculator { get; }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGunStat Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        [Header("[최대 탄약 수 - [IGunStat]]")]
        [SerializeField] private int _maxAmmo = 10;
        public int MaxAmmo { get => _maxAmmo; }

        //현재 탄약은 외부에 노출하지 않음
        private int _currentAmmo = 0;
        public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }

        //ProjectileSetting 에 구현되어 있어서 일단 외부 노출하지 않음
        private float _reloadTime = 2f;
        public float ReloadTime { get => _reloadTime = ReloadTimeCalculator.GetReloadTime(this); }
        public ReloadTimeCalculator ReloadTimeCalculator { get; }

        //ProjectileSetting 에 구현되어 있어서 일단 외부 노출하지 않음
        private float _fireSpeed;
        public float FireSpeed { get => _fireSpeed = FireSpeedCalculator.GetFireSpeed(this);}
        public FireSpeedCalculator FireSpeedCalculator { get; }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGunSettings Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        GunSettings IGunSettings.GunSettings { get { return this; } set { value = this; } }




        /// <summary>
        /// _prefab 을 사용하여 _prefabInstance에 인스턴스 만듬.
        /// </summary>
        /// <returns>_prefabInstance 리턴 </returns>
        public GameObject MakeModel()
        {
            if (_prefab != null)
            {
                _prefabInstance = Instantiate(_prefab) as GameObject;
                _prefabInstance.name = _name;
            }
            else
            {
                Debug.Log("[Error] Gun Prefab is NULL !!");
            }

            return _prefabInstance;
        }


        public GunSettings GetGunSettings()
		{
			return this;
		}
    }
}
