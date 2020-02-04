using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Projectile : MonoBehaviour, Lean.Pool.IPoolable
    {
        private Rigidbody _rigidbody;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnSpawn()
        {
            //Debug.Log("Hi OnSpawn");
        }

        public void OnDespawn()
        {
            //Debug.Log("Hi OnDespawn");

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}