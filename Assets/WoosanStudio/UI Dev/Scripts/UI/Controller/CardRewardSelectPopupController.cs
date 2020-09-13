using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 보상 카드를 선택하는 팝업 컨트롤러.
    /// 보상 카드를 선택 및 해당 선택 결과를 해당 플레이어에 카드 반영하는 곳에 전달 또는 이벤트 처리
    /// </summary>
    public class CardRewardSelectPopupController : MonoBehaviour
    {
        [Header("[카드 선택]")]
        public CardSelectionController CardSelection;

        [Header("[카드 데이터 리스트]")]
        //현재 세팅되었는 3개는 임시 데이터
        public List<CardSetting> cardSettings = new List<CardSetting>();

        [Header("[스킬 타이틀]")]
        public Text title;

        [Header("[스킬 스택 카운트]")]
        public Text stack;

        [Header("[스킬 내용]")]
        public Text contexts;

        [Header("[Ok 버튼 click 컨트롤")]
        //클릭 이벤트를 제어하기 위해 사용
        public Ricimi.BasicButton BasicButton;


        //캐쉬용
        private StringBuilder stringBuilder;
        string[] splits;
        private string descripsion;


        #region [-TestCode] 임시확인용으로 생성시 임시 카드 3개를 바로 업데이트
        private void Awake()
        {
            //1번카드 업데이트
            UpdateCards(0, cardSettings[0]);
            //2번카드 업데이트
            UpdateCards(1, cardSettings[1]);
            //3번카드 업데이트
            UpdateCards(2, cardSettings[2]);
        }
        #endregion

        /// <summary>
        /// 1-3번 까지의 카드를 업데이트 합니다
        /// </summary>
        /// <param name="index">해당 카드 업데트</param>
        /// /// <param name="cardSetting">해당 카드 정보</param>
        void UpdateCards(int index, CardSetting cardSetting)
        {
            //해당 되는 인덱스 카드 이미지 업데이트
            CardSelection.SelectItems[index].IconUpdate(cardSetting.Sprite,cardSetting.IconColor, cardSetting.Scale);
        }
         
        /// <summary>
        /// 카드 선택에 의해 변경된
        /// 화면에 보이는 카드 정보를 변경
        /// </summary>
        void ChangeCardInformation(CardSetting cardSetting)
        {
            //[LV 20] [이름]
            //타이틀 이름 생성
            stringBuilder = new StringBuilder("LV ");
            stringBuilder.Append(cardSetting.Level + 1);
            stringBuilder.AppendLine();
            //stringBuilder.Append(" ");
            stringBuilder.Append(cardSetting.Title);

            //타이틀에 적용
            this.title.text = stringBuilder.ToString();

            //재사용 위해 초기화
            stringBuilder.Clear();



            #region [-스택 카운트 표기]
            /*stringBuilder.Append("[");
            //최대 스택 카운트-1 보다 현재 카드 카운트가 같다면
            //"MAX"로 표기
            if (cardSetting.MaxStack -1 == cardSetting.StackCount)
            {
                stringBuilder.Append("MAX");
            } else //아니면 해당 카운트에 + 1 증가 표기 => 다음에 어떨지 표기임으로
            {
                stringBuilder.Append(cardSetting.StackCount + 1);
            }

            stringBuilder.Append("/");
            //0부터 시작이기에 실제 표기에는 +1증가
            stringBuilder.Append(cardSetting.MaxStack + 1);
            stringBuilder.Append("]");
            stack.text = stringBuilder.ToString();

            //재사용 위해 초기화
            stringBuilder.Clear();*/
            #endregion


            #region [-내구도 표기]
            stringBuilder.Append("Durability : [");
            stringBuilder.Append(cardSetting.Durability);
            stringBuilder.Append("/");
            stringBuilder.Append(cardSetting.MaxDurability);
            stringBuilder.Append("]");
            stack.text = stringBuilder.ToString();
            //재사용 위해 초기화x
            stringBuilder.Clear();
            #endregion

            //카드의 프로퍼티의 내용들을 하나로 묶음
            for (int i = 0; i < cardSetting.Properties.Count; i++)
            {
                //일단 기본 문장 넣음
                descripsion = cardSetting.Properties[i].Descripsion;
                //"/d를 찾아서 해당 레벨에 맞는 값으로 변경"
                //먼저 찾아야 한다.
                
                //"/d"의 첫번째 /를 알아옴
                int index = descripsion.IndexOf('/');
                //'/' -> 바로 뒤가 d인지 확인
                //해당 카드에 레벨에 따라 수치가 변하는 정보가 있느냐?
                if (descripsion[index + 1].Equals('d')) {
                    //isFind = true;
                    //기본 값 + 카드 1레벨당 상승 값 * 카드 레벨 
                    int value = (cardSetting.Level * cardSetting.Properties[i].IncreasedValuePerLevelUp) + cardSetting.Properties[i].Value;

                    //'/d'를 삭제
                    descripsion = descripsion.Remove(index,2);
                    //삭제된 위치에 계산됨 값 넣기
                    descripsion = descripsion.Insert(index, value.ToString());
                }

                //계산 완료후 완성된 문장 한줄을 더함.
                stringBuilder.Append(descripsion);
                //한줄 개행
                stringBuilder.AppendLine();
            }

            //UI에 적용
            contexts.text = stringBuilder.ToString();
        }

        /// <summary>
        /// 첫번째 카드 선택됨
        /// </summary>
        /// <param name="value">활성 상태</param>
        public void OnValueChangeByFirst(bool value)
        {
            if (value) {
                Debug.Log("첫째 카드 선택 " + value);
                //카드 UI 선택
                CardSelection.Select(0);

                //선택시 해당 카드 정보 업데이트
                ChangeCardInformation(cardSettings[0]);
            }
        }

        /// <summary>
        /// 두번째 카드 선택됨
        /// </summary>
        /// <param name="value">활성 상태</param>
        public void OnValueChangeBySecond(bool value)
        {
            if (value) {
                Debug.Log("둘째 카드 선택 " + value);
                //카드 UI 선택
                CardSelection.Select(1);

                //선택시 해당 카드 정보 업데이트
                ChangeCardInformation(cardSettings[1]);
            }
        }

        /// <summary>
        /// 세번째 카드 선택됨
        /// </summary>
        /// <param name="value">활성 상태</param>
        public void OnValueChangeByThird(bool value)
        {
            if (value) {
                Debug.Log("셋째 카드 선택 " + value);
                //카드 UI 선택
                CardSelection.Select(2);

                //선택시 해당 카드 정보 업데이트
                ChangeCardInformation(cardSettings[2]);
            }
        }
    }
}
