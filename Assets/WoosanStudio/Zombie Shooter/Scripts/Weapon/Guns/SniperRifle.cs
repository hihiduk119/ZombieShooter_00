﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using DarkTonic.MasterAudio;

namespace WoosanStudio.ZombieShooter
{
    public class SniperRifle : MonoBehaviour , IWeapon, IGun
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

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGun.IProjectileSettings Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        private ProjectileSettings _projectileSettings;
        public ProjectileSettings ProjectileSettings { get => _projectileSettings; set => _projectileSettings = value; }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGun.IAmmo Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        public UnityEvent EmptyEvent => emptyEvent;
        private UnityEvent emptyEvent = new UnityEvent();

        //캐쉬용
        IGunStat gunStat;

        void Awake()
        {
            //생성시 런쳐 자동으로 생성
            _projectileLauncher = gameObject.AddComponent<ProjectileLauncher>();

            //발사 런처에 발사할때 마다 AttackAction 호출하게 등록
            ProjectileLauncher.TriggerEvent.AddListener(FireControl);
        }

        /// <summary>
        /// 사격을 통제함.
        /// </summary>
        void FireControl()
        {
            //호출시 이미 탄이 발사 되었기 때문에 탄사용 먼저 호출.
            UseAmmo();

            if (gunStat == null) { gunStat = (IGunStat)GunSettings; }

            //Debug.Log("Max [" + _Model.MaxAmmo + "]   CurrentAmmo [" + _Model.CurrentAmmo + "]");

            if (gunStat.CurrentAmmo == 0)
            {
                //사격 중지
                ProjectileLauncher.StopFiring();

                //Debug.Log("ReloadEvent!! reloadTime = [" + GunSettings.ReloadTime + "]");

                //재장전 호출 
                EmptyEvent.Invoke();

                //탄약 재장전
                FullOfAmmo();

                //리로드 사운드
                MasterAudio.FireCustomEvent("CustomEvent_ReloadSniperRifle", this.transform);

                //Debug.Log("Reload!!");
            }
            //Debug.Log("Pistol.Fire() ammo => " + _Model.CurrentAmmo);
        }

        /// <summary>
        /// 탄약 가득 채움
        /// </summary>
        public void FullOfAmmo()
        {
            if (gunStat == null) { gunStat = (IGunStat)GunSettings; }
            gunStat.CurrentAmmo = gunStat.MaxAmmo;
        }

        /// <summary>
        /// 탄 사용 (기본 값은 1)
        /// </summary>
        /// <param name="value">해당 값에 의해 탄소모 값 증가</param>
        public void UseAmmo(int value = 1)
        {
            if (gunStat == null) { gunStat = (IGunStat)GunSettings; }
            gunStat.CurrentAmmo -= value;
        }

        /// <summary>
        /// 발사 런처의 액션들을 가져오기.
        /// </summary>
        /// <returns></returns>
        public IProjectileLauncherEvents GetProjectileLauncherEvents()
        {
            return (IProjectileLauncherEvents)_projectileLauncher;
        }

        //public IInputEvents GetInput()
        //{
        //    return null;
        //}

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

        public GameObject GetInstnace()
        {
            return this.gameObject;
        }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGun Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


        public void SetInputEventHandler(IStart start, IEnd end)
        {
            ProjectileLauncher.SetInputEventHandler(start, end);
        }

        public void Initialize()
        {
            Debug.Log("Gun Initialize");
            FullOfAmmo();
        }
    }
}