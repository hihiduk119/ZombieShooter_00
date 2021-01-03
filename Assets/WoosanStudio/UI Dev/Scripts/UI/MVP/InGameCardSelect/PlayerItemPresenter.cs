using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter.UI.MVP.InGameCardSelect
{
    /// <summary>
    /// 플레이어 선택 아이템
    /// *MVP 모델
    /// </summary>
    public class PlayerItemPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public PlayerItemView View;

        [Header("[[현재 임시로 넣어둠]변경하려는 카드 => 반듯이 1개이상 있어야 함]")]
        public List<CardSetting> cardSettings = new List<CardSetting>();

        [Header("[현재 선택된 카드 인덱스]")]
        public int CardIndex = 0;

        public class UpdateCard : UnityEvent<CardSetting> { }
        public UpdateCard UpdateCardEvent = new UpdateCard();

        private void Awake()
        {
            //클릭 이벤트 연결
            View.LeftEvent.AddListener(ChangeCard);
            View.RightEvent.AddListener(ChangeCard);
        }

        private void Start()
        {
            //처음 시작시 0으로 세팅
            ChangeCard(0);
        }

        /// <summary>
        /// 카드 변경
        /// </summary>
        void ChangeCard(int value)
        {
            //현재카드 인덱스에 반영 
            CardIndex += value;
            //선택 카드는 0보다 작을수 없다
            if (CardIndex < 0) { CardIndex = 0; }
            //선택 카드는 변경하려는 카드 갯수보다 클수 없다.
            if (CardIndex > cardSettings.Count) { CardIndex = cardSettings.Count-1; }

            //현재 카드인덱스가 왼쪽 끝이라면
            if (CardIndex == 0)
            {
                View.SetActiveLeftButton(false);
            } else
            {
                View.SetActiveLeftButton(true);
            }

            //현재 카드인덱스가 오른쪽 끝이라면
            if (CardIndex == cardSettings.Count-1)
            {
                View.SetActiveRightButton(false);
            }
            else//아니라면
            {
                View.SetActiveRightButton(true);
            }

            //뷰에 정보 업데이트
            View.UpdateView(cardSettings[CardIndex].Sprite, cardSettings[CardIndex].IconColor, cardSettings[CardIndex].Name);

            //해당 카드 업데이트 호출
            UpdateCardEvent.Invoke(cardSettings[CardIndex]);
        }
    }
}
