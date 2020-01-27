using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IProjectileLauncherEvents
    {
        /// <summary>
        /// 발사시 호출할 액션.
        /// </summary>
        UnityEvent TriggerEvent { get; set; }
    }

    //public class FireEvent : UnityEvent<IGunStat> { }
}