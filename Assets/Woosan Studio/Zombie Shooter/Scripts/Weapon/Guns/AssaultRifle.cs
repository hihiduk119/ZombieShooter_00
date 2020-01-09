using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class AssaultRifle : MonoBehaviour , IGunStat , IWeapon , IProjectileLauncher
    {
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGunStat Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        public int MaxAmmo { get; private set; }
        public int CurrentAmmo { get; private set; }
        public int GetCurrentAmmo { get; private set; }
        public int Level { get; private set; }
        public int Type { get; private set; }

        private int _damage;
        public int Damage
        {
            get => _damage;
            private set
            {
                if (DamageCalculator != null)
                {
                    DamageCalculator = new DamageCalculator();
                }
                value = DamageCalculator.GetDamage(Type, Level);
            }
        }
        public DamageCalculator DamageCalculator { get; private set; }

        private int _reloadTime;
        public float ReloadTime
        {
            get => _reloadTime;
            private set
            {
                if (ReloadTimeCalculator != null)
                {
                    ReloadTimeCalculator = new ReloadTimeCalculator();
                }
                value = ReloadTimeCalculator.GetReloadTime(Type, Level);
            }
        }
        public ReloadTimeCalculator ReloadTimeCalculator { get; private set; }

        private int _fireSpeed;
        public float FireSpeed
        {
            get => _fireSpeed;
            private set
            {
                if (FireSpeedCalculator != null)
                {
                    FireSpeedCalculator = new FireSpeedCalculator();
                }
                value = FireSpeedCalculator.GetFireSpeed(Type, Level);
            }
        }

        public FireSpeedCalculator FireSpeedCalculator { get; private set; }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IWeapon Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IProjectileLauncher Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        private ProjectileLauncher projectileLauncher;
        public ProjectileLauncher ProjectileLauncher { get => projectileLauncher; set => projectileLauncher = value; }

        private GunSettings gunSettings;
        public GunSettings GunSettings { get => gunSettings; set => gunSettings = value; }
    }
}
