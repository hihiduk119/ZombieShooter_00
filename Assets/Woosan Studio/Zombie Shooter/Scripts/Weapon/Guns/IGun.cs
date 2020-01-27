namespace WoosanStudio.ZombieShooter
{
    public interface IGun : IReloadEvent, IGunSettings
    {
        //IGunStat GetGunStat();
        ProjectileLauncher ProjectileLauncher { get; set; }

        void SetInputEventHandler(IInputEvents inputEvents);

        void Initialize();
    }
}