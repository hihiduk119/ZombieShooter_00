using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IAttackAction
    {
        UnityAction AttackAction { get; set; }
    }
}