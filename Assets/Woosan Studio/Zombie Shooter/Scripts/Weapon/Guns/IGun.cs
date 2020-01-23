namespace WoosanStudio.ZombieShooter
{
    public interface IGun : IReloadEvent, IGunSettings
    {
        //IGunStat GetGunStat();
        ProjectileLauncher ProjectileLauncher { get; set; }

        void SetInputActionHandler(IInputActions inputActions);

        void Initialize();
    }
}