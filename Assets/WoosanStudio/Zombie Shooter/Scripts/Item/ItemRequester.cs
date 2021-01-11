using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 아이템 팩토리에서 아이템을 요청 및 셋업 시켜줌
    /// </summary>
    public class ItemRequester : MonoBehaviour
    {
        //싱글톤 패턴
        //static public ItemRequester Instance;

        [Header("[아이템 팩토리]")]
        public ItemFactory ItemFactory;

        [Header("[연출시 이동 목표 타겟]")]
        public GameObject Target;

        [Header("[Item 자동 생성 (Auto->Awake())]")]
        public GameObject Item;

        [Header("[타겟 UI 세팅 ]")]
        public RectTransform TargetUI;

        [Header("[UI 연출용 에니메이션 ]")]
        public PlayAnimation PlayAnimation;

        [Header("[Ray에 맞은 포지션 타겟 -> [눈으로 보기위한 테스트용]")]
        public GameObject RayHitTarget;

        //캐시
        private GameObject player;

        //private void Awake()
        //{
        //    Instance = this;
        //}

        /// <summary>
        /// 외부에서 생성요청 
        /// </summary>
        public void Requester(Vector3 targetPosition)
        {
            Debug.Log("!!!!!=======> Requester");
            Make(Calculate(), targetPosition);
        }

        /// <summary>
        /// 어떤 아이템을 생성 할지 말지 확율데이터에 의해 계산한 값을 리턴
        /// </summary>
        int Calculate()
        {
            return 0;
        }

        /// <summary>
        /// 코인 아이템 만들기
        /// </summary>
        /// <param name="coinValue"></param>
        /// <param name="index"></param>
        private void MakeCoin(int coinValue , int index)
        {

        }


        /// <summary>
        /// 생성 요청 및 생성된 아이템에 필요한 컨퍼넌트 추가 및 세 
        /// </summary>
        /// <param name="index"></param>
        private void Make(int index,Vector3 targetPosition)
        {
            //아이템 생성 -> ItemController 생성.
            Item = ItemFactory.Make(index);

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

            //타겟 UI 세팅
            moveToUITarget.TargetUI = TargetUI;

            //Ray Hit 타겟 세팅
            moveToUITarget.RayHitTarget = RayHitTarget;

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
        //void Update()
        //{
        //    //아이템 요
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        Make(0,Vector3.zero);
        //    }
        //}
        #endregion
    }
}
