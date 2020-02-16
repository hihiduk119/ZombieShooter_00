using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    [RequireComponent(typeof(Regdoll))]
    [RequireComponent(typeof(NavMeshController))]
    public class DoDie : MonoBehaviour
    {
        public Regdoll Regdoll;
        public NavMeshController NavMeshController;
        public GameObject NavMeshModel;

        public Transform Accident;

        private void Awake()
        {
            Regdoll = GetComponent<Regdoll>();
            NavMeshController = GetComponent<NavMeshController>();
        }

        public void Die()
        {
            NavMeshController.Stop();
            Regdoll.ExplosionForce(Accident.position);
            NavMeshModel.SetActive(false);
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("hi~");
                Die();
            }
        }
    }
}
