using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        void Awake()
        {
            _damagedEvent.AddListener(Damaged);
        }

        public void Damaged(int damage)
        {
            Health -= damage;

            Debug.Log("Health = " + Health);
        }
    }
}
