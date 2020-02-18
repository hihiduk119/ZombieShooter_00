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
        public CopyComponets CopyComponets;

        public Transform Accident;

        //Test Code
        public GameObject Prefab;
        public Boom Boom;

        private void Awake()
        {
            Regdoll = GetComponent<Regdoll>();
            NavMeshController = GetComponent<NavMeshController>();
        }

        public void Die()
        {
            NavMeshController.Stop();
            CopyComponets.Copy();
            NavMeshModel.SetActive(false);

            Regdoll.SetActive(true);
            Boom.Pow();

            //Test Code
            MakeObject(Accident.position);
        }

        //Test Code
        void MakeObject(Vector3 position)
        {   
            Transform tf = (Instantiate(Prefab) as GameObject).transform;
            tf.position = position;
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Click left");
                Die();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Click right");
                Regdoll.SetActive(true);
                Boom.Pow();
            }
        }
    }
}
