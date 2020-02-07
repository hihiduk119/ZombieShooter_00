namespace WoosanStudio.ZombieShooter
{
    public interface ICharacterInput
    {
        float Horizontal { get; }
        float Vertical { get; }

        void ReadInput();
    }
}