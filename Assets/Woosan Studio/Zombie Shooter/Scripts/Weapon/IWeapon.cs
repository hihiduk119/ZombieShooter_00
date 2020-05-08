using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IWeapon : IAttackAction
    {
        //void Attack();
        //void Stop();

        IWeaponStat GetWeaponStat();
    }

    //public class PlayAnimationEvent : UnityEvent<int> { }
}
