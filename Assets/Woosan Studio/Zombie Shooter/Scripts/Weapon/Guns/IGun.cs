namespace WoosanStudio.ZombieShooter
{
    public interface IGun : IGunActions, IGunSettings
    {
        //IGunStat GetGunStat();
        ProjectileLauncher ProjectileLauncher { get; set; }

        void Initialize();
    }
}