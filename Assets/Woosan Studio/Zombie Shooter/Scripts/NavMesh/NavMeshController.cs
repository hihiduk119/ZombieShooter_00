using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


namespace WoosanStudio.ZombieShooter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class NavMeshController : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private new Rigidbody rigidbody;
        private CapsuleCollider capsuleCollider;

        private void Awake()
        {
            navMeshAgent = GetComponent <NavMeshAgent>();
            rigidbody = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        public void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            navMeshAgent.enabled = false;
            capsuleCollider.enabled = false;
        }
    }
}
