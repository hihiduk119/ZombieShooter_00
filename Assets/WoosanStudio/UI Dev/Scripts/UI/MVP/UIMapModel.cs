using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 맵 데이터
    /// 모든 맵 데이터와 동기화도 진행
    /// *MVP 모델
    /// </summary>
    public class UIMapModel : MonoBehaviour
    {
        [Header("[모든 맵 세팅 => 세이브 로드시 반드시 동기화 해야함]")]
        /// *Data변경시 마다 세이브 해야함.
        public List<Map.Setting> mapSettings = new List<Map.Setting>();

        /*
        /// <summary>
        /// 카드 데이터의 불러오기
        /// Map.Setting의 Load 호출
        /// </summary>
        public void Load()
        {
            //맵의 저장 데이터 로드
            //*Stage데이터 로드
            mapSettings.ForEach(value => value.Load());
        }

        /// <summary>
        /// 테스트용 별 추가
        /// </summary>
        public void TestSave()
        {
            //mapSettings.ForEach(value => value.data.StageDatas[0].StarCount = 2);
            //mapSettings.ForEach(value => value.Save());
            mapSettings[0].data.StageDatas[0].RankCount = 2;
            mapSettings[0].Save();
        }

        /// <summary>
        /// 테스트용 데이터 제거
        /// </summary>
        public void TestRemove()
        {
            mapSettings.ForEach(value => value.Reset());
            //mapSettings[0].Reset();
        }


        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                TestSave();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                //모든 데이터 초기화
                TestRemove();
            }
        }
        #endregion
        */
    }
}
