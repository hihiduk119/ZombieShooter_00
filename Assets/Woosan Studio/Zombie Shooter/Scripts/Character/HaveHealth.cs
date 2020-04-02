using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// Health Bar 에셋을 사용하는 클래스.
    /// </summary>
    public class HaveHealth : MonoBehaviour , IHaveHealth
    {
        public int health = 200;
        public int Health { get => health; set => health = value; }

        private DamagedEvent _damagedEvent = new DamagedEvent();
        public DamagedEvent DamagedEvent { get => _damagedEvent; set => throw new System.NotImplementedException(); }

        private UnityEvent _zeroHealthEvent = new UnityEvent();
        public UnityEvent ZeroHealthEvent { get => _zeroHealthEvent; set => throw new System.NotImplementedException(); }

        void Awake()
        {
            _damagedEvent.AddListener(DamagedListener);
        }

        public void DamagedListener(int damage,Vector3 hit)
        {
            Health -= damage;

            //체력이 0 이하면 호출
            if(Health <= 0)
            {
                _zeroHealthEvent.Invoke();
            }
        }
    }
}
