using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{

    public class DoDieForPlayer : MonoBehaviour
    {
        //컨트롤 모델
        public GameObject ControlModel;
        //레그돌 모델
        public GameObject RagdollModel;
        
        public CopyComponets CopyComponets;

        //cache
        //private RegdollController regdollController;
        //private NavMeshController navMeshController;
        //캐릭터에 죽음 체크용
        //private Monster monster;
        //체력 계산용
        private IHaveHealth haveHealth;
        //죽었을때 무중력으로 떠오르게 하기 위해
        private DoZeroGravity doZeroGravity;
        //데미지입었을때 빨간 블링크 용
        private IBlink blink;
        //죽을 오브젝트를 세팅
        private ICanDestory canDestory;
        //몬스터 세팅 가져오기
        //private IMonsterSettings monsterSettings;

        //죽음 연출용 강제 Force발생
        Boom boom;

        [Header("[죽음 이벤트 => 인터페이스로 바꿀지 말지 결정해야함]")]
        public PositionEvent OnDieEvent = new PositionEvent();

        [Header("[죽은다음 하늘로 이동할때 이벤트 발생 => 인터페이스로 바꿀지 말지 결정해야함]")]
        public PositionEvent GoHeavenEvent = new PositionEvent();

        //죽은 위치의 좌표를 위해 Vector3 포함
        public class PositionEvent : UnityEvent<Vector3> { }

        private void Awake()
        {
            //regdollController = GetComponent<RegdollController>();
            //navMeshController = GetComponent<NavMeshController>();
            //monster = GetComponent<Monster>();
            haveHealth = GetComponent<IHaveHealth>();
            doZeroGravity = GetComponent<DoZeroGravity>();
            blink = transform.GetComponentInChildren<IBlink>();
            canDestory = GetComponent<ICanDestory>();
            //monsterSettings = GetComponent<IMonsterSettings>();

            //IHaveHealth 에 체력 체크 등록.
            haveHealth.DamagedEvent.AddListener(CheckHealth);
        }

        //체력 체크용 콜백 함수
        //
        public void CheckHealth(int damage, Vector3 hit, string keyValue)
        {
            //if (haveHealth.Health <= 0) { Die(hit); }
            if (haveHealth.Health <= 0) { Die(); }
        }

        /// <summary>
        /// 죽음
        /// </summary>
        /// <param name="hit"></param>
        //public void Die(Vector3 hit)
        public void Die()
        {
            //데미지 이벤트 등록된 모든 리스너 등록해제
            //해제를 안하면 데미지 받는데로 행동 실행.
            haveHealth.DamagedEvent.RemoveAllListeners();

            //죽는 사운드 발생
            //MasterAudio.FireCustomEvent("MonsterDead", this.transform);

            //죽음 이벤트 시작
            //* 원래는 모든 체력, 블릭크초기화 몬스터 죽음호출 정지 등 모든 부분이 이벤트의
            //* 리스너로 넣어야 한다. -> 리팩토링 필요한 부분
            //OnDieEvent.Invoke(hit);

            //Debug.Log("=============" + this.gameObject.name + " 죽음 호출!!");

            //blink.Initialize();

            //monster.Die();
            //navMeshController.Stop();
            CopyComponets.Copy();
            //컨트롤 모델 비활성
            ControlModel.SetActive(false);
            //레그돌 모델 활성
            RagdollModel.SetActive(true);

            //regdollController.SetActive(true);

            //죽을때 이펙트 생성
            //if (monsterSettings.MonsterSettings.DeadEffect != null)
            //{
            //    GameObject deadEffect = Instantiate(monsterSettings.MonsterSettings.DeadEffect) as GameObject;
            //    deadEffect.transform.position = transform.position;
            //    deadEffect.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            //}

            Vector3 pos = transform.position;
            pos.x += Random.Range(-1, 3);
            pos.y += Random.Range(0, 2);
            pos.z += Random.Range(-1, 3);
            //죽음 연출용 강제 포스
            boom = new Boom(pos);
            //testDummy.transform.position = hit;

            //Invoke("GoToHeaven", 3f);
            //Debug.Log("=================>    GoToHeaven");
        }

        /// <summary>
        /// 부활
        /// </summary>
        public void Resurrection()
        {
            //컨트롤 모델 비활성
            ControlModel.SetActive(true);
            //레그돌 모델 활성
            RagdollModel.SetActive(false);
        }

        /// <summary>
        /// 하늘로 떠오르며 죽는 연출
        /// </summary>
        void GoToHeaven()
        {
            //하늘로 올름 이벤트 발생
            GoHeavenEvent.Invoke(transform.position);

            //X 값은 연출용 값으로 값이 클수록 화면 쪽으로 시체가 앞으록 움직임.
            //Y 값은 떠오르는 속도이며 값이 크면 빠르게 떠오름.
            doZeroGravity.UpForce(new Vector3(0, 7.5f, 0));

            if (canDestory != null)
            {
                //연결돈 모든 리스너 삭제
                GoHeavenEvent.RemoveAllListeners();
                canDestory.Destory(2.5f);
            }
            else
            {
                Debug.Log("ICanDestory Null!!. 해당 오브젝트를 세팅 해주세요.");
            }

            //Debug.Log("=================>    Go       ToHeaven");
        }
    }
}
