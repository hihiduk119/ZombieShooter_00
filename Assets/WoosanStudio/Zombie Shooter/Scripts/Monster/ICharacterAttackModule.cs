using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.Character
{
    public interface ICharacterAttackModule
    {
        bool AttackStart { get; set; }

        void Attack(ICharacterAnimatorModule characterAnimatorModule);
    }
}