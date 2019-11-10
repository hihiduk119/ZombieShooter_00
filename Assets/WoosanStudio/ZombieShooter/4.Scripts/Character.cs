﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

using System;

namespace Woosan.SurvivalGame
{
    public class Character : MonoBehaviour
    {
        public static Character instance;
        //좀비의 공격
        //public UnityAction<ZombieKinds> attackAction;
        //사거리에 걸림
        //public UnityAction<Transform> rangeEnterAction;
        //사거리에 걸림에서 빠짐
        //public UnityAction<Transform> rangeExitAction;

        //사거리에 들어온 좀비 리스트
        List<Transform> zombies = new List<Transform>();
        //사거리 관련 
        public Range range;
        //에니메이션 컨트롤
        public Animator animator;

        //네비메쉬 [실제 이동 담당]
        //public NavMeshAgent navMeshAgent;
        //서드퍼슨 컨트롤 [ 애니메이션만 담당 , 회전도 담 당]
        public ThirdPersonCharacter thirdPersonCharacter;


        float horizon;
        float vertical;
        Vector3 desiredVelocity;
        //캠의 방향으로 조이스틱 조정하기 위해 사용
        private Transform cam;                  
        private Vector3 camForward;
        //움직임 관련
        bool aimed = false;
        //조준이 되는 에임 마커
        public AimMarker aimMarker;

        //발사체 관련
        public projectileActor m_projectileActor;
        //현재 사격중인지 아닌지 여부
        bool firing = false;
        bool isReloading = false;
        Coroutine corFire;

        //현재 설정된 최대 총알수
        int bulletMagazineMaxCount = 30;
        //현재 남은 총알수
        int bulletMagazineCount = 0;

        //사격시 발생하는 BlobLight
        public GameObject objGunFireBlobLights;


        //추가로 가져온 부분
        //private float horizon;
        //private float vertical;
        //private Vector3 desiredVelocity;
        //캠의 방향으로 조이스틱 조정하기 위해 사용
        //public Transform cam;
        //private Vector3 camForward;
        //최적화 캐슁
        private SkinnedMeshRenderer myRenderer;

        //테스트용 움직임 랙이 생기는것 때문에 시간재기
        float deltaTime = 0;
        float preDeltaTime = 0;

        IEnumerator WaitAndDo(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            //네비게이션 세팅
            //navMeshAgent.updateRotation = false;

            //액션 설정
            //rangeEnterAction = new UnityAction<Transform>(EnterGunRange);
            //rangeExitAction = new UnityAction<Transform>(ExitGunRange);

            //초기 이벤트 세팅
            //range.triggerEnterEvent.AddListener(rangeEnterAction);
            //range.triggerExitEvent.AddListener(rangeExitAction);

            //좀비의 액션에 대한 콜백메서드 세팅
            //attackAction = new UnityAction<ZombieKinds>(BeAttackedCallback);

            if (Camera.main != null)
            {
                cam = Camera.main.transform;
            }

            //재시작용 초기화
            Reset();
        }

        public void Reset()
        {
            //시작시 에임 마커 디스에이블
            aimMarker.gameObject.SetActive(false);
            //사격중지
            m_projectileActor.Stop();
        }

        public void BeAttackedCallback(ZombieKinds zombieKinds)
        {
            Debug.Log("좀비에게 공격받음");
        }

        /// <summary>
        /// 좀비가 제거 됐을때 호출
        /// </summary>
        /// <param name="target">Target.</param>
        public void TargetDead(Transform target) 
        {
            //죽은 넘인지 아닌지 부터 확인 => 죽은 넘이면
            //좀비가 생존하지 않았다면
            //if (!target.GetComponent<Enemy>().IsAlive)
            //{
            //    firing = false;
            //    DontFire();
            //}

            //남은 적이 없다면 사격 비활성화
            if (zombies.Count == 0)
            {
                DontFire();
            }

            //죽은 좀비 찾아서 제거a
            if (!zombies.Find(value => value.Equals(target.name)))
            {
                //있다면 제거
                int index = zombies.FindIndex(value => value.name.Equals(target.name));
                zombies.RemoveAt(index);
            }
        }

        /// <summary>
        /// 총 사거리 들어옴
        /// </summary>
        /// <param name="target">Target.</param>
        public void EnterGunRange(Transform target) {

            //리스트에서 기존에 있는지 없는지 확인[없다]
            if (!zombies.Find(value => value.Equals(target.name))) 
            {
                //없다면 추가
                zombies.Add(target);
            }

            //Debug.Log("좀비에게 사거리 들어옴  count = " + zombies.Count);


            //사격중이 아니었다면
            if(!firing) {
                if (corFire != null) { StopCoroutine(corFire); }
                corFire = StartCoroutine(CorFire());
                //사격 활성화
                firing = true;
            }
        }

        /// <summary>
        /// 총 사거리 벗어남
        /// </summary>
        /// <param name="target">Target.</param>
        public void ExitGunRange(Transform target) {

            //리스트에서 기존에 있는지 없는지 확인
            if (zombies.Find(value => value.name.Equals(target.name)))
            {
                //있다면 제거
                zombies.RemoveAt(zombies.FindIndex(value => value.name.Equals(target.name)));
            }

            //Debug.Log("좀비에게 사거리 벗어남  count = " + zombies.Count);
            //남은 적이 없다면 사격 비활성화
            if (zombies.Count == 0) {
                DontFire();
            }
        }

        /// <summary>
        /// 사격 중지
        /// </summary>
        void DontFire()
        {
            if (corFire != null) { StopCoroutine(corFire); }
            firing = false;
        }

        IEnumerator CorFire() {
            //사격 딜레이
            WaitForSeconds delay = new WaitForSeconds(.075f);
            yield return new WaitForSeconds(0.1f);
            while (true) {
                //재장전 중이 아닐때만 사격
                if(!isReloading) {
                    m_projectileActor.Fire();
                    //총기 화염 표현
                    this.objGunFireBlobLights.SetActive(true);
                    StartCoroutine(WaitAndDo(0.07f, () => {
                        this.objGunFireBlobLights.SetActive(false);
                    }));


                    AudioManager.instance.GunShot(SoundOneshot.RifleOne_00);
                    bulletMagazineCount++;
                    if(bulletMagazineCount >= bulletMagazineMaxCount) {
                        Reload();
                        bulletMagazineCount = 0;
                    }
                }
                yield return delay;

            }
        }

        private void Update()
        {

            LookAtTarget();
            Move();

            //Test code
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }

        void Reload()
        {
            animator.SetTrigger("Reload");
            AudioManager.instance.OneShot(SoundOneshot.RifleOne_Reload_00);
            isReloading = true;
        }

        //재장전 완료시
        public void ReloadEnd() {
            isReloading = false;
        }

        /// <summary>
        /// 캐릭터의 움직임 컨트롤
        /// </summary>
        /*private void Move()
        {
            //실제 조이스틱 값 가져오는 부분
            horizon = UltimateJoystick.GetHorizontalAxis("Move");
            vertical = UltimateJoystick.GetVerticalAxis("Move");

            preDeltaTime = deltaTime;
            deltaTime += Time.deltaTime;

            //Debug.Log("현재["+deltaTime + "]   간격[" + (deltaTime - preDeltaTime) + "]   =====> h = " + vertical + " v = " + vertical);

            if (cam != null)
            {
                //카메라 기준으로 조이스틱 방향성 바꿔줌
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                desiredVelocity = vertical * camForward + horizon * cam.right;
            }
            else
            {
                //카메라가 없다면 기본 방향
                desiredVelocity = vertical * Vector3.forward + horizon * Vector3.right;
            }
            //이동을 담당 (이동을 하면 안된다. 에이전트들이 많아지면 조금씩멈춤 현상 발생 )
            //navMeshAgent.destination = transform.position + desiredVelocity;
            //애니메이션 움직임 담당 [회전 포함]
            //실제 이동 없이 회전 만 시키기 위해서 MoveSpeedMultiplier 값을 0으로 바꿈
            //thirdPersonCharacter.Move(desiredVelocity, false, false);
        }*/

        private void Move()
        {
            //실제 조이스틱 값 가져오는 부분
            horizon = UltimateJoystick.GetHorizontalAxis("Move");
            vertical = UltimateJoystick.GetVerticalAxis("Move");

            //Debug.Log("h = " + vertical + " v = " + vertical);

            if (cam != null)
            {
                //카메라 기준으로 조이스틱 방향성 바꿔줌
                camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
                desiredVelocity = vertical * camForward + horizon * cam.right;
            }
            else
            {
                //카메라가 없다면 기본 방향
                desiredVelocity = vertical * Vector3.forward + horizon * Vector3.right;
            }
            //실제 이동을 담당
            //navMeshAgent.destination = transform.position + desiredVelocity;
            //조준 됐다면 타겟을 바라봐야함
            if (aimed) {
                Vector3 look = zombies[0].position - transform.position;
                look = look.normalized;

                //비활성화라면 활성화
                if(!aimMarker.gameObject.activeSelf) {
                    aimMarker.gameObject.SetActive(true);
                }
                //조준된 좀비에 에임 활성화
                aimMarker.SetValue(zombies[0], ZombieKinds.WeakZombie);
                //Debug.Log("look!");
                //가상패드 인식이 없을때 그냥 서서 총쏘는 애니메이션
                if(horizon  == 0 && vertical == 0) {
                    thirdPersonCharacter.OnlyTurn(look, false, false);
                    //Debug.Log("정지");
                    return;
                } else {//가상패드 인식이 있을때는 걸어다니며 슈팅a
                    //애니메이션 움직임만 담당 [회전 포함]
                    thirdPersonCharacter.Move(look, false, false);
                    //Debug.Log("x = " + look.x +"  z = " + look.z);
                    //navMeshAgent.speed = 4;
                }
            } else {
                //애니메이션 움직임만 담당 [회전 포함]
                thirdPersonCharacter.Move(desiredVelocity, false, false);
                //navMeshAgent.speed = 5;
            }
        }

        void LookAtTarget() 
        {
            //Debug.Log(zombies.Count);
            if(zombies.Count > 0) {
                animator.SetBool("Aimed", true);
                aimed = true;
            } else {
                animator.SetBool("Aimed", false);
                aimed = false;

                //에임 비활성화 시키고 나에게로 가져오기
                aimMarker.SetValue(transform, ZombieKinds.WeakZombie);
                aimMarker.gameObject.SetActive(false);
            }
        }


        /*void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 200, 150), "사격"))
            {
                //m_projectileActor.Fire();
                m_projectileActor.Switch(1);
            }

            //if (GUI.Button(new Rect(0, 150, 200, 150), "중지"))
            //{

            //}
        }*/
    }
}
