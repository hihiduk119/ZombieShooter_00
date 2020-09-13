using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IWeapon : IAttackAction
    {
        //void Attack();
        //void Stop();

        IWeaponStat GetWeaponStat();

        GameObject GetInstnace();
    }

    //public class PlayAnimationEvent : UnityEvent<int> { }
}
