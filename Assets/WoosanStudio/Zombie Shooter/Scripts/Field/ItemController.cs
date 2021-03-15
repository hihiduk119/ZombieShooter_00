using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.Field
{
    /// <summary>
    /// 아이템의 컨트롤
    /// </summary>
    public class ItemController : MonoBehaviour
    {
        [Header("모델 [(Auto->ItemFactory.Make())]")]
        public GameObject Model;

        [Header("메인 이펙트 [(Auto->ItemFactory.Make())]")]
        public GameObject MainEffect;

        [Header("서브 이펙트 [(Auto->ItemFactory.Make())]")]
        public GameObject SubEffect;

        [Header("거리 체크기 [(Auto->ItemFactory.Make())]")]
        public DistanceCheck DistanceCheck;

        [Header("해당 타겟으로 이동 [(Auto->ItemFactory.Make())]")]
        public MoveToUITarget MoveToUITarget;

        [Header("스폰용 트렌스폼 [(Auto->ItemFactory.Make())]")]
        //삭제시 ItemManager.cs에 제거 알림
        public Transform spawmTransform;

        [Header("HUD [(Auto->ItemFactory.Make())]")]
        public SickscoreGames.HUDNavigationSystem.HUDNavigationElement HUDNaviElement;

        [Header("[고유 값]")]
        public int Value;

        [Header("[아이템 타입]")]
        public ItemSetting.FieldItem Type;

        //삭제 이벤트
        //0 = 스폰 위치 트랜스폼, 1 = 자기 자신
        public class DestoryEvent : UnityEvent<Transform,Transform> { }
        public DestoryEvent ItemDestoryEvent = new DestoryEvent();
        //삭제 이벤트 내자신 보내기
        //public DestoryEvent ItemDestoryEvent2 = new DestoryEvent();

        /// <summary>
        /// 비활성화 시킴
        /// </summary>
        public void Deactivate()
        {
            Model.SetActive(false);
            MainEffect?.SetActive(false);
            SubEffect?.SetActive(false);
        }

        /// <summary>
        /// 활성화 이때 위치도 조정
        /// </summary>
        /// <param name="position">활성 위치 좌표</param>
        public void Activate(Vector3 position)
        {
            transform.position = position;

            Model.SetActive(true);
            MainEffect?.SetActive(true);
            SubEffect?.SetActive(true);
        }

        /// <summary>
        /// 아이템 획득 연출
        /// </summary>
        public void GetItem()
        {
            //메인 이펙트 비활성
            MainEffect?.SetActive(false);
            //서브 이펙트 비활성
            SubEffect?.SetActive(false);
            //이동 시작
            MoveToUITarget.Move();
        }

        /// <summary>
        /// 아이템 획득 완료
        /// </summary>
        public void GetItemComplete()
        {
            //모델 비활성화
            Model.SetActive(false);

            //아이템 삭제 이벤트 발생->스폰했던 트렌스폼 돌려줌
            ItemDestoryEvent.Invoke(spawmTransform, this.transform);

            //아이템 삭제 이벤트 발생 -> 내자신을 보냄
            //ItemDestoryEvent2.Invoke(this.transform);

            //오브젝트 풀 쓸지 말지 고민중..
            Destroy(this.gameObject);
        }


        /// <summary>
        /// HUD 활성 & 비활성
        /// </summary>
        /// <param name="value"></param>
        private void SetActiveHUD(bool value)
        {
            HUDNaviElement.enabled = value;
        }

        /// <summary>
        /// HUD 비활성화
        /// </summary>
        public void DeactiveHUD()
        {
            this.SetActiveHUD(false);
        }

        /// <summary>
        /// 획득한 값 글로벌 데이터에 세팅
        /// </summary>
        public void GaineValue()
        {
            //해당 타입의 값 추가
            switch (this.Type)
            {
                case ItemSetting.FieldItem.Coin:
                    //코인 획득
                    GlobalDataController.StageGainedCoin += this.Value;

                    //코인 획득 사운드 플레이
                    DarkTonic.MasterAudio.MasterAudio.FireCustomEvent("SFX_GetCoin", this.transform);
                    break;
                case ItemSetting.FieldItem.Exp:
                    //경험치 획득
                    GlobalDataController.StageGainedXP += this.Value;

                    //경험치 획득 사운드 플레이
                    DarkTonic.MasterAudio.MasterAudio.FireCustomEvent("SFX_GetXP", this.transform);
                    break;
            }
        }

        //#region [-TestCode]
        //void Update()
        //{
        //    //아이템 획득 테스트
        //    if (Input.GetKeyDown(KeyCode.H))
        //    {
        //        GetItem();
        //    }
        //}
        //#endregion
    }
}
