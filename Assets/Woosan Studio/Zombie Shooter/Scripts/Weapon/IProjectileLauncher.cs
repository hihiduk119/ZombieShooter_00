namespace WoosanStudio.ZombieShooter
{
    public interface IProjectileLauncher
    {
        ProjectileLauncher ProjectileLauncher { get; set; }
        GunSettings GunSettings { get; set; }
    }
}