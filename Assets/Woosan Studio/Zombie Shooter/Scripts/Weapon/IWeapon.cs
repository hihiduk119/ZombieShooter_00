using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IWeapon
    {
        void Attack();

        IWeaponStat GetWeaponStat();

        //IGunStat GetGunStat();
        //IProjectileLauncher GetProjectileLauncher();
    }
}
