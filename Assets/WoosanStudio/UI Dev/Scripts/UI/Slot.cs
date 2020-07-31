using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

namespace WoosanStudio.ZombieShooter
{
    public class Slot : MonoBehaviour
    {
        //버튼의 상태는 활성 여부를 결정한다
        [SerializeField]
        public enum ButtonState //순서는 Buttons 리스트와 같다
        {
            Cancel = 0,     //취소 버튼
            Research,   //연구 버튼
            None,       //버튼 없음
        }

        [Header ("[현재 슬로의 이름]")]
        public Text Name;
        [Header("[슬로의 세부 내용]")]
        public Text Description;
        [Header("[슬롯의 이미지]")]
        public Image SlotImage;
        [Header("[가지고 있는 버튼 리스트]")]
        public List<GameObject> Buttons = new List<GameObject>();
        [Header("[버튼의 활성 상태 결정]")]
        public ButtonState CurrentButtonState =  ButtonState.None;

        [Header("[프로그래스 바의 남은 시간]")]
        public Text TextProcess;
        [Header("[프로그래스 바]")]
        public Image ProgressBar;

        //가져온 실제 시간 데이터
        Timeset timeset;
        //시간을 돌릴 코루틴 0.5 tick으로 시간 업데이트
        Coroutine timeUpdate;

        private void Awake()
        {
            //시작시 모든 버튼     비활성화
            Buttons.ForEach(value => value.SetActive(false));
        }

        #region [-TestCode]
        public void Start()
        {
            //5분 타이머
            ActiveTimer(TextProcess,ProgressBar,new Timeset(5));
        }
        #endregion


        /// <summary>
        /// 해당 슬롯의 데이터를 세팅
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="slotSprite"></param>
        /// <param name="buttonState"></param>
        public void SetSlot(string name, string description, Sprite slotSprite, ButtonState buttonState)
        {
            Name.text = name;
            Description.text = description;
            SlotImage.sprite = slotSprite;

            //어떤 버튼을 활성화 할지 결정.
            switch(buttonState)
            {
                case ButtonState.Cancel:
                    Buttons[(int)buttonState].SetActive(true);
                    break;
                case ButtonState.Research:
                    Buttons[(int)buttonState].SetActive(true);
                    break;
                case ButtonState.None:
                    break;
            }
        }

        /// <summary>
        /// 타이머를 실제 동작시키는 메서드
        /// </summary>
        /// <param name="textProcess">표시되는 텍스트</param>
        /// <param name="progressBar">진행 프로그래스</param>
        /// <param name="timeset">실제 데이터</param>
        public void ActiveTimer(Text textProcess,Image progressBar , Timeset timeset)
        {
            if (timeUpdate != null) StopCoroutine(timeUpdate);
            timeUpdate = StartCoroutine(TimeUpdate(textProcess, progressBar, timeset));
        }

        IEnumerator TimeUpdate(Text textProcess, Image progressBar, Timeset timeset)
        {
            //1분 집어 넣음
            timeset = new Timeset(5);

            WaitForSeconds WFS = new WaitForSeconds(0.5f);

            while (true)
            {
                //남은 시간 알아오기
                timeset.GetRemainTime();
                TextProcess.text = timeset.TimeString;

                //0-1사이 값으로 변환한것 가져오기.
                ProgressBar.fillAmount = timeset.GetRemainValue();
                yield return WFS;
            }
        }
    }
}
