using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class Pistol : MonoBehaviour , IWeapon , IProjectileLauncher
    {
        private ProjectileLauncher _projectileLauncher;
        public ProjectileLauncher ProjectileLauncher { get => _projectileLauncher; set => _projectileLauncher = value; }

        private GunSettings _gunSettings;
        public GunSettings GunSettings { get => _gunSettings; set => _gunSettings = value; }

        //캐쉬용
        IGunStat _gunStat;

        void Start()
        {
            //런쳐에서 탄 발사시 호출 등록.
            GetProjectileLauncherActions().FireActionHandler += UseAmmo;
            _gunStat = (IGunStat)GunSettings;
        }

        void Fire()
        {
            Debug.Log("= Fire = ");
            if(_gunStat.CurrentAmmo > 0)
            {
                Debug.Log("= 0 = ");
                _projectileLauncher.Fire();
                UseAmmo();
            } else
            {
                Debug.Log("= 1 = ");
                Reload();
            }
        }

        /// <summary>
        /// 발사 중지
        /// </summary>
        public void Stop()
        {
            ProjectileLauncher.StopFiring();
        }


        void Reload()
        {
            ReloadAmmo();
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IProjectileLauncher Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        /// <summary>
        /// 재장전
        /// </summary>
        public void ReloadAmmo()
        {
            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }
            _gunStat.CurrentAmmo = _gunStat.MaxAmmo;
        }

        /// <summary>
        /// 탄 사용
        /// </summary>
        public void UseAmmo()
        {
            _gunStat.CurrentAmmo--;

            Debug.Log("남은 탄약 = " + _gunStat.CurrentAmmo);
            if (_gunStat.CurrentAmmo == 0) { Stop(); }
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

        public IGunStat GetStatLauncher()
        {
            return (IGunStat)GunSettings;
        }

        public IWeaponStat GetWeaponStat()
        {
            return (IWeaponStat)GunSettings;
        }

        public IProjectileLauncher GetProjectileLauncher()
        {
            return (IProjectileLauncher)this;
        }

        
    }
}


