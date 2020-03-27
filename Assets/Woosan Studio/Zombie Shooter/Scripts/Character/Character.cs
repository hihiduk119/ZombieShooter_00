using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터의 시작점이나 문제가 있음
    /// 모든 플레이어 공용으로 사용하려 하였으나 정의가 재대로 안돼서 일단
    /// 몬스터용으로만 사용
    /// </summary>
    public class Character : MonoBehaviour , IHaveHit ,  ICanDestory
    {
        //캐릭터의 네비메쉬 관련 이동 및 정지거리 등의 셋업 값.
        [SerializeField] public CharacterSettings characterSettings;
        public Transform target;

        //캐릭터 조증을 위한 유한상태기게
        private IFiniteStateMachine FSM;

        //죽었는지 살았는지 확인용
        public bool isDead = false;

        //데이지 연출용
        IBlink blink;

        private IEnumerator waitThenCallback(float time, System.Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }

        private void Awake()
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

            //FSM 세팅 생성
            FSM = new MonsterFSM();

            //유한상태기계 세팅
            //character는 순수 하게 FSM의 Tick호출만 할 뿐 모든 작업은 FSM 에서 진행한다.
            FSM.SetFSM(
                target,//어떤 타겟을 목표로 움직이는 세팅
                new AiInput(), //입력부분 생성
                new AiDrivingModule(GetComponent<UnityEngine.AI.NavMeshAgent>(), transform, target, characterSettings) as ICharacterDrivingModule,//움직임부분 생성
                new ZombieAnimatorModule(GetComponentInChildren<Animator>()) as ICharacterAnimatorModule);// 에니메이션부분 생성

            //데미지 연출용 블링크
            blink = transform.GetComponentInChildren<IBlink>();
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

        /// <summary>
        /// 자동 삭제
        /// </summary>
        /// <param name="deley">해당 시간만큼 딜레이</param>
        public void Destory(float deley)
        {
            StartCoroutine(waitThenCallback(deley, () => { Object.Destroy(this.gameObject); }));
        }     
    }
}
