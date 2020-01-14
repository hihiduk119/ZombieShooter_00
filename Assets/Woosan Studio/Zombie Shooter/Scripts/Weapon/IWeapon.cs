namespace WoosanStudio.ZombieShooter
{
    public interface IWeapon
    {
        void Attack();

        IWeaponStat GetWeaponStat();
        IGunStat GetStatLauncher();
        IProjectileLauncher GetProjectileLauncher();
    }
}
