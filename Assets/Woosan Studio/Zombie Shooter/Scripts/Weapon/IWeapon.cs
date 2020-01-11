namespace WoosanStudio.ZombieShooter
{
    public interface IWeapon
    {
        void Attack();
        GunSettings GunSettings { get; set; }
        //void Stop();
    }
}
