using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Woosan.SurvivalGame01
{
    [RequireComponent(typeof(EnemyData))]
    [RequireComponent(typeof(EnemyView))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        private AttackEvent attackEvent;
        public AttackEvent GetAttackEvent => attackEvent;
        public Transform tfModel;

        [HideInInspector] public EnemyData enemyData;
        [HideInInspector] public EnemyView enemyView;

        void Awake()
        {
            Debug.Log("Enemy Awake");

            attackEvent = new AttackEvent();
            enemyData = GetComponent<EnemyData>();
            enemyView = GetComponent<EnemyView>();
        }

        public void Attack()
        {
            float dmg = 0 ;
            attackEvent.Invoke(dmg);
        }

        public void Dead()
        {

        }

        public void Respawn()
        {

        }
    }
}
