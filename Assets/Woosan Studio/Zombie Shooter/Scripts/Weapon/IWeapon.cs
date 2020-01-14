using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IWeapon
    {
        void Attack();
        void Stop();

        IWeaponStat GetWeaponStat();
        //IGunStat GetGunStat();
        //IProjectileLauncher GetProjectileLauncher();
    }
}
