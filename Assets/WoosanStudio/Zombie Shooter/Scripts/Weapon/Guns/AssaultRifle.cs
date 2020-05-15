using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class AssaultRifle : MonoBehaviour , IWeapon ,  IGun
    {
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGun Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        private bool _useLaserPoint;
        public bool UseLaserPoint { get => _useLaserPoint; set => _useLaserPoint = value; }

        private ProjectileLauncher _projectileLauncher;
        public ProjectileLauncher ProjectileLauncher { get => _projectileLauncher; set => _projectileLauncher = value; }
        public IProjectileLauncher IProjectileLauncher { get => (IProjectileLauncher)_projectileLauncher; }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGun.IGunSettings Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        private GunSettings _gunSettings;
        public GunSettings GunSettings { get => _gunSettings; set => _gunSettings = value; }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGun.IReloadEvent Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        [SerializeField] ReloadEvent _reloadEvent = new ReloadEvent();
        public ReloadEvent ReloadEvent { get => _reloadEvent; set => _reloadEvent = value; }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IWeapon.IAttackAction Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        public UnityAction AttackAction { get; set; }


        //캐쉬용
        IGunStat _gunStat;


        void Awake()
        {
            //생성시 런쳐 자동으로 생성
            _projectileLauncher = gameObject.AddComponent<ProjectileLauncher>();

            //액션 등록
            AttackAction += FireControl;
            //발사 런처에 발사할때 마다 AttackAction 호출하게 등록
            ProjectileLauncher.TriggerEvent.AddListener(AttackAction);
        }

        /// <summary>
        /// 사격을 통제함.
        /// </summary>
        void FireControl()
        {
            //호출시 이미 탄이 발사 되었기 때문에 탄사용 먼저 호출.
            UseAmmo();

            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }

            //Debug.Log("Max [" + _gunStat.MaxAmmo + "]   CurrentAmmo [" + _gunStat.CurrentAmmo + "]");

            if (_gunStat.CurrentAmmo == 0)
            {
                //사격 중지
                ProjectileLauncher.StopFiring();

                //Debug.Log("ReloadEvent!! reloadTime = [" + GunSettings.ReloadTime + "]");

                //재장전 호출 
                ReloadEvent.Invoke(GunSettings.ReloadTime);

                //탄약 재장전
                FullOfAmmo();

                //Debug.Log("======== Reload!! ==========");
            }
            //Debug.Log("Pistol.Fire() ammo => " + _gunStat.CurrentAmmo);
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
        public IProjectileLauncherEvents GetProjectileLauncherEvents()
        {
            return (IProjectileLauncherEvents)_projectileLauncher;
        }

        public IInputEvents GetInput()
        {
            return null;
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IWeapon Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        public void Attack()
        {

        }

        public void Stop()
        {

        }

        public IWeaponStat GetWeaponStat()
        {
            return (IWeaponStat)GunSettings;
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGun Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


        public void SetInputEventHandler(IInputEvents inputEvents)
        {
            ProjectileLauncher.SetInputEventHandler(inputEvents);
        }

        public void Initialize()
        {
            Debug.Log("Gun Initialize");
            FullOfAmmo();
        }
    }
}
