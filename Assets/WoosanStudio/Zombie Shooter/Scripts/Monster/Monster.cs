﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using WoosanStudio.Extension;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터의 시작점이나 문제가 있음
    /// 모든 플레이어 공용으로 사용하려 하였으나 정의가 재대로 안돼서 일단
    /// 몬스터용으로만 사용
    /// </summary>
    public class Monster : MonoBehaviour , IHaveHit , ICanDestory , ISpawnHandler , IMonsterSettings
    {
        //캐릭터의 네비메쉬 관련 이동 및 정지거리 등의 셋업 값.
        //[SerializeField] public CharacterSettings characterSettings;
        //몬스터의 네비메쉬 및 공격력 이동 관련등의 세팅 값.

        [Header("[(Auto)몬스터 팩토리에서 자동 세팅]")]
        [SerializeField]
        private MonsterSettings monsterSettings;
        public MonsterSettings MonsterSettings { get => monsterSettings; set => monsterSettings = value; }

        [Header("[(Auto)]")]
        public Transform target;

        //캐릭터 조증을 위한 유한상태기게
        private IFiniteStateMachine FSM;

        //죽었는지 살았는지 확인용
        public bool isDead = false;

        //데이지 연출용
        IBlink blink;

        //데미지 UI 텍스트 연결용 인터페이스
        IConnect connect;

        //체력바 UI 가져오기
        IHaveHealth haveHealth;

        //분노 게이지 컨트롤러
        private UI.AngerGaugePresenter angerGaugePresenter;

        private UnityEvent spawnEvent = new UnityEvent();
        public UnityEvent SpawnEvent => spawnEvent;

        //타겟 찾기
        private WaitForSeconds WFS = new WaitForSeconds(0.2f);

        private Coroutine findTargetCoroutine;

        private IEnumerator waitThenCallback(float time, System.Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }

        void Awake()
        {
            //분노 게이지 컨트롤러
            angerGaugePresenter = GameObject.FindObjectOfType<UI.AngerGaugePresenter>();

            //체력바 가져오기
            haveHealth = this.GetComponent(typeof(IHaveHealth)) as IHaveHealth;
        }

        /// <summary>
        /// 플레이어 타겟 찾기
        /// </summary>
        /// <returns></returns>
        IEnumerator FindTargetCoroutine(string tag)
        {
            //임시 타겟
            GameObject findTarget;
            
            while(true)
            {
                //0.25초 단위로 타겟 찾기.
                findTarget = GameObject.FindGameObjectWithTag(tag);

                //태그를 못찾았으면 타겟 및 목표 null
                if(findTarget == null)
                {
                    //Debug.Log("타겟 없음...");
                    //강제로 FSM 타겟에 null 넣음
                    this.FSM.GetCharacterDrivingModule().Destination = null;
                    this.target = null;
                }
                else //태그를 찾았으면 타겟 및 목표 재설정
                {
                    //Debug.Log("타겟 찾음!!!");
                    this.target = findTarget.transform;
                    //FSM 새팅이 되어야만 설정
                    if(FSM != null)
                    {
                        this.FSM.GetCharacterDrivingModule().Destination = this.target;
                    }
                }

                yield return WFS;
            }
        }

        [System.Obsolete]
        private void Start()
        {
            //생성시 플레이어 찾음
            //target = FindRandomTarget("Player");
            //&플레이어 찾는 부분 코루틴으로 대체
            findTargetCoroutine = StartCoroutine(FindTargetCoroutine("Player"));

            if (target == null)
            {
                Debug.Log("[error] Player Tag is null!! ");
                return;
            }

            if (monsterSettings == null) { Debug.Log("monsterSettings null "); }
            

            //에니메이션에 이벤트 발생 부분을 가져오기
            AnimationEvent animationEvent = GetComponentInChildren<AnimationEvent>();

            //FSM 세팅 생성 [MonsterFSM 하나로 통합할지 말지 결정 해야함]
            //*일단 통합
            FSM = new ZombieFSM();

            //유한상태기계 세팅
            //character는 순수 하게 FSM의 Tick호출만 할 뿐 모든 작업은 FSM 에서 진행한다.
            //*몬스터 추가시마다 ID 에 맞는 세팅 추가되어야 함을 잊지 말자
            switch (monsterSettings.MonsterId)
            {
                case MonsterSettings.MonsterID.WeakZombie:
                    //생성은 클래스로 하지만 인수는 인터페이스를 받는다.
                    FSM.SetFSM(
                        target,//어떤 타겟을 목표로 움직이는 세팅
                        new AiInput(), //입력부분 생성
                        new WalkZombieDrivingModule(GetComponent<UnityEngine.AI.NavMeshAgent>(), transform, target, monsterSettings) as ICharacterDrivingModule,//움직임부분 생성
                        new ZombieAnimatorModule(GetComponentInChildren<Animator>()) as ICharacterAnimatorModule,// 에니메이션부분 생성
                        new MeleeAttackModule(monsterSettings, target.GetComponent<IHaveHit>(), target.GetComponent<IHaveHealth>(), ref animationEvent.AttackEndEvent)//공격 모듈 생성.
                    );
                    break;
                case MonsterSettings.MonsterID.ThrowZombie:
                    //프로젝타일 런터를 생성할 트렌스폼 가져오가
                    //* 해당 계층 구조로 미리 만들어져 있어야 정상 작동한다.
                    //* 현재 트렌스 폼의 자식-> 자식 에서 만듬 이때 Regdoll 이면 안되고
                    //* Navi 오브젝트의 하위로 들어 가야 함.
                    Transform projectileLauncherTransform = MakeTransformHierarchyForProjectileLauncher(transform.GetChild(0).GetChild(0));

                    //생성은 클래스로 하지만 인수는 인터페이스를 받는다.
                    FSM.SetFSM(
                        target,//어떤 타겟을 목표로 움직이는 세팅
                        new AiInput(), //입력부분 생성
                        new ThrowZombieDrivingModule(GetComponent<UnityEngine.AI.NavMeshAgent>(), transform, target, monsterSettings) as ICharacterDrivingModule,//움직임부분 생성
                        new ZombieAnimatorModule(GetComponentInChildren<Animator>()) as ICharacterAnimatorModule,// 에니메이션부분 생성
                        new ThrowAttackModule(monsterSettings, target.GetComponent<IHaveHit>(), target.GetComponent<IHaveHealth>(), projectileLauncherTransform ,ref animationEvent.AttackEndEvent)//공격 모듈 생성.
                    );

                    break;
                case MonsterSettings.MonsterID.RunnerZombie:
                    //생성은 클래스로 하지만 인수는 인터페이스를 받는다.
                    FSM.SetFSM(
                        target,//어떤 타겟을 목표로 움직이는 세팅
                        new AiInput(), //입력부분 생성
                        new WalkZombieDrivingModule(GetComponent<UnityEngine.AI.NavMeshAgent>(), transform, target, monsterSettings) as ICharacterDrivingModule,//움직임부분 생성
                        new ZombieAnimatorModule(GetComponentInChildren<Animator>()) as ICharacterAnimatorModule,// 에니메이션부분 생성
                        new MeleeAttackModule(monsterSettings, target.GetComponent<IHaveHit>(), target.GetComponent<IHaveHealth>(), ref animationEvent.AttackEndEvent)//공격 모듈 생성.
                    );
                    break;
                case MonsterSettings.MonsterID.Jeff:   //*네임드 이름 마다 설정 필요.
                    //생성은 클래스로 하지만 인수는 인터페이스를 받는다.
                    FSM.SetFSM(
                        target,//어떤 타겟을 목표로 움직이는 세팅
                        new AiInput(), //입력부분 생성
                        new WalkZombieDrivingModule(GetComponent<UnityEngine.AI.NavMeshAgent>(), transform, target, monsterSettings) as ICharacterDrivingModule,//움직임부분 생성
                        new ZombieAnimatorModule(GetComponentInChildren<Animator>()) as ICharacterAnimatorModule,// 에니메이션부분 생성
                        new MeleeAttackModule(monsterSettings, target.GetComponent<IHaveHit>(), target.GetComponent<IHaveHealth>(), ref animationEvent.AttackEndEvent)//공격 모듈 생성.
                    );
                    break;
            }

            //데미지 연출용 블링크
            blink = transform.GetComponentInChildren<IBlink>();

            //데미지 UI 텍스트 연결용 인터페이스
            connect = GetComponent<IConnect>();

            //스폰됬음을 알림
            spawnEvent.Invoke();
        }

        /// <summary>
        /// 프로젝트 런처 생성시 필요한 GameObject 계층 구조 만들기
        /// </summary>
        /// <param name="parent"></param>
        Transform MakeTransformHierarchyForProjectileLauncher(Transform parent)
        {
            GameObject clone = new GameObject("ProjectileLauncher");
            clone.transform.parent = parent;
            //높이를 1정도 높혀서 구체가 날아갈수 있도록 함.
            clone.transform.Reset(new Vector3(0,1,0));

            GameObject childClone = new GameObject("Locator");
            childClone.transform.parent = clone.transform;
            childClone.transform.Reset();

            childClone = new GameObject("MuzzleLocator");
            childClone.transform.parent = clone.transform;
            childClone.transform.Reset();

            childClone = new GameObject("ShellLocator");
            childClone.transform.parent = clone.transform;
            childClone.transform.Reset();

            return clone.transform;
        }

        private void Update()
        {
            if (isDead) return;

            if (target == null)
            {
                Debug.Log("[Player Tag is null] ");

                return;
            } else
            {
                //타겟이 사라지고 새로운 타갯이 나타나면 타겟 재설정
                if (this.FSM.GetCharacterDrivingModule().Destination == null) {
                    //Debug.Log("새로운 타겟으로 변경 완료");
                    this.FSM.GetCharacterDrivingModule().Destination = this.target;
                }
            }

            FSM.Tick();
        }


        /// <summary>
        /// 가장 가까운 거리의 타겟을 찾는다.
        /// </summary>
        /// <returns></returns>
        private Transform FindNearestTarget(string findTag)
        {
            Transform nearestTarget = null;
            List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag(findTag));

            List<float> distances = new List<float>();

            targets.ForEach(value => distances.Add(Vector3.Distance(value.transform.position, transform.position)));

            nearestTarget = targets[distances.IndexOf(distances.Min())].transform;

            return nearestTarget;
        }

        /// <summary>
        /// 랜덤 타겟을 찾는다.
        /// </summary>
        /// <returns></returns>
        private Transform FindRandomTarget(string findTag)
        {
            Transform nearestTarget = null;
            List<GameObject> targets = new List<GameObject>(GameObject.FindGameObjectsWithTag(findTag));

            nearestTarget = targets[Random.Range(0,targets.Count)].transform;

            return nearestTarget;
        }

        /// <summary>
        /// 데미지 받았을때 연출
        /// </summary>
        public void Hit()
        {
            //Debug.Log("hi");
            //빨갛게 깜빡임.
            if (blink != null && blink.myGameObject.activeSelf) { blink.Blink(); }

            //공습 게이지 채움
            float angerValue = DamageCalculator.Instance.GetAirStrikeRechargeValue(GlobalDataController.AirStrikeRechargingValue);
            //분노 값 추가
            angerGaugePresenter.AddProgressValue(angerValue);
        }

        /// <summary>
        /// 글로벌 데이미 받음 
        /// </summary>
        public void HitByGlobalDamage()
        {
            //Debug.Log(this.gameObject.name + " = HitByGlobalDamage()");

            //계산된 데미지 얻기
            float damage = DamageCalculator.Instance.GetAirStrikeDamage(monsterSettings,GlobalDataController.AirStrikeDamge);
            //데미지 넣기
            haveHealth.DamagedEvent.Invoke(Mathf.RoundToInt(damage), Vector3.zero, "default");
        }

        /// <summary>
        /// 몬스터 죽음
        /// *죽자마자 DoDie.cs에 의해서 호출됨.
        /// </summary>
        public void Die()
        {
            this.isDead = true;

            //몬스터 메니저 등록에서 제거
            MonsterList.Instance.Items.Remove(this.transform);
        }

        /// <summary>
        /// 자동 삭제
        /// </summary>
        /// <param name="deley">해당 시간만큼 딜레이</param>
        public void Destory(float deley)
        {
            StartCoroutine(waitThenCallback(deley, () => {
                //데미지 UI 텍스트 연결 해제
                connect.Disconnect();

                //리얼 삭제
                Object.Destroy(this.gameObject);
            }));
        }     
    }
}
