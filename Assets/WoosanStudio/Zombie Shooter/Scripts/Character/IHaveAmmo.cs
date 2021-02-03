using UnityEngine.Events;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IHaveAmmo
    {
        int Ammo { get; set; }
        int MaxAmmo { get; set; }

        void SetMaxAmmo(int maxAmmo);
        //void UsedListener();
        /// <summary>
        /// 탄약이 사용될때 이벤트
        /// </summary>
        UnityEvent FireEvent { get; set; }
        /// <summary>
        /// 탄약이 제로일때 이벤트
        /// </summary>
        UnityEvent ZeroEvent { get; set; }
    }

    //[System.Serializable]
    //public class UsedEvent : UnityEvent<int> { }
}
