using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class Pistol : MonoBehaviour , IWeapon ,  IGun
    {
        private ProjectileLauncher _projectileLauncher;
        public ProjectileLauncher ProjectileLauncher { get => _projectileLauncher; set => _projectileLauncher = value; }

        private GunSettings _gunSettings;
        public GunSettings GunSettings { get => _gunSettings; set => _gunSettings = value; }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGunActions Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        public UnityAction FireAction { get; set; }
        public UnityAction ReloadAction { get; set; }

        //캐쉬용
        IGunStat _gunStat;

        void Awake()
        {
            //생성시 런쳐 자동으로 생성
            _projectileLauncher = gameObject.AddComponent<ProjectileLauncher>();
            
            //액션 등록
            FireAction += Fire;
            ReloadAction += Reload;
        }

        void Fire()
        {
            Debug.Log("Fire");
            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }

            if (_gunStat.CurrentAmmo > 0)
            {
                Debug.Log("ammo 10");
                _projectileLauncher.Fire();
                UseAmmo();
            } else
            {
                FullAmmo();
                Debug.Log("ammo 0");
            }
        }

        /// <summary>
        /// 발사 중지
        /// </summary>
        public void StopFire()
        {
            ProjectileLauncher.StopFiring();
        }

        /// <summary>
        /// 재장전
        /// </summary>
        void Reload()
        {
            FullAmmo();
        }

        
        public void FullAmmo()
        {
            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }
            _gunStat.CurrentAmmo = _gunStat.MaxAmmo;
        }

        /// <summary>
        /// 탄 사용
        /// </summary>
        public void UseAmmo()
        {
            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }
            _gunStat.CurrentAmmo--;

            Debug.Log("남은 탄약 = " + _gunStat.CurrentAmmo);
            if (_gunStat.CurrentAmmo == 0) { StopFire(); }
        }

        /// <summary>
        /// 발사 런처의 액션들을 가져오기.
        /// </summary>
        /// <returns></returns>
        public IProjectileLauncherActions GetProjectileLauncherActions()
        {
            return (IProjectileLauncherActions)_projectileLauncher;
        }

        public IInputActions GetInput()
        {
            return null;
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IWeapon Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        public void Attack()
        {
            Fire();
        }

        public IWeaponStat GetWeaponStat()
        {
            return (IWeaponStat)GunSettings;
        }

        public void Initialize()
        {
            //FullAmmo();
        }
    }
}


