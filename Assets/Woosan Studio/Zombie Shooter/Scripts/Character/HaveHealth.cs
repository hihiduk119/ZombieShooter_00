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
        private int _health = 200;
        public int Health { get => _health; set => _health = value; }

        public void AddDamage(int damage)
        {
            Health -= damage;
        }
    }
}
