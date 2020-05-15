using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class ShootController : MonoBehaviour
    {
        public GameObject impactPrefab;
        public GameObject muzzlePrefab;
        public GameObject projectilePrefab;

        public Transform spawnLocator;

        float speed = 1000f;

        IEnumerator Start()
        {
            float delay = 0.2f;

            while(true)
            {
                yield return new WaitForSeconds(delay);
                Fire();
            }
        }

        void Fire()
        {
            GameObject clone = Lean.Pool.LeanPool.Spawn(projectilePrefab, spawnLocator.position, spawnLocator.rotation);
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddForce(spawnLocator.forward * speed);
        }
    }
}
