using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 메시지를 UI 에 출력
    /// </summary>
    public class MessagePrinter : MonoBehaviour
    {
        [Header ("[표시할 메시지]")]
        public string Message;

        [Header("[몇초가 표시할지]")]
        public float duration = 3f;

        [Header("[해당 프리팹]")]
        public GameObject messagePrefab;

        protected Canvas m_canvas;


        /// <summary>
        /// 람다식 코루틴
        /// </summary>
        /// <param name="time">대기시간</param>
        /// <param name="action">실행액션</param>
        /// <returns></returns>
        IEnumerator WaitAndDoCoroutine(float time, System.Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        protected void Start()
        {
            GameObject canvasObject = GameObject.Find("Canvas");

            if (canvasObject != null)
                m_canvas = canvasObject.GetComponent<Canvas>();

            canvasObject = GameObject.Find("Robby Canvas");

            if (canvasObject != null)
                m_canvas = canvasObject.GetComponent<Canvas>();
        }

        /// <summary>
        /// 팝업 생성
        /// </summary>
        public void OpenPopup()
        {
            var message = Instantiate(messagePrefab) as GameObject;
            message.SetActive(true);
            message.transform.localScale = Vector3.one;
            message.transform.SetParent(m_canvas.transform, false);
            //표시할 메시지 집어 넣기
            message.GetComponent<Text>().text = Message;

            //글자 연출 시작
            Show(message.GetComponent<Text>());
        }

        /// <summary>
        /// 팝업 생성 및 리턴
        /// </summary>
        /// <returns></returns>
        public GameObject OpenPopupAndReturn()
        {
            var message = Instantiate(messagePrefab) as GameObject;
            message.SetActive(true);
            message.transform.localScale = Vector3.one;
            message.transform.SetParent(m_canvas.transform, false);
            //표시할 메시지 집어 넣기
            message.GetComponent<Text>().text = Message;

            //글자 연출 시작
            Show(message.GetComponent<Text>());

            return message;
        }

        /// <summary>
        /// 실제 글자 연출
        /// </summary>
        /// <param name="text"></param>
        private void Show(Text text)
        {
            //투명상태로 초기화
            Color tmpColor = text.color;
            tmpColor.a = 0;
            text.color = tmpColor;

            //글자 페이드 표시
            text.DOFade(1f, 0.2f).OnComplete(()=>
            //3초간 대기
                StartCoroutine(WaitAndDoCoroutine(duration, () =>
                {
                    //글자 페이드 닫기 
                    text.DOFade(0f, 0.2f).OnComplete(() =>
                        //종료후 삭제
                        Destroy(text.gameObject)
                    );
                })));
        }


        #region [-TestCode]
        //void Update()
        //{
        //    //메시지 정상 출력 되는지 테스트
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        OpenPopup();
        //    }
        //}
        #endregion

    }
}
