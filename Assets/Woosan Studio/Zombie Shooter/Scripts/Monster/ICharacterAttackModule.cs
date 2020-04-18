using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface ICharacterAttackModule
    {
        //UnityEvent HitEvent { get; set; }
        bool AttackStart { get; set; }

        void Attack(ICharacterAnimatorModule characterAnimatorModule);
    }
}