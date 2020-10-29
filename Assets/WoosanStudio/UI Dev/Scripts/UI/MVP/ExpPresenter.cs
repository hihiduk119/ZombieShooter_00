using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 경험치 프리젠터
    /// *MPV 모델
    /// </summary>
    public class ExpPresenter : MonoBehaviour
    {
        //MVP Model
        public ExpModel Model;

        public ExpView View;

        [System.Serializable]
        public class ExpSliderEvent : UnityEvent<float> { }

        [System.Serializable]
        public class LevelUpEvent : UnityEvent<int> { }

        [Header("[경험치 슬라이더 업데이트 이벤트]")]
        public ExpSliderEvent UpdateExpSliderEvent = new ExpSliderEvent();

        [Header("[레벨 업데이트 이벤트]")]
        public LevelUpEvent UpdateLevelUpEvent = new LevelUpEvent();

        void Start()
        {
            //모델 초기화
            Model.Initialize();

            //변경된 레벨 전달
            UpdateLevelUpEvent.Invoke(Model.data.Level);

            //변경된 경험치 슬라이더 값 전달
            UpdateExpSliderEvent.Invoke(GetExpSliderValue());
        }

        /// <summary>
        /// 슬라이더 값 가져오기
        /// </summary>
        public float GetExpSliderValue()
        {
            float value;

            value = (float)Model.data.CurrentExp / (float)Model.MaxExpList[Model.data.Level - 1];

            return value;
        }

        /// <summary>
        /// 경험치 누적
        /// </summary>
        /// <param name="value"></param>
        public void AddExp(int value)
        {
            //현재 Exp에 추가
            Model.data.CurrentExp += value;

            //레벨이 업됬는지 확인
            if (Model.MaxExpList[Model.data.Level-1] <= Model.data.CurrentExp)
            {
                int extraExp = Model.data.CurrentExp - Model.MaxExpList[Model.data.Level - 1];
                //레벨없 시킴
                Model.data.Level++;
                //남은 경험치 현재 경험치에 넣음
                Model.data.CurrentExp = extraExp;

                //변경된 레벨 전달
                UpdateLevelUpEvent.Invoke(Model.data.Level);
            }

            //변경된 경험치 슬라이더 값 전달
            UpdateExpSliderEvent.Invoke(GetExpSliderValue());



            //데이터 저장
            Model.Save();
        }


        #region [-TestCode]
        void Update()
        {
            //경험치 추가
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddExp(100);
            }
        }
        #endregion

    }
}
