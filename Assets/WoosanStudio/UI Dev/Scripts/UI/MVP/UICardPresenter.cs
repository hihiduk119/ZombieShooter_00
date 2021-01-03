using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 카드 모델 데이터를 필요에 따라 동기화 시키는 역활을 함.
    /// *MVP 모델
    /// </summary>
    public class UICardPresenter : MonoBehaviour
    {
        [Header("[MVP Model]")]
        public UICardModel Model;

        /*IEnumerator Start()
        {
            //0.1f초 대기 이유??
            //일단 에러 발생은 확인
            yield return new WaitForSeconds(0.1f);
            Initialize();
        }*/

        private void Awake()
        {
            Model.Load();
        }

        /// <summary>
        /// 최초 사용시 기존 데이터 모두 로드
        /// </summary>
        /*public void Initialize()
        {
            //싱크로 호출하여 데이터 동기화
            //Model.Synchronization();
        }*/

        /// <summary>
        /// 카드 업그레이드 시작시 카드 데이터 반영 및 싱크 마추기
        /// </summary>
        public void CardUpgradeStart(CardSetting cardSetting)
        {
            switch (cardSetting.WhoCallToUpgrade)
            {
                case CardSetting.CallToUpgrade.Coin:
                    //업그레이드 중인 상태로 카드 등록시킴
                    UIGlobalMesssageQueueVewModel.UpgradingEvent.Invoke(cardSetting);

                    //아직 완료 통지 알림 안됨
                    cardSetting.ShownUpgradeComplate = false;

                    //시간 데이터 업데이트
                    //세팅할 업글 시간 가져오기
                    int seconds = NextValueCalculator.GetUpgradeTimeByLevel(cardSetting.MaxLevel, cardSetting.Level);
                    //시간 업데이트
                    cardSetting.UpgradeTimeset = new Timeset(seconds);
                    //현재 업글 중으로 변경
                    cardSetting.IsUpgrading = true;

                    //코인의 경우 즉시 화면 갱신 필요.
                    CardUpgradeComplate(cardSetting);
                    break;
                case CardSetting.CallToUpgrade.Gem:
                    //아직 완료 통지 알림 안됨
                    cardSetting.ShownUpgradeComplate = false;
                    //업그레이드 완료인 상태로 카드 등록시킴
                    UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
                    break;
                case CardSetting.CallToUpgrade.Gamble:
                    //아직 완료 통지 알림 안됨
                    cardSetting.ShownUpgradeComplate = false;
                    //업그레이드 완료인 상태로 카드 등록시킴
                    UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 카드 업그레이드 취소
        /// </summary>
        public void CancelCardUpgrade(CardSetting cardSetting)
        {

            //강제로 업그레이드 중인 리스트에서 제거
            UIGlobalMesssageQueueVewModel queue = GameObject.FindObjectOfType<UIGlobalMesssageQueueVewModel>();

            //카드 제거
            if(!queue.UpgradingCardList.Remove(cardSetting))
            {
                Debug.Log("취소에 의해 카드 제거 실패");
            }

            //*데이터 모두 변경
            CardSetting.CancelToUpgrade(cardSetting);

            //취소 호출은 2군대 호출 가능 및 3군대 업데이트 필요.
            //[1].모든 카드 인포창
            //[2]. 카드 업그레이드 창
            //*[3]. 카드 업그레이드 정보 슬롯 3개 있는 창.

            //[1]모든 카드 인포창
            UICardInfoPopupPresenter cardInfoPopupPresenter = GameObject.FindObjectOfType<UICardInfoPopupPresenter>();
            if (cardInfoPopupPresenter != null)
            {
                //선택된 카드 아이템 강제 선택 이벤트 발생.
                cardInfoPopupPresenter.CardItemPresenter.Selected();
            }

            //[2]카드 연구 정보 팝업 가져오기
            UICardResearchInfoPopupPresenter cardResearchInfoPopupPresenter = GameObject.FindObjectOfType<UICardResearchInfoPopupPresenter>();
            if (cardResearchInfoPopupPresenter != null)
            {
                //[2]연구 화면 갱신
                cardResearchInfoPopupPresenter.UpdateCardInfo();
            }
        }

        /// <summary>
        /// 완료 통지가 끝나고 업그레이드 데이터 실제 반영
        /// </summary>
        public void CardUpgradeComplate(CardSetting cardSetting)
        {
            //카드 연구 정보 팝업 가져오기
            UICardResearchInfoPopupPresenter cardResearchInfoPopupPresenter = GameObject.FindObjectOfType<UICardResearchInfoPopupPresenter>();

            //연구 화면 갱신
            cardResearchInfoPopupPresenter.UpdateCardInfo(); 
        }


        /// <summary>
        /// Presenter에서 업그레이드 완료 통지시 부분
        /// </summary>
        /// <param name="cardSetting"></param>
        public void UpdateCardUpgrade(CardSetting cardSetting)
        {
            //완료된 카드가 아닌 업글중인 카드라면 다른곳에 업데이트 해야함

            //카드 완료 큐에 넣기
            UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(cardSetting);
        }

        /// <summary>
        /// 카드 저장 사용하기 위해 맵위에
        /// </summary>
        public void CardSaveToUseOnMap()
        {
            //Destory 삭제
            if (GlobalDataController.Instance.SelectAbleAllCard != null) { GlobalDataController.Instance.SelectAbleAllCard.ForEach(card => Destroy(card)); }
            if (GlobalDataController.Instance.SelectAbleAmmoCard != null) { GlobalDataController.Instance.SelectAbleAmmoCard.ForEach(card => Destroy(card)); }
            if (GlobalDataController.Instance.SelectAbleCharacterCard != null) { GlobalDataController.Instance.SelectAbleCharacterCard.ForEach(card => Destroy(card)); }
            if (GlobalDataController.Instance.SelectAbleWeaponCard != null) { GlobalDataController.Instance.SelectAbleWeaponCard.ForEach(card => Destroy(card)); }

            //카드 초기화
            GlobalDataController.Instance.SelectAbleAllCard.Clear();
            GlobalDataController.Instance.SelectAbleAmmoCard.Clear();
            GlobalDataController.Instance.SelectAbleCharacterCard.Clear();
            GlobalDataController.Instance.SelectAbleWeaponCard.Clear();


            //캐릭터 카드 넣기 일회용 만들어 넣기
            GlobalDataController.MapSetting.RoundEndCardSetting.Character.ForEach(card => {
                GlobalDataController.Instance.SelectAbleCharacterCard.Add(Instantiate<CardSetting>(card) as CardSetting);
            });
            //현재 선택 캐릭터 카드가 없다면 추가
            CardSetting characterCard = GlobalDataController.Instance.SelectAbleCharacterCard.Find(card => card.Equals(GlobalDataController.SelectedCharacterCard));
            if (characterCard == null)
            {
                GlobalDataController.Instance.SelectAbleCharacterCard.Add(Instantiate<CardSetting>(GlobalDataController.SelectedCharacterCard) as CardSetting);
            }


            //무기 카드 넣기 일회용 만들어 넣기
            GlobalDataController.MapSetting.RoundEndCardSetting.Weapon.ForEach(card => {
                GlobalDataController.Instance.SelectAbleWeaponCard.Add(Instantiate<CardSetting>(card) as CardSetting);
            });
            //현재 선택 캐릭터 카드가 없다면 추가
            CardSetting weaponCard = GlobalDataController.Instance.SelectAbleWeaponCard.Find(card => card.Equals(GlobalDataController.SelectedWeaponCard));
            if (weaponCard == null)
            {
                GlobalDataController.Instance.SelectAbleWeaponCard.Add(Instantiate<CardSetting>(GlobalDataController.SelectedWeaponCard) as CardSetting);
            }


            //탄약 카드 넣기 일회용 만들어 넣기
            GlobalDataController.MapSetting.RoundEndCardSetting.Ammo.ForEach(card => {
                GlobalDataController.Instance.SelectAbleAmmoCard.Add(Instantiate<CardSetting>(card) as CardSetting);
            });
            //현재 선택 캐릭터 카드가 없다면 추가
            CardSetting ammoCard = GlobalDataController.Instance.SelectAbleAmmoCard.Find(card => card.Equals(GlobalDataController.SelectedAmmoCard));
            if (ammoCard == null)
            {
                GlobalDataController.Instance.SelectAbleAmmoCard.Add(Instantiate<CardSetting>(GlobalDataController.SelectedAmmoCard) as CardSetting);
            }
                

            //맵에서 사용될 카드 세팅
            Model.cardSettings.ForEach(value => {
                //사용될 언락된 일반 카드 저장
                if (value.UseAble && ((0 <= (int)value.Type) && ((int)value.Type < 100)))
                {
                    //캐릭터,무기,탄약 카드 제외하고 모두 더하기
                    //*새로 생성해서 더하기함
                    GlobalDataController.Instance.SelectAbleAllCard.Add(Instantiate<CardSetting>(value) as CardSetting);
                }
            });

            //캐릭터,무기,탄약 카드가 모두 세팅된 후에 마지막에 사용될카드 더함
            GlobalDataController.Instance.SelectAbleCharacterCard.ForEach(card => GlobalDataController.Instance.SelectAbleAllCard.Add(card));
            GlobalDataController.Instance.SelectAbleWeaponCard.ForEach(card => GlobalDataController.Instance.SelectAbleAllCard.Add(card));
            GlobalDataController.Instance.SelectAbleAmmoCard.ForEach(card => GlobalDataController.Instance.SelectAbleAllCard.Add(card));

            //SelectAbleCharacterCard 안의 같은 카드로 교체
            //*원본 카드를 복제 카드로 모두 교체-> SelectAble카드의 참조로 교체
            GlobalDataController.SelectedCharacterCard = GlobalDataController.Instance.SelectAbleCharacterCard.Find(delegate(CardSetting card) {
                return card.Type.Equals(GlobalDataController.SelectedCharacterCard.Type);
	        });

            GlobalDataController.SelectedWeaponCard = GlobalDataController.Instance.SelectAbleWeaponCard.Find(delegate (CardSetting card) {
                return card.Type.Equals(GlobalDataController.SelectedWeaponCard.Type);
            });

            GlobalDataController.SelectedAmmoCard = GlobalDataController.Instance.SelectAbleAmmoCard.Find(delegate (CardSetting card) {
                return card.Type.Equals(GlobalDataController.SelectedAmmoCard.Type);
            });


        }

        bool FindCard(CardSetting target,CardSetting compare)
        {
            if(target.Type.Equals(compare.Type))
            {
                return true;
            }   

            return false;
        }

        /// <summary>
        /// 글로벌 데이터에 현재 카드중 언락된 카드 모두 선택 가능 카드로 세팅
        /// </summary>
        /*public void SetSelectAbleAllCard()
        {
            Model.cardSettings.ForEach(value => {
                //언락이 풀린 카드 인지 확인
                if (value.UseAble)
                {
                    //언락 됬다면 카드 세팅
                    GlobalDataController.Instance.SelectAbleAllCard.Add(value);
                }
            });
        }*/
    }
}
