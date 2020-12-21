using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// *MVP 모델
    /// </summary>
    public class CoinPresenter : MonoBehaviour
    {
        [Header("[MVP Model]")]
        public CoinModel Model;

        [System.Serializable]
        public class UpdateCoinEvent : UnityEvent<int> { }

        public UpdateCoinEvent UpdateEvent = new UpdateCoinEvent();

        [Header("[코인 빼기 실패 이벤트]")]
        public UnityEvent SubtractCoinFailEvent = new UnityEvent();

        private void Start()
        {
            //모델 초기화
            Model.Initialize();

            //업데이트 이벤트
            UpdateEvent.Invoke(Model.data.Coin);
        }

        /// <summary>
        /// 코인 더하기
        /// </summary>
        /// <param name="value"></param>
        public void AddCoin(int value)
        {
            Model.data.Coin += value;

            //0보다 작을수는 없다.
            if(Model.data.Coin < 0 )
            {
                Model.data.Coin = 0;
                Debug.Log("Coin 0 보다 작은 값 발생");
            }

            //업데이트 이벤트
            UpdateEvent.Invoke(Model.data.Coin);

            Model.Save();
        }

        /// <summary>
        /// 코인 빼기
        /// * 0뻬는 값의 크기가 0보다 작으면 실패 리턴.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SubtractCoin(int value)
        {
            //value가 0보다 작다면 계산하지 않는다.
            int subtractValue = Model.data.Coin - value;
            bool success = true;

            //코인은 0보다 작을수 없다.
            if (subtractValue <= 0)
            {
                success = false;
                //코인 빼기 실패 이벤트 호출
                SubtractCoinFailEvent.Invoke();
            }
            else
            {
                Model.data.Coin -= value;

                UpdateEvent.Invoke(Model.data.Coin);

                Model.Save();
            }

            return success;
        }

        /// <summary>
        /// 현재 코인 알아오기
        /// </summary>
        /// <returns></returns>
        public int GetCoin()
        {
             return Model.data.Coin;
        }

        #region [-TestCode]
        void Update()
        {
            //경험치 추가
            if (Input.GetKeyDown(KeyCode.A))
            {
                AddCoin(100000);
            }

            //경험치 추가
            if (Input.GetKeyDown(KeyCode.S))
            {
                SubtractCoin(500);
            }
        }
        #endregion
    }
}
