using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.Character
{
    public enum AttackState
    {
        Idle,
        Attacking,
    }

    public interface ICharacterAttackModule
    {
        bool AttackStart { get; set; }

        void Attack(ICharacterAnimatorModule characterAnimatorModule);
    }
}