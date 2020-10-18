using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에너지 관련 정보 세이브, 로드
    /// MVC 패턴
    /// </summary>
    public class EnergyModel : MonoBehaviour
    {
        //최대 에너지
        //로드해서 가져옴
        public int MaxEnergy = 100;
        //현재 에너지
        //로드해서 가져옴
        public int CurrentEnergy = 0;
        //회복 하는 시간
        //로드해서 가져옴
        public int MaxRechargingTime = 45;

        //회복 하는 시간
        //로드해서 가져옴
        public int RemainRechargingTime = 0;

        //한번에 회복하는 수치
        //로드해서 가져옴
        public int RechargingPoint = 1;

        private WaitForEndOfFrame WFF = new WaitForEndOfFrame();

        private float deltaTime;

        /// <summary>
        /// 생성시 자동으로 돌며 에너지 업데이트
        /// 0.5s로 업데이트
        /// </summary>
        /// <returns></returns>
        IEnumerator Start()
        {
            //플레이어 데이터 로드
            Load();

            deltaTime = 0;

            while (true)
            {
                deltaTime += Time.deltaTime;

                //Debug.Log(deltaTime);
                if(1f <= deltaTime)
                {
                    deltaTime -= 1f;
                    //남은 시간 1 줄이기
                    RemainRechargingTime -= 1;

                    //0이되면 45로 초기화
                    if(RemainRechargingTime < 0){
                        //최대 충전 시간 남은시간에 넣기
                        RemainRechargingTime = MaxRechargingTime;

                        //업데이트
                        UpdateEnergy(RechargingPoint);
                    }
                }
                
                yield return WFF;
            }
        }

        /// <summary>
        /// 에너지 값의 변화 적용
        /// </summary>
        /// <param name="value"></param>
        public void UpdateEnergy(int value)
        {
            //현재 에너지 증가
            CurrentEnergy += value;

            //최대 에너지 보다 클수 없다
            if (MaxEnergy < CurrentEnergy) { CurrentEnergy = MaxEnergy; }

            //0보다 작을수 없다
            if (CurrentEnergy < 0) { CurrentEnergy = 0; }
                
            //업데이트 정보 저장 필요
            Save();
        }

        /// <summary>
        /// 최소 실행시 플레이어 이전 데이터를 로드해서 실행
        /// </summary>
        private void Load()
        {
            MaxRechargingTime = 45;
            RemainRechargingTime = MaxRechargingTime;
            RechargingPoint = 1;
            MaxEnergy = 100;
            //나중에 변경 되어야 함
            CurrentEnergy = MaxEnergy;
        }

        /// <summary>
        /// 에너지 변화 있을시 저장
        /// </summary>
        private void Save()
        {

        }
    }
}
