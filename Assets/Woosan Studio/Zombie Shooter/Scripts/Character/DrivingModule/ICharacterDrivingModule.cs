namespace WoosanStudio.ZombieShooter
{
    public interface ICharacterDrivingModule
    {
        void Tick();
        float Speed { get; }
    }
}