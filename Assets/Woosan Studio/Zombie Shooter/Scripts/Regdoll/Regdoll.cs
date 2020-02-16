using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class Regdoll : MonoBehaviour
    {
        public GameObject Model;
        public Rigidbody[] Rigidbodies;

        public float Power = 2f;
        public Vector3 ExplosionPos;
        public float Radius = 1f;
        public float UpPower = 3f;



        private void Awake()
        {
            Rigidbodies = Model.GetComponentsInChildren<Rigidbody>();
            Model.SetActive(false);
        }

        public void ExplosionForce(Vector3 explosionPos)
        {
            Model.SetActive(true);
            for (int index = 0; index < Rigidbodies.Length ;index++)
            {
                Rigidbodies[index].AddExplosionForce(Power, explosionPos, Radius, UpPower);
            }
        }
    }
}
