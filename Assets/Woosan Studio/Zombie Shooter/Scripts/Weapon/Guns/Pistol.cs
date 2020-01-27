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
        [SerializeField] ReloadEvent _reloadEvent = new ReloadEvent();
        public ReloadEvent ReloadEvent { get => _reloadEvent; set => _reloadEvent = value; }

        private UnityAction _attackAction;
        public UnityAction AttackAction { get { return _attackAction; } set { _attackAction = value; } }

        //캐쉬용
        IGunStat _gunStat;

        void Awake()
        {
            //생성시 런쳐 자동으로 생성
            _projectileLauncher = gameObject.AddComponent<ProjectileLauncher>();

            //액션 등록
            AttackAction += FireControl;

            //발사 런처에 발사할때 마다 FireControl 호출하게 등록
            //ProjectileLauncher.FireActionHandler += FireControl;
            ProjectileLauncher.FireActionHandler += AttackAction;
        }


        /// <summary>
        /// 사격을 통제함.
        /// </summary>
        void FireControl()
        {
            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }

            if (_gunStat.CurrentAmmo > 0)
            {
                UseAmmo();
            } else 
            {
                StopFire();
                Reload();

                ReloadEvent.Invoke(GunSettings.ReloadTime);
            }

            Debug.Log("Pistol.Fire() ammo => " + _gunStat.CurrentAmmo);
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
            FullOfAmmo();
        }

        /// <summary>
        /// 탄약 가득 채움
        /// </summary>
        public void FullOfAmmo()
        {
            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }
            _gunStat.CurrentAmmo = _gunStat.MaxAmmo;
        }

        /// <summary>
        /// 탄 사용 (기본 값은 1)
        /// </summary>
        /// <param name="value">해당 값에 의해 탄소모 값 증가</param>
        public void UseAmmo(int value = 1)
        {
            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }
            _gunStat.CurrentAmmo -= value;
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
            throw new System.NotImplementedException();
        }

        public IWeaponStat GetWeaponStat()
        {
            return (IWeaponStat)GunSettings;
        }

        public void Initialize()
        {
            //FullAmmo();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void SetInputActionHandler(IInputActions inputActions)
        {
            ProjectileLauncher.SetInputActionHandler(inputActions);
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IReloadAction Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        public void ConnectReloadEvent(IReloadEventSocket reloadEventSocket)
        {
            reloadEventSocket.SetReloadEvent((IReloadEvent)this);
        }
    }
}


