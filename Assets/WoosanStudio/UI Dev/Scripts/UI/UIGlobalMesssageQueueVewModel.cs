using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 글로벌 메시지들을 큐에 모아 로비씬에 왔을때 순차 통지
    /// *카드 업글 성공 메시지 호출
    /// *메시지가 여러개일 경우 순차적으로 호출
    /// *MVVM 모델
    /// </summary>
    public class UIGlobalMesssageQueueVewModel : MonoBehaviour
    {
        public class ComplateEvent : UnityEvent<CardSetting> {}

        [Header("[업그레이드 중인 카드 리스트]")]
        public List<CardSetting> UpgradingCardList = new List<CardSetting>();
        [Header("[업그레이드 완료된 카드 리스트]")]
        public List<CardSetting> UpgradComplateCardList = new List<CardSetting>();

        [Header("[업그레이드 완료 이벤트]")]
        //완료된 이벤트는 카드 완료 카드 리스트에 넣어서 처리
        static public ComplateEvent UpgradeComplateEvent = new ComplateEvent();

        [Header("[업그레이드 중 이벤트]")]
        static public ComplateEvent UpgradingEvent = new ComplateEvent();

        [Header("[업데이트 완료 확인 이벤트]")]
        static public UnityEvent ConfirmUpgradeComplatedEvent = new UnityEvent();

        [Header("[카드 업그레이드 결과 팝업 컨트롤러]")]
        public CardUpgradeResultPopupController cardUpgradeResultPopupController;

        //메시지가 찾는지 아닌 확인용
        private bool isQueueEmpty = true;

        [Header("[테스트 전용 나중에 삭제 필요]")]
        public CardSetting TestCardSetting;

        public enum State
        {
            Seek,
            Showing,
            //None,
        }

        public enum Action
        {
            Show,
            Done,
        }


        private State state = State.Seek;
        private Action action = Action.Show;

        //메시지 표시 조건
        //1.로비에 왔을때 메시지 큐를 확인(이미 UICardPresenter에서 처리함) 하고 메시지 있으면 출력
        //2.메시지큐에 뭔가 추가 됬고 현재 씬리 PresentScene이라면 출력

        private void Awake()
        {
            //업그레이드 중 등록
            UpgradingEvent.AddListener(UpdateUpgradingCard);

            //업그레이드 완료 등록
            UpgradeComplateEvent.AddListener(CheckMessagePop);

            DontDestroyOnLoad(this);

            //FSM 실행
            StartCoroutine(FSM());
        }

        /// <summary>
        /// 현재 카드 업그레이드 중으로 표시
        /// </summary>
        /// <param name="cardSetting"></param>
        void UpdateUpgradingCard(CardSetting cardSetting)
        {
            UpgradingCardList.Add(cardSetting);
        }

        /// <summary>
        /// 업그레이드 완료 메시지 처리
        /// *UICardModel.Load()에서 최초 실행 -> CardSetting.CheckTheCardToUpgradeComplated()
        /// 즉 최초 로드시에 전체 카드를 확인해서 완료한 카드가 있으면 팝업 시키기 위해 
        /// </summary>
        void CheckMessagePop(CardSetting cardSetting)
        {
            UpgradComplateCardList.Add(cardSetting);

        }

        /// <summary>
        /// 최종 업데이트된 카드 제거
        /// * 카드 완료 팝업에서 호출되어야 함.
        /// </summary>
        /// <param name="cardSetting"></param>
        void RemoveUpdatedCard(CardSetting cardSetting)
        {
            for (int i = 0; i < UpgradComplateCardList.Count; i++)
            {
                //같은 거 찾음
                if (UpgradComplateCardList[i].Name.Equals(cardSetting.Name))
                {
                    //찾은 완료 카드 삭제
                    UpgradComplateCardList.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 결과 표출
        /// *겜블의 경우 실패가 존재 -> 이 부분 디테일하게 설정 필요. 
        /// *그외 성공창 결과에 따라 다르게 표시 해야함. 
        /// </summary>
        public void PopCardUpgradeResult()
        {
            Debug.Log("[PopCardUpgradeResult]");
            //완료 이벤트 호출시Seek 상태로 갈수 있게 세팅
            ConfirmUpgradeComplatedEvent.AddListener(GoToSeek);

            CardSetting cardSetting = UpgradComplateCardList[0];

            //결과는 여기서 이미 만들고 반영
            CardSetting.UpgradeData upgradeDate = CardSetting.GetUpgradeResult(cardSetting);

            string strResult;
            string strLevel;

            if (upgradeDate.IsSuccess) { strResult = "SUCCESS";}
            else { strResult = "FAIL"; }
            //항상 표시할때는 +1한 값
            strLevel = (upgradeDate.SuccessLevel+1).ToString();

            //성공 결과 팝업으로 출력
            //* 결과는 이미 반영->단지 보여주기용
            cardUpgradeResultPopupController.OpenResult(cardSetting.Sprite,cardSetting.IconColor, cardSetting.Name, strResult, strLevel, upgradeDate.IsSuccess);

            //해당 카드 큐에서 제거
            RemoveUpdatedCard(cardSetting);
        }

        /// <summary>
        /// 완료된 카드가 있는지 없는지 업그레이드 중인 카드 리스트에서 찾는다.
        /// </summary>
        void CheckComplatedCardInUpgradingCard()
        {
            //미구현
        }

        /// <summary>
        /// 상태패턴 FSM
        /// </summary>
        /// <returns></returns>
        IEnumerator FSM()
        {
            // Execute the current coroutine (state)
            while (true) yield return StartCoroutine(state.ToString());
        }


        /// <summary>
        /// 왼료 카드가 있는지 찾는 상태
        /// </summary>
        /// <returns></returns>
        IEnumerator Seek()
        {
            //뭔가 들어왔는지 찾기
            bool isComplatedCard = true;

            while (isComplatedCard)
            {
                //Debug.Log("[Seek]");
                yield return new WaitForSeconds(0.33f);
                //업글중인 카드에서 완료 된 카드가 있는지 찾는 부분.
                CheckComplatedCardInUpgradingCard();
                //완료된 카드가 리스트에 들어왔다
                if (0 < UpgradComplateCardList.Count)
                {
                    //팝업으로 보여주기 상태로 변경
                    //액션은 완료 변경
                    action = Action.Show;

                    //Seek으로 이동
                    isComplatedCard = false;   
                }
            }

            state = State.Showing;
        }

        /// <summary>
        /// 팝업으로 보여주기 상태
        /// </summary>
        /// <returns></returns>
        IEnumerator Showing()
        {
            //카드 결과 호출
            PopCardUpgradeResult();

            //액션이 Done 이 되면 탈출
            //* 확인 버튼 눌렀다면
            while (action != Action.Done)
            {
                //Debug.Log("[Showing]");
                //무한 대기
                yield return new WaitForSeconds(0.1f);
            }

            //찾기 상태로 돌아감
            state = State.Seek;
        }

        /// <summary>
        /// Seek상태로 보냄
        /// * UICardUpgradeResultPopupPresenter.ClickOk() 에서 호출
        /// </summary>
        public void GoToSeek()
        {
            //Debug.Log("[GoToSeek]");
            //모든 확인 완료 리스너 제거
            ConfirmUpgradeComplatedEvent.RemoveAllListeners();
            //Seek로 이동
            //액션을 Done으로 바꾸면 Showing 루프 탈출
            action = Action.Done;
            //업그레이드가 반영 되어야 하는 View 업데이트 명령 내려야함.
            //*카드 연구 화면 업데이트 명령
            GameObject.FindObjectOfType<UICardResearchInfoPopupPresenter>().UpdateCardInfo();
        }

        //#region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        UIGlobalMesssageQueueVewModel.UpgradeComplateEvent.Invoke(TestCardSetting);
        //    }
        //}
        //#endregion
    }
}
