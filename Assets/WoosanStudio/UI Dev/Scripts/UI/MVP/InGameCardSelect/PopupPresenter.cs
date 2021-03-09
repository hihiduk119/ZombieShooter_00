using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.UI.MVP.InGameCardSelect
{
    /// <summary>
    /// 카드 선택 팝업
    /// *MVP 모델
    /// </summary>
    public class PopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public PopupView View;

        [Header("[[Auto-> GlobalDataController]변경하려는 카드 => 반듯이 3개이상 있어야 함]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();

        [Header("[카드 아이템들")]
        public List<ItemPresenter> itemPresenters = new List<ItemPresenter>();

        //[Header("[현재 선택된 카드 인덱스]")]
        //public int CardIndex = 0;

        [Header("[플레이어 카드중 캐릭터")]
        public PlayerItemPresenter characterItemPresenter;
        [Header("[플레이어 카드중 무기")]
        public PlayerItemPresenter weaponItemPresenter;
        [Header("[플레이어 카드중 탄약")]
        public PlayerItemPresenter ammoItemPresenter;

        [Header("[[확인용] 선택된 카드")]
        public CardSetting SelectedCard;

        //데이터 부분
        //최초 3개=> 언락에 의해 6개까지 풀림
        private int CurrentMaxCardCount = 3;

        private void Awake()
        {
            //로비에서 가져온 카드 넣기
            //*이때 6개만,최대중첩 제외,셔플
            cardSettings = GlobalDataController.Instance.GetSixCardExcludingNotMaxStack();

            //처음 시작시 카드 세팅
            CardsUpdate(true);

            //아이템 플레젠터에 캐릭터 카드 세팅
            characterItemPresenter.cardSettings = GlobalDataController.Instance.SelectAbleCharacterCard;
            //아이템 플레젠터에 무기 카드 세팅
            weaponItemPresenter.cardSettings = GlobalDataController.Instance.SelectAbleWeaponCard;
            //아이템 플레젠터에 탄약 카드 세팅
            ammoItemPresenter.cardSettings = GlobalDataController.Instance.SelectAbleAmmoCard;

            //카드 업데이트 이벤트 연결
            characterItemPresenter.UpdateCardEvent.AddListener(UpdateCharacterCard);
            //카드 업데이트 이벤트 연결
            weaponItemPresenter.UpdateCardEvent.AddListener(UpdateWeaponCard);
            //카드 업데이트 이벤트 연결
            ammoItemPresenter.UpdateCardEvent.AddListener(UpdateAmmoCard);
        }

        private void Start()
        {
            //0번 카드 포커스 상태로 맡들기
            itemPresenters[0].Focus();

            //팝업 생성시 선택된 카드 아이템 설정
            characterItemPresenter.ChangeCard((int)GlobalDataController.SelectedCharacterCard.Type - 300);
            //팝업 생성시 선택된 카드 아이템 설정
            weaponItemPresenter.ChangeCard((int)GlobalDataController.SelectedCharacterCard.Type - 100);
            //팝업 생성시 선택된 카드 아이템 설정
            ammoItemPresenter.ChangeCard((int)GlobalDataController.SelectedCharacterCard.Type - 200);

            //카드 선택 송 플레이
            Audio.SongManager.Instance.PlayCardSelectionSong();
        }

        /// <summary>
        /// 카드들을 각 업데이트
        /// </summary>
        /// <param name="isFirst">첫 시작시 리스너 등록</param>
        private void CardsUpdate(bool isFirst = false)
        {
            int addPriceCount = 0;
            //카드버튼 카드데이터 만큼 세팅
            for (int i = 0; i < cardSettings.Count; i++)
            {
                //3개 이후부터 Add Price 세팅
                if (2 < i)
                {
                    itemPresenters[i].AddPrice = GlobalDataController.CardAddPrices[addPriceCount];

                    //Debug.Log( "["+ addPriceCount + "]  price = " + GlobalDataController.CardAddPrices[addPriceCount]);

                    addPriceCount++;
                }

                if (i < CurrentMaxCardCount)         //현재 최대 카운트 만큼 활성화
                {
                    itemPresenters[i].Initialize(cardSettings[i], ItemPresenter.ItemState.SelectAble);
                }
                else if(i == CurrentMaxCardCount)   //최대 카운트와 같다면 Add활성화
                {
                    itemPresenters[i].Initialize(cardSettings[i], ItemPresenter.ItemState.Add);
                }
                else                                //나머지는 Empty상태
                {
                    itemPresenters[i].Initialize(cardSettings[i], ItemPresenter.ItemState.Empty);
                }

                //포커스시 언포커스 시킴
                //itemPresenters[i].FocusedEvent.AddListener(AllUnfocus);

                //최초 한번 실행
                if(isFirst)
                {
                    //포커스 리스너 등록
                    itemPresenters[i].FocusedEvent.AddListener(UpdateView);

                    //카드 추가 성공 리스너 등록
                    itemPresenters[i].AddSucceededEvent.AddListener(AddCard);
                }
            }
        }

        /// <summary>
        /// 모든 포커스 언포커스 시킴
        /// </summary>
        private void AllUnfocus(CardSetting cardSetting)
        {
            itemPresenters.ForEach(value => value.Unfocus());
        }

        /// <summary>
        /// 포커스에 선택한 카드 정보 활성화
        /// </summary>
        /// <param name="cardSetting"></param>
        private void UpdateView(CardSetting cardSetting)
        {
            string stackCount = cardSetting.StackCount + "/"+cardSetting.MaxStack;
            View.UpdateView(cardSetting.Name, stackCount, cardSetting.AllDescriptionForSelectedCardInfo());

            itemPresenters.ForEach(value => value.Unfocus());

            //카드 선택됨
            SelectedCard = cardSetting;
        }

        /// <summary>
        /// 카드 추가하기
        /// </summary>
        private void AddCard()
        {
            //카드 카운트 증가
            CurrentMaxCardCount++;

            //itemPresenters[CurrentMaxCardCount - 1].AddSucceededEvent.RemoveListener(AddCard);

            //Debug.Log("CurrentMaxCardCount  = " + CurrentMaxCardCount);

            //카드 카운트는 카드 갯수보다 클수 없다.
            //ex)카드 갯수가 4개라면 CurrentMaxCardCount 는 4가 최대.
            //if (CurrentMaxCardCount > cardSettings.Count) { CurrentMaxCardCount = cardSettings.Count; }

            //증가한 카드 반영하여 업데이트
            CardsUpdate();
        }

        /// <summary>
        /// 캐릭터 카드 업데이트
        /// *PlayerItemPresenter.CardChange()에서 이벤트 호출
        /// </summary>
        private void UpdateCharacterCard(CardSetting cardSetting)
        {
            //*참조 클론 형태인지 확인
            GlobalDataController.SelectedCharacterCard = cardSetting;
        }

        /// <summary>
        /// 무기 카드 업데이트
        /// *PlayerItemPresenter.CardChange()에서 이벤트 호출
        /// </summary>
        private void UpdateWeaponCard(CardSetting cardSetting)
        {
            //*참조 클론 형태인지 확인
            GlobalDataController.SelectedWeaponCard = cardSetting;
        }

        /// <summary>
        /// 탄약 카드 업데이트
        /// *PlayerItemPresenter.CardChange()에서 이벤트 호출
        /// </summary>
        private void UpdateAmmoCard(CardSetting cardSetting)
        {
            //*참조 클론 형태인지 확인
            GlobalDataController.SelectedAmmoCard = cardSetting;
        }

        /// <summary>
        /// OK 클릭
        /// </summary>
        public void ClickedOk()
        {
            //선택된 카드 스택 증가
            //*스택 증가는 해당 카드 데이터 반영을 의미
            SelectedCard.StackCount++;

            //사운드 변경
            Audio.SongManager.Instance.PlayBattleSong(Random.Range(0, 10));

            //카드 데이터를 캐릭터에 적용하는 코드 필요
        }
    }
}
