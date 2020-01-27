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

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IAttackAction Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
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
            if (_gunStat == null) { _gunStat = (IGunStat)GunSettings; }

            Debug.Log("CurrentAmmo [" + _gunStat.CurrentAmmo + "]");

            if (_gunStat.CurrentAmmo > 0)
            {
                UseAmmo();
                
            } else 
            {
                //사격 중지
                ProjectileLauncher.StopFiring();

                Debug.Log("ReloadEvent!! reloadTime = [" + GunSettings.ReloadTime + "]");

                //재장전 호출 
                ReloadEvent.Invoke(GunSettings.ReloadTime);

                //탄약 재장전
                FullOfAmmo();
            }
            //Debug.Log("Pistol.Fire() ammo => " + _gunStat.CurrentAmmo);
        }

        //void Test(float gaga)
        //{
        //    Debug.Log("Test gaga = " + gaga);
        //}

        /// <summary>
        /// 발사 중지
        /// </지mmary>
        //public void StopFire()
        //{
            
        //}

        /// <summary>
        /// 재장전
        /// </summary>
        //void Reload()
        //{
        //    FullOfAmmo();
        //}

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
            throw new System.NotImplementedException();
        }

        public IWeaponStat GetWeaponStat()
        {
            return (IWeaponStat)GunSettings;
        }

        public void Initialize()
        {
            Debug.Log("Gun Initialize");
            FullOfAmmo();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void SetInputEventHandler(IInputEvents inputEvents)
        {
            ProjectileLauncher.SetInputEventHandler(inputEvents);
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IReloadAction Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        //public void ConnectReloadEvent(IReloadEventSocket reloadEventSocket)
        //{
        //    reloadEventSocket.SetReloadEvent((IReloadEvent)this);
        //}
    }
}


