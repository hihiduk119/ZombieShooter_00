using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// *MPV 모델
    /// </summary>
    public class GemPresenter : MonoBehaviour
    {
        [Header("[MVP Model]")]
        public GemModel Model;

        [System.Serializable]
        public class UpdateCoinEvent : UnityEvent<int> { }

        [Header("[코인 빼기 실패 이벤트]")]
        public UnityEvent SubtractGemFailEvent = new UnityEvent();

        public UpdateCoinEvent UpdateEvent = new UpdateCoinEvent();

        private void Start()
        {
            //모델 초기화
            Model.Initialize();

            //업데이트 이벤트
            UpdateEvent.Invoke(Model.data.Gem);
        }

        /// <summary>
        /// 코인 더하기
        /// </summary>
        /// <param name="value"></param>
        public void AddGem(int value)
        {
            Model.data.Gem += value;

            //업데이트 이벤트
            UpdateEvent.Invoke(Model.data.Gem);

            Model.Save();
        }

        /// <summary>
        /// 코인 빼기
        /// * 0뻬는 값의 크기가 0보다 작으면 실패 리턴.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SubtractGem(int value)
        {
            //value가 0보다 작다면 계산하지 않는다.
            int subtractValue = Model.data.Gem - value;
            bool success = true;

            //코인은 0보다 작을수 없다.
            if (subtractValue <= 0)
            {
                success = false;
                //코인 빼기 실패 이벤트 호출
                SubtractGemFailEvent.Invoke();
            }
            else
            {
                Model.data.Gem -= value;

                UpdateEvent.Invoke(Model.data.Gem);

                Model.Save();
            }

            return success;
        }

        #region [-TestCode]
        //void Update()
        //{
        //    //경험치 추가
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        AddGem(100);
        //    }

        //    //경험치 추가
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        SubtractGem(500);
        //    }
        //}
        #endregion
    }
}
