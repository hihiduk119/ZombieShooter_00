namespace WoosanStudio.ZombieShooter.Character
{
    /// <summary>
    /// 에니메이션 상태
    /// </summary>
    public enum AnimatorState
    {
        Idle = 0,
        Move,
        Attack,
    }

    public interface ICharacterAnimatorModule
    {
        AnimatorState State { get; }

        void Idle();
        void Move(float speed);
        void Attack();
    }
}