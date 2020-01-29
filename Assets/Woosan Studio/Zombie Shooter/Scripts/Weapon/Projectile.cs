using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public Vector3 Force;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            _rigidbody.AddForce(Force);
        }
    }
}