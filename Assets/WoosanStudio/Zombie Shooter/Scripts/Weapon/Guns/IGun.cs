namespace WoosanStudio.ZombieShooter
{
    public interface IGun : IGunSettings ,IProjectileSettings, IAmmo //, IReload
    {
        bool UseLaserPoint { get; set; }
        //IGunStat GetGunStat();
        ProjectileLauncher ProjectileLauncher { get; set; }
        IProjectileLauncher IProjectileLauncher { get; }

        void SetInputEventHandler(IStart start, IEnd end);

        void Initialize();
    }
}