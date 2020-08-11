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


        

        /// <summary>
        /// 생성 요청 및 생성된 아이템에 필요한 컨퍼넌트 추가 및 세 
        /// </summary>
        /// <param name="index"></param>
        public void Requester(int index)
        {
            //아이템 생성 -> ItemController 생성.
            Item = ItemFactory.Make(index);

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

            //아이템 컨트로러에 거리체크 세팅
            itemController.DistanceCheck = distanceCheck;

            //반응거리 넣기
            distanceCheck.MixDistance = 2f;
        }


        #region [-TestCode]
        void Update()
        {
            //아이템 요
            if (Input.GetKeyDown(KeyCode.G))
            {
                Requester(0);
            }
        }
        #endregion
    }
}
