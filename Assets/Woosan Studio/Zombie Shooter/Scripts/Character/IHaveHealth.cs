namespace WoosanStudio.ZombieShooter
{
    public interface IHaveHealth
    {
        int Health { get; set; }
        void AddDamage(int damage);
    }
}