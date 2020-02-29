using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    [RequireComponent(typeof(RegdollController))]
    [RequireComponent(typeof(NavMeshController))]
    public class DoDie : MonoBehaviour
    {
        
        public GameObject NavMeshModel;
        public CopyComponets CopyComponets;
        //public Transform Accident;

        //cashe
        private RegdollController regdollController;
        private NavMeshController navMeshController;
        private Character character;

        //Test Code
        public GameObject Prefab;
        Boom boom;

        private void Awake()
        {
            regdollController = GetComponent<RegdollController>();
            navMeshController = GetComponent<NavMeshController>();
            character = GetComponent<Character>();
        }

        public void CheckHealth()
        {

        }

        public void Die()
        {
            character.isDead = true;

            navMeshController.Stop();
            CopyComponets.Copy();
            NavMeshModel.SetActive(false);

            regdollController.SetActive(true);
            boom = new Boom(transform.position);

            //Test Code
            //MakeObject(Accident.position);
        }

        //Test Code
        //void MakeObject(Vector3 position)
        //{   
        //    Transform tf = (Instantiate(Prefab) as GameObject).transform;
        //    tf.position = position;
        //}

        //Test Code
        //void Update()
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        Debug.Log("Click left");
        //        Die();
        //    }

        //    if (Input.GetButtonDown("Fire2"))
        //    {
        //        Debug.Log("Click right");
        //        regdollController.SetActive(true);
        //        boom = new Boom(transform.position);
        //    }
        //}
    }
}
