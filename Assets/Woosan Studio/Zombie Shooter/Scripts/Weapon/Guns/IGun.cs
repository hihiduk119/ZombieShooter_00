namespace WoosanStudio.ZombieShooter
{
    public interface IGun : IReloadEvent, IGunSettings
    {
        bool UseLaserPoint { get; set; }
        //IGunStat GetGunStat();
        ProjectileLauncher ProjectileLauncher { get; set; }

        void SetInputEventHandler(IInputEvents inputEvents);

        void Initialize();
    }
}