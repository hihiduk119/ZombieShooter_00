using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using WoosanStudio.Extension;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터의 시작점이나 문제가 있음
    /// 모든 플레이어 공용으로 사용하려 하였으나 정의가 재대로 안돼서 일단
    /// 몬스터용으로만 사용
    /// </summary>
    public class Monster : MonoBehaviour , IHaveHit ,  ICanDestory 
    {
        //캐릭터의 네비메쉬 관련 이동 및 정지거리 등의 셋업 값.
        //[SerializeField] public CharacterSettings characterSettings;
        //몬스터의 네비메쉬 및 공격력 이동 관련등의 세팅 값.
        [SerializeField] public MonsterSettings monsterSettings;

        public Transform target;

        //캐릭터 조증을 위한 유한상태기게
        private IFiniteStateMachine FSM;

        //죽었는지 살았는지 확인용
        public bool isDead = false;

        //데이지 연출용
        IBlink blink;

        //데미지 UI 텍스트 연결용 인터페이스
        IConnect connect;

        private IEnumerator waitThenCallback(float time, System.Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }

        [System.Obsolete]
        private void Start()
        {
            //최소거리의 바리케이트 오브젝트를 찾음
            //target = FindNearestTarget("Barrier");
            //랜덤한 바리케이트 오브젝트 찾음
            target = FindRandomTarget("Barrier");

            if (target == null)
            {
                Debug.Log("[error] Player Tag is null!! ");
                return;
            }

            if(monsterSettings == null)
            Debug.Log("monsterSettings null ");

            //유한상태기계 세팅
            //character는 순수 하게 FSM의 Tick호출만 할 뿐 모든 작업은 FSM 에서 진행한다.
            switch (monsterSettings.MonsterId)
            {
                case MonsterSettings.MonsterID.WeakZombie:
                    //FSM 세팅 생성 [MonsterFSM 하나로 통합할지 말지 결정 해야함]
                    FSM = new WeakZombieFSM();

                    FSM.SetFSM(
                        target,//어떤 타겟을 목표로 움직이는 세팅
                        new AiInput(), //입력부분 생성
                        new WalkDrivingModule(GetComponent<UnityEngine.AI.NavMeshAgent>(), transform, target, monsterSettings) as ICharacterDrivingModule,//움직임부분 생성
                        new ZombieAnimatorModule(GetComponentInChildren<Animator>()) as ICharacterAnimatorModule,// 에니메이션부분 생성
                        new MeleeAttackModule(monsterSettings, target.GetComponent<IHaveHit>(), target.GetComponent<IHaveHealth>())//공격 모듈 생성.
                    );
                    break;
                case MonsterSettings.MonsterID.ThrowZombie:

                    //프로젝타일 런터를 생성할 트렌스폼 가져오가
                    //* 해당 계층 구조로 미리 만들어져 있어야 정상 작동한다.
                    //* 현재 트렌스 폼의 자식-> 자식 에서 만듬 이때 Regdoll 이면 안되고
                    //* Navi 오브젝트의 하위로 들어 가야 함.
                    Transform projectileLauncherTransform = MakeTransformHierarchyForProjectileLauncher(transform.GetChild(0).GetChild(0));

                    //FSM 세팅 생성 [MonsterFSM 하나로 통합할지 말지 결정 해야함]
                    FSM = new WeakZombieFSM();

                    FSM.SetFSM(
                        target,//어떤 타겟을 목표로 움직이는 세팅
                        new AiInput(), //입력부분 생성
                        new WalkDrivingModule(GetComponent<UnityEngine.AI.NavMeshAgent>(), transform, target, monsterSettings) as ICharacterDrivingModule,//움직임부분 생성
                        new ZombieAnimatorModule(GetComponentInChildren<Animator>()) as ICharacterAnimatorModule,// 에니메이션부분 생성
                        new ThrowAttackModule(monsterSettings, target.GetComponent<IHaveHit>(), target.GetComponent<IHaveHealth>(), projectileLauncherTransform)//공격 모듈 생성.
                    );

                    break;
                case MonsterSettings.MonsterID.RunnerZombie:
                    //FSM 세팅 생성 [MonsterFSM 하나로 통합할지 말지 결정 해야함]
                    FSM = new WeakZombieFSM();

                    FSM.SetFSM(
                        target,//어떤 타겟을 목표로 움직이는 세팅
                        new AiInput(), //입력부분 생성
                        new WalkDrivingModule(GetComponent<UnityEngine.AI.NavMeshAgent>(), transform, target, monsterSettings) as ICharacterDrivingModule,//움직임부분 생성
                        new ZombieAnimatorModule(GetComponentInChildren<Animator>()) as ICharacterAnimatorModule,// 에니메이션부분 생성
                        new MeleeAttackModule(monsterSettings, target.GetComponent<IHaveHit>(), target.GetComponent<IHaveHealth>())//공격 모듈 생성.
                    );
                    break;
            }

            //데미지 연출용 블링크
            blink = transform.GetComponentInChildren<IBlink>();

            //데미지 UI 텍스트 연결용 인터페이스
            connect = GetComponent<IConnect>();
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
        }

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
