﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    [RequireComponent(typeof(RegdollController))]
    [RequireComponent(typeof(NavMeshController))]
    public class DoDie : MonoBehaviour 
    {
        
        public GameObject NavMeshModel;
        public CopyComponets CopyComponets;

        //cache
        private RegdollController regdollController;
        private NavMeshController navMeshController;
        //캐릭터에 죽음 체크용
        private Monster monster;
        //체력 계산용
        private IHaveHealth haveHealth;
        //죽었을때 무중력으로 떠오르게 하기 위해
        private DoZeroGravity doZeroGravity;
        //데미지입었을때 빨간 블링크 용
        private IBlink blink;
        //죽을 오브젝트를 세팅
        private ICanDestory canDestory;

        //Test Code
        //public GameObject testDummy;
        Boom boom;

        [Header("[죽은다음 하늘로 이동할때 이벤트 발생]")]
        public GoToHeavenEvent HeavenEvent = new GoToHeavenEvent();
        //죽은 위치의 좌표를 위해 Vector3 포함
        public class GoToHeavenEvent : UnityEvent<Vector3> { }

        private void Awake()
        {
            regdollController = GetComponent<RegdollController>();
            navMeshController = GetComponent<NavMeshController>();
            monster = GetComponent<Monster>();
            haveHealth = GetComponent<IHaveHealth>();
            doZeroGravity = GetComponent<DoZeroGravity>();
            blink = transform.GetComponentInChildren<IBlink>();
            canDestory = GetComponent<ICanDestory>();

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
            //데미지 이벤트 등록된 모든 리스너 등록해제
            //해제를 안하면 데미지 받는데로 행동 실행.
            haveHealth.DamagedEvent.RemoveAllListeners();

            blink.Initialize();

            monster.Die();
            navMeshController.Stop();
            CopyComponets.Copy();
            NavMeshModel.SetActive(false);

            regdollController.SetActive(true);

            //Add Force
            boom = new Boom(hit);
            //testDummy.transform.position = hit;

            Invoke("GoToHeaven", 3f);
            //Debug.Log("=================>    GoToHeaven");
        }

        /// <summary>
        /// 하늘로 떠오르며 죽는 연출
        /// </summary>
        void GoToHeaven()
        {
            //하늘로 올름 이벤트 발생
            HeavenEvent.Invoke(transform.position);

            //X 값은 연출용 값으로 값이 클수록 화면 쪽으로 시체가 앞으록 움직임.
            //Y 값은 떠오르는 속도이며 값이 크면 빠르게 떠오름.
            doZeroGravity.UpForce(new Vector3(0, 7.5f, 0));

            if (canDestory != null)
            {
                //연결돈 모든 리스너 삭제
                HeavenEvent.RemoveAllListeners();
                canDestory.Destory(2.5f);
            }
            else
            {
                Debug.Log("ICanDestory Null!!. 해당 오브젝트를 세팅 해주세요.");
            }
                
            //Debug.Log("=================>    Go       ToHeaven");
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
