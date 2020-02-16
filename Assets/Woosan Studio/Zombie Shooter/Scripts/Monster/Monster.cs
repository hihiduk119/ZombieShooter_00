using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Monster : MonoBehaviour, IHaveMonsterStat
    {
        [SerializeField]
        private int _health;

        public int Health { get => _health; set => _health = value; }
    }
}
