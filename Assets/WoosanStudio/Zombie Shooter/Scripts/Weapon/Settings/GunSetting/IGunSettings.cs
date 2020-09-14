namespace WoosanStudio.ZombieShooter
{
    public interface IGunSettings
    {
        GunSettings GunSettings { get; set; }
    }

    public interface IProjectileSettings
    {
        ProjectileSettings ProjectileSettings { get; set; }
    }
}