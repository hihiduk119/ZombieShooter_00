using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface ICharacterAttackModule
    {
        bool AttackStart { get; set; }

        void Attack(ICharacterAnimatorModule characterAnimatorModule);
    }
}