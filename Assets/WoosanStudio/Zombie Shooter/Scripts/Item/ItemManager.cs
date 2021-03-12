using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 아이템 팩토리에서 아이템을 요청 및 셋업 시켜줌
    /// </summary>
    public class ItemManager : MonoBehaviour
    {
        //싱글톤 패턴
        //static public ItemRequester Instance;

        [Header("[아이템 팩토리]")]
        public ItemFactory ItemFactory;

        [Header("[연출시 이동할 목표 타겟]")]
        public GameObject Target;

        [Header("[Item 자동 생성 (Auto->Awake())]")]
        public GameObject Item;

        //[Header("[타겟 UI 세팅 ]")]
        //public RectTransform TargetUI;

        //[Header("[아이템 획득시 움직일 타겟 세팅]")]
        //public Transform Target;

        [Header("[UI 연출용 에니메이션 ]")]
        public PlayAnimation PlayAnimation;

        //[Header("[Ray에 맞은 포지션 타겟 -> [눈으로 보기위한 테스트용]")]
        //public GameObject RayHitTarget;

        [Header("스폰 위치")]
        public SpawnPositionController ItemSpawnPositionController;

        [Header("아이템 스폰위치 중복을 막기위한 저장소")]
        //아이템 획득시 해당 아이템 위치 제거되어야 함
        public List<Transform> spawnedItemPositionList = new List<Transform>();

        //캐쉬용
        Coroutine schedulerForMakingCoinCoroutine;

        //맵에 최대 아이템 최대 생성 제한
        //*글로벌 데이터에서 가져올 필요가 있음
        int maxItemCount = 2;

        //캐시
        private GameObject player;

        /// <summary>
        /// 외부에서 생성요청 
        /// </summary>
        public void Requester(Transform spawnTransform)
        {
            Debug.Log("!!!!!=======> Requester");
            MakeItem(Calculate(), spawnTransform);
        }

        /// <summary>
        /// 어떤 아이템을 생성 할지 말지 확율데이터에 의해 계산한 값을 리턴
        /// </summary>
        int Calculate()
        {
            return 0;
        }

        /// <summary>
        /// 코인 메이킹 스케줄러 실행
        /// </summary>
        void RunSchedulerForMakingCoin()
        {
            //코인 메이킹 스케줄러 정지
            StopSchedulerForMakingCoin();
            //코루틴 시작
            schedulerForMakingCoinCoroutine = StartCoroutine(SchedulerForMakingCoinCoroutine());
        }

        /// <summary>
        /// 코인 메이킹 스케줄러 정지
        /// </summary>
        void StopSchedulerForMakingCoin()
        {
            if (schedulerForMakingCoinCoroutine != null)
            {
                StopCoroutine(schedulerForMakingCoinCoroutine);
            }
        }

        /// <summary>
        /// 코인 메이킹 스케줄러 구현부
        /// </summary>
        /// <returns></returns>
        IEnumerator SchedulerForMakingCoinCoroutine()
        {
            while(true)
            {
                //코인 만들기
                MakeCoin();
                //10-15초 사이 랜덤
                yield return new WaitForSeconds(Random.Range(2,4));
            }
        }

        /// <summary>
        /// 아이템 삭제시 호출되는 이벤트 연결
        /// *ItemController.GetItemComplete()에서 호출
        /// </summary>
        /// <param name="spawnTransform"></param>
        public void ItemDestoryEventHandler(Transform spawnTransform)
        {
            int removeIndex = spawnedItemPositionList.FindIndex(value => value.Equals(spawnTransform));
            spawnedItemPositionList.RemoveAt(removeIndex);
             
        }

        /// <summary>
        /// 코인 아이템 만들기
        /// </summary>
        /// <param name="coinValue"></param>
        /// <param name="index"></param>
        private void MakeCoin()
        {
            //스테이지 가져와야 함
            int stage = 0;
            //코인 생성 가능확인용
            bool makeAble = false;

            //해당 스테이지의 스폰 포인트 가져옴
            SpawnPoints spawnPoints = ItemSpawnPositionController.GetSpawnPoints(stage);
            //최대 아이템 스폰 
            int maxSpawn = spawnPoints.Points.Count;
            Transform spawnTransform = null;


            Debug.Log("생성된 값 = [" + spawnedItemPositionList.Count + "] 최대 스폰피봇 = [" + maxSpawn + "] 맵 최대 스폰 제한 = ["+ maxItemCount +"]");
            //*현재 스폰된 아이템이 0보다 커야함.
            //*최대 아이템 스폰 위치갯수 보다 현재 스폰된 아이템이 작을때 수행
            //*최대 아이템 스폰 보다 현재 스폰된 아이템이 작을때 수행
            if (0 < spawnedItemPositionList.Count && spawnedItemPositionList.Count < maxSpawn && spawnedItemPositionList.Count < maxItemCount)
            {
                bool loop = true;
                //중복 스폰 위치가 안나올때 까지 루프.
                while(loop)
                {
                    //새로운 스폰 장소 가져오기
                    spawnTransform = spawnPoints.GetSpawnPositionByRandom();

                    //일단 루프 정지할수 있게 만듬
                    loop = false;

                    //중복 체크용 같은게 있는지 찾기
                    for (int i = 0; i < spawnedItemPositionList.Count; i++)
                    {
                        if(spawnedItemPositionList[i].Equals(spawnTransform))
                        {
                            //같은 스폰 장소를 가져 왔다면 루프 또 돔.
                            loop = true;
                        }
                    }
                }

                //만들기 가능
                makeAble = true;
            } else if(spawnedItemPositionList.Count == 0)//현재 스폰된 아이템이 0이라면
            {
                //새로운 스폰 장소 가져오기
                spawnTransform = spawnPoints.GetSpawnPositionByRandom();

                //만들기 가능
                makeAble = true;
            }

            //코인 만들기
            if (makeAble)
            {
                //중복 체크용 리스트에 저장
                spawnedItemPositionList.Add(spawnTransform);

                //코인 생성
                MakeItem(0, spawnTransform);
                Debug.Log("아이템 생성 성공");
            } else
            {
                Debug.Log("아이템 생성 실패");
            }
        }


        /// <summary>
        /// 생성 요청 및 생성된 아이템에 필요한 컨퍼넌트 추가 및 세 
        /// </summary>
        /// <param name="index"></param>
        private void MakeItem(int index,Transform spawnTransform)
        {
            //아이템 생성 -> ItemController 생성.
            Item = ItemFactory.Make(index, spawnTransform, ItemDestoryEventHandler);

            Vector3 targetPosition = spawnTransform.position;

            //높이 0.01f으로 초기화.
            //*바닦에서 살짝 띄움 -> 그래야 바닦에 생김
            targetPosition.y = 0.01f;

            //위치 재조정
            Item.transform.position = targetPosition;

            //ItemFactory에서 생성된 컨트롤러 가져오기
            ItemController itemController = Item.GetComponent<ItemController>();

            //아이템을 활성화 시킴.
            //이때 좌표도 필요. => 몬스터 사망시 좌표가 필요하다.
            //itemController.Activate();

            //무브 투 타겟 추가
            MoveToUITarget moveToUITarget = Item.AddComponent<MoveToUITarget>();

            //UI 타겟의 이동 완료에 아이템 컨트롤러 아이템 획득 완료를 연결
            moveToUITarget.MoveCompleteEvent.AddListener(itemController.GetItemComplete);
            moveToUITarget.MoveCompleteEvent.AddListener(PlayAnimation.Play);

            //아이템 컨트롤러에 무브 투 타겟 세팅
            itemController.MoveToUITarget = moveToUITarget;

            //연출시 이동할 타겟 UI 세팅
            moveToUITarget.Target = Target.transform;

            //Ray Hit 타겟 세팅
            //moveToUITarget.RayHitTarget = RayHitTarget;

            //거리 체크 스크립트 추가
            DistanceCheck distanceCheck = Item.AddComponent<DistanceCheck>();

            //캐시로 찾은 플레이어가 없을때 한번 찾음
            if (player == null)
            {
                player = GameObject.FindObjectOfType<PlayerController>().gameObject;
            }            

            //아이템 컨트로러에 거리체크 세팅
            itemController.DistanceCheck = distanceCheck;
            //찾을 타겟을 플레이어로 설정
            itemController.DistanceCheck.Reset(player);
            //타겟을 사용하게 온
            itemController.DistanceCheck.UseTarget = true;
            //반응거리 넣기
            distanceCheck.MixDistance = 1.5f;
            //거리 체커의 근접이벤트 발생과  아이템 컨트롤러의 아이템 획득 연결.
            itemController.DistanceCheck.CloseEvent.AddListener(itemController.GetItem);
        }

        #region [-TestCode]
        void Update()
        {
            //코인 자동 생성기 실행
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                RunSchedulerForMakingCoin();
            }
        }
        #endregion
    }
}
