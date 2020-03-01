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

        //cache
        private RegdollController regdollController;
        private NavMeshController navMeshController;
        private Character character;
        private IHaveHealth haveHealth;
        private DoZeroGravity doZeroGravity;

        //Test Code
        //public GameObject testDummy;
        Boom boom;

        private void Awake()
        {
            regdollController = GetComponent<RegdollController>();
            navMeshController = GetComponent<NavMeshController>();
            character = GetComponent<Character>();
            haveHealth = GetComponent<IHaveHealth>();
            doZeroGravity = GetComponent<DoZeroGravity>();

            //IHaveHealth 에 체력 체크 등록.
            haveHealth.DamagedEvent.AddListener(CheckHealth);

            //Test Code
            //testDummy = GameObject.FindGameObjectWithTag("TestDummy");
        }

        //체력 체크용 콜백 함수
        //
        public void CheckHealth(int damage,Vector3 hit)
        {
            if(haveHealth.Health < 0) { Die(hit); }
        }

        public void Die(Vector3 hit)
        {
            character.isDead = true;

            navMeshController.Stop();
            CopyComponets.Copy();
            NavMeshModel.SetActive(false);

            regdollController.SetActive(true);

            //Add Force
            boom = new Boom(hit);
            //testDummy.transform.position = hit;

            //Test Code
            //MakeObject(Accident.position);

            Invoke("GoToHeaven", 3f);
            Debug.Log("=================>    GoToHeaven");
        }

        void GoToHeaven()
        {
            //X 값은 연출용 값으로 값이 클수록 화면 쪽으로 시체가 앞으록 움직임.
            //Y 값은 떠오르는 속도이며 값이 크면 빠르게 떠오름.
            doZeroGravity.ZeroGravity(new Vector3(10, 10, 0));
            Debug.Log("=================>    Go       ToHeaven");
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
