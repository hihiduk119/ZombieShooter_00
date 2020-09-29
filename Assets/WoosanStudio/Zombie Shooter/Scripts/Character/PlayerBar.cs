﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// Health Bar 에셋을 사용하는 클래스.
    /// </summary>
    public class PlayerBar : MonoBehaviour , IHaveHealth
    {
        public int maxHealth = 200;
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int health = 200;
        public int Health { get => health; set => health = value; }
          
        public int reload = 30;

        private DamagedEvent _damagedEvent = new DamagedEvent();
        public DamagedEvent DamagedEvent { get => _damagedEvent; set => throw new System.NotImplementedException(); }

        private UnityEvent _zeroHealthEvent = new UnityEvent();
        public UnityEvent ZeroHealthEvent { get => _zeroHealthEvent; set => throw new System.NotImplementedException(); }

        void Awake()
        {
            _damagedEvent.AddListener(DamagedListener);

            //초기화 
            Initialize();
        }

        /// <summary>
        /// 초기화
        /// </summary>
        public void Initialize()
        {
            //최대 체력에 현재 체력 마춤
            health = maxHealth;
        }

        public void DamagedListener(int damage,Vector3 hit,string keyValue)
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
