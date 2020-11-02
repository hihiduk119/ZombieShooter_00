using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 카드 모델 데이터를 필요에 따라 동기화 시키는 역활을 함.
    /// *MPV 모델
    /// </summary>
    public class UICardPresenter : MonoBehaviour
    {
        [Header("[MVP Model]")]
        public UICardModel Model;


        IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            Initialize();
        }

        /// <summary>
        /// 최초 사용시 기존 데이터 모두 로드
        /// </summary>
        public void Initialize()
        {
            //싱크로 호출하여 데이터 동기화
            Model.Synchronization();
        }
    }
}
