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
        //*아이템이 생성됬던 위치만 기억함
        public List<Transform> SpawnedItemPositionList = new List<Transform>();

        //[Header("생성된 모든 아이템")]
        public List<Transform> SpawnedAllItemList = new List<Transform>();

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
        //public void Requester(Transform spawnTransform)
        //{
        //    Debug.Log("!!!!!=======> Requester");
        //    MakeItem(Calculate(), spawnTransform);
        //}

        /// <summary>
        /// 어떤 아이템을 생성 할지 말지 확율데이터에 의해 계산한 값을 리턴
        /// </summary>
        //int Calculate()
        //{
        //    return 0;
        //}

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
            //스폰 포지션
            Transform spawnTransform = null;

            while (true)
            {
                //아이템 스폰 위치 가져오기
                spawnTransform = GetItemSpawnPosition();

                //스폰 위치가 있다면 아이템 생성
                if (spawnTransform != null)
                {
                    //중복 체크용 리스트에 저장
                    SpawnedItemPositionList.Add(spawnTransform);

                    //랜덤확률 발생 5:5 로 코인 or EXP 생성
                    int randValue = Random.Range(0, 10);
                    if (0 <= randValue && randValue <= 4)
                    {
                        //아이템 생성
                        MakeItem(ItemSetting.FieldItem.Coin, spawnTransform);
                    }  else
                    {
                        //아이템 생성
                        MakeItem(ItemSetting.FieldItem.Exp, spawnTransform);
                    }
                }

                //10-15초 사이 랜덤
                yield return new WaitForSeconds(Random.Range(2,4));
            }
        }

        /// <summary>
        /// 아이템 삭제시 호출되는 이벤트 연결
        /// *ItemController.GetItemComplete()에서 호출
        /// *spawnTransform -> 아이템 생성이 등록된 위치 트렌스폼 삭제
        /// *item -> 아이템 삭제
        /// </summary>
        /// <param name="spawnTransform"></param>
        public void ItemDestoryEventHandler(Transform spawnTransform, Transform item)
        {
            //스폰 아이템 포지션
            int removeIndex = SpawnedItemPositionList.FindIndex(value => value.Equals(spawnTransform));
            SpawnedItemPositionList.RemoveAt(removeIndex);

            //모든 아이템
            removeIndex = SpawnedAllItemList.FindIndex(value => value.Equals(item));
            SpawnedAllItemList.RemoveAt(removeIndex);
        }

        /// <summary>
        /// 아이템 삭제시 호출되는 이벤트 연결
        /// *ItemController.GetItemComplete()에서 호출
        /// 
        /// </summary>
        /// <param name="item"></param>
        //public void ItemDestoryEventHandler2()
        //{
            
        //}

        /// <summary>
        /// 모든 아이템 제거
        /// *라운드 새로 시작하기 위해 사용
        /// </summary>
        public void DestoryAllItem()
        {
            //스폰됬던 모든 위치 비우기
            SpawnedItemPositionList.Clear();

            //모든 아이템 삭제
            SpawnedAllItemList.ForEach(value => { Destroy(value.gameObject); });

            //아이템 리스트 초기화
            SpawnedAllItemList.Clear();
        }

        /// <summary>
        /// 코인 아이템 만들기
        /// </summary>
        /// <param name="coinValue"></param>
        /// <param name="index"></param>
        private Transform GetItemSpawnPosition()
        {
            //스테이지 가져와야 함
            int stage = 0;
            //코인 생성 가능확인용
            //bool makeAble = false;

            //해당 스테이지의 스폰 포인트 가져옴
            SpawnPoints spawnPoints = ItemSpawnPositionController.GetSpawnPoints(stage);
            //최대 아이템 스폰 
            int maxSpawn = spawnPoints.Points.Count;
            //null 초기화
            Transform spawnTransform = null;


            //Debug.Log("생성된 값 = [" + SpawnedItemPositionList.Count + "] 최대 스폰피봇 = [" + maxSpawn + "] 맵 최대 스폰 제한 = ["+ maxItemCount +"]");
            //*현재 스폰된 아이템이 0보다 커야함.
            //*최대 아이템 스폰 위치갯수 보다 현재 스폰된 아이템이 작을때 수행
            //*최대 아이템 스폰 보다 현재 스폰된 아이템이 작을때 수행
            if (0 < SpawnedItemPositionList.Count && SpawnedItemPositionList.Count < maxSpawn && SpawnedItemPositionList.Count < maxItemCount)
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
                    for (int i = 0; i < SpawnedItemPositionList.Count; i++)
                    {
                        if(SpawnedItemPositionList[i].Equals(spawnTransform))
                        {
                            //같은 스폰 장소를 가져 왔다면 루프 또 돔.
                            loop = true;
                        }
                    }
                }

                //만들기 가능
                //makeAble = true;
            } else if(SpawnedItemPositionList.Count == 0)//현재 스폰된 아이템이 0이라면
            {
                //새로운 스폰 장소 가져오기
                spawnTransform = spawnPoints.GetSpawnPositionByRandom();

                //만들기 가능
                //makeAble = true;
            }

            /*
            //코인 만들기
            if (makeAble)
            {
                //중복 체크용 리스트에 저장
                SpawnedItemPositionList.Add(spawnTransform);

                //코인 생성
                MakeItem(ItemSetting.FieldItem.Coin, spawnTransform);
                Debug.Log("아이템 생성 성공");
            } else
            {
                Debug.Log("아이템 생성 실패");
            }
            */
            return spawnTransform;
        }


        /// <summary>
        /// 생성 요청 및 생성된 아이템에 필요한 컨퍼넌트 추가 및 세 
        /// </summary>
        /// <param name="index"></param>
        private void MakeItem(ItemSetting.FieldItem type,Transform spawnTransform)
        {
            NextValueCalculator nextValueCalculator = GameObject.FindObjectOfType<NextValueCalculator>();

            int value = 0;

            //Debug.Log("[현재 라운드 = " + GlobalDataController.CurrentRound + "  플레이어 레벨 = " + GlobalDataController.PlayerLevel+"]");

            //아이템 타입별 값 가져오기
            switch (type)
            {
                case ItemSetting.FieldItem.Coin:
                    value = nextValueCalculator.GetItemCoinValue(GlobalDataController.CurrentRound,GlobalDataController.PlayerLevel,GlobalDataController.Instance.SelectAbleAllCard);
                    break;
                case ItemSetting.FieldItem.Exp:
                    value = nextValueCalculator.GetItemExpValue(GlobalDataController.CurrentRound, GlobalDataController.PlayerLevel, GlobalDataController.Instance.SelectAbleAllCard);
                    break;
                default:
                    value = 0;
                    break;
            }

            //아이템 생성 -> ItemController 생성.
            Item = ItemFactory.Make(type, spawnTransform, value, ItemDestoryEventHandler);

            //생성 아이템 리스트에도 저장
            SpawnedAllItemList.Add(Item.transform);

            Vector3 targetPosition = spawnTransform.position;

            //높이 0.01f으로 초기화.
            //*바닦에서 살짝 띄움 -> 그래야 바닦에 생김
            targetPosition.y = 0.01f;

            //위치 재조정
            Item.transform.position = targetPosition;

            //ItemFactory에서 생성된 컨트롤러 가져오기
            Field.ItemController itemController = Item.GetComponent<Field.ItemController>();

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

            //거리 체커의 근접이벤트 발생 이벤트와 아이템 컨트롤러의 아이템 획득 연결.
            itemController.DistanceCheck.CloseEvent.AddListener(itemController.GetItem);

            //거리 체커의 근접이벤트 발생 이벤트와 HUD 끝내기 등록
            itemController.DistanceCheck.CloseEvent.AddListener(itemController.DeactiveHUD);

            //거리 체커의 근접이벤트 발생 이벤트와 아이템의 값을 획득 등록
            itemController.DistanceCheck.CloseEvent.AddListener(itemController.GaineValue);
        }

        #region [-TestCode]
        void Update()
        {
            //코인 자동 생성기 실행
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("아이템 생성 콜");
                //아이템 생성
                RunSchedulerForMakingCoin();
            }

            //모든 아이템 삭제
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //아이템 생성 정지
                StopSchedulerForMakingCoin();

                Debug.Log("모든 아이템 삭제");
                DestoryAllItem();
            }
        }
        #endregion
    }
}
