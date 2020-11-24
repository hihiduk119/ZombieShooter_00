﻿using System.Collections;
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

        [Header("[완료카드 리스트]")]
        //CardSetting.CheckTheCardToUpgradeComplated()에 의해 추가됨.
        public List<CardSetting> UpgradeComplatedCardList = new List<CardSetting>();

        [Header("[업그레이드 완료 이벤트]")]
        //완료된 이벤트는 카드 완료 카드 리스트에 넣어서 처리
        static public ComplateEvent UpgradeComplateEvent = new ComplateEvent();

        [Header("[업그레이드 데이터 업데이트 완료 이벤트]")]
        static public ComplateEvent ComplatedToUpdatedTheUpgradeDataEvent = new ComplateEvent();

        [Header("[카드 업그레이드 결과 팝업 컨트롤러]")]
        public CardUpgradeResultPopupController cardUpgradeResultPopupController;

        //메시지가 찾는지 아닌 확인용
        private bool isQueueEmpty = true;

        public enum State
        {
            Seek,
            None,
        }

        public enum Action
        {
            Show,
            Showing,
            Done,
        }


        private State state = State.Seek;
        private Action action = Action.Show;

        //메시지 표시 조건
        //1.로비에 왔을때 메시지 큐를 확인(이미 UICardPresenter에서 처리함) 하고 메시지 있으면 출력
        //2.메시지큐에 뭔가 추가 됬고 현재 씬리 PresentScene이라면 출력

        private void Awake()
        {
            //업그레이드 이벤트 처리를 위해 메시치 처리기 등록
            UpgradeComplateEvent.AddListener(CheckMessagePop);
            
            DontDestroyOnLoad(this);

            //FSM 실행
            StartCoroutine(state.ToString());
        }

        /// <summary>
        /// 업그레이드 완료 메시지 처리
        /// </summary>
        void CheckMessagePop(CardSetting cardSetting)
        {
            UpgradeComplatedCardList.Add(cardSetting);
            Debug.Log(cardSetting.Name + " 의 완료를 감지 했습니다.");

            //1.로비에 왔을때 메시지 큐를 확인하고 메시지 있으면 출력
        }

        /// <summary>
        /// 최종 업데이트된 카드 제거
        /// * 카드 완료 팝업에서 호출되어야 함.
        /// </summary>
        /// <param name="cardSetting"></param>
        void RemoveUpdatedCard(CardSetting cardSetting)
        {
            for (int i = 0; i < UpgradeComplatedCardList.Count; i++)
            {
                //같은 거 찾음
                if (UpgradeComplatedCardList[i].Name.Equals(cardSetting.Name))
                {
                    //찾은 완료 카드 삭제
                    UpgradeComplatedCardList.RemoveAt(i);
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
            //cardUpgradeResultPopupController.OpenResult();
            
        }

        /// <summary>
        /// 대기 상태
        /// </summary>
        /// <returns></returns>
        IEnumerator None()
        {
            while (true)
            {
                //완료된 카드가 리스트에 들어왔다
                if (0 < UpgradeComplatedCardList.Count)
                {
                    //팝업으로 보여주기 상태로 변경
                    state = State.Seek; StartCoroutine(state.ToString());
                    yield break;
                }
                yield return new WaitForSeconds(1f);
            }
        }

        /// <summary>
        /// 대기 상태
        /// </summary>
        /// <returns></returns>
        IEnumerator Seek()
        {
            while(0 < UpgradeComplatedCardList.Count)
            {

                switch (action)
                {
                    case Action.Show:
                        //여기서 화면 보여주는 액션을 해야함.
                        action = Action.Showing;
                        break;
                    case Action.Showing:
                        //보여주는 부분에서 완료 통지를 받기위해 대기하는 부분.
                        
                        break;
                    case Action.Done:
                        //완료가 끝났다면 다음것 Show함.
                        action = Action.Show;
                        break;
                    default:
                        break;
                }

                yield return new WaitForSeconds(0.33f);
            }
        }
    }
}
