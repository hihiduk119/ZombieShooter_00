namespace WoosanStudio.ZombieShooter
{
    public interface IGun : IReloadEvent, IGunSettings
    {
        bool UseLaserPoint { get; set; }
        //IGunStat GetGunStat();
        ProjectileLauncher ProjectileLauncher { get; set; }
        IProjectileLauncher IProjectileLauncher { get; }

        void SetInputEventHandler(IInputEvents inputEvents);

        void Initialize();
    }
}