using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public interface IProjectileLauncherActions
    {
        /// <summary>
        /// 발사시 호출할 액션.
        /// </summary>
        UnityAction FireActionHandler { get; set; }

        /// <summary>
        /// 발사시 호출할 이벤트.
        /// </summary>
        //UnityEvent FireEventHandler { get; set; }
    }

    public class FireEvent : UnityEvent<IGunStat> { }
}