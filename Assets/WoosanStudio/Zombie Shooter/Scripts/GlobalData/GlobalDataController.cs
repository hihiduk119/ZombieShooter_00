﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary> 
    /// 공통으로 공유하는 데이터
    /// </summary>
    public class GlobalDataController : MonoBehaviour
    {
        //싱글톤 패턴
        static public GlobalDataController Instance;

        //*CardSetting.CardType 이넘과 모델 리스트의 순서는 동일해야 한다.
        public enum ModelType
        {
            BusinessMan = 0,
            FireFighter,
            Hobo,
            Pimp,
            Policeman,
            Prostitute,
            Punk,
            RiotCop,
            RoadWorker,
            Robber,
            Sheriff,
            StreetMan,
            Trucker,
            Woman,
        }

        //*CardSetting.CardType 이넘과 모델 리스트의 순서는 동일해야 한다.
        public enum GunType
        {
            Pistol = 0,             //무기를 권총으로 변경 및 해당 무기 데미지 25%증가
            Shotgun,            //무기를 샷건으로 변경 및 해당 무기 데미지 25%증가   lv 4 unlock
            AssaultRifle,       //무기를 돌격소총으로 변경 및 무기 데미지 25%증가    lv 8 unlock  
            SniperRifle,        //무기를 스나이퍼소총으로 변경 및 무기 데미지 25%증가 lv 12 unlock
        }

        //*CardSetting.CardType 이넘과 모델 리스트의 순서는 동일해야 한다.
        public enum AmmoType
        {
            BulletAmmo = 0,         //총알타입탄약 변경 및 총알타입탄약 데미지 25% 증가
            LaserAmmo,          //레이저타입탄약 변경 및 레이저타입탄약 데미지 25% 증가        lv 10 unlock
            PlasmaAmmo,         //플라즈마타입탄약 변경 및 플라즈마타입탄약 데미지 25% 증가       lv 14 unlock
        }

        //현재 도박 성공률
        private int gambleCurrentSuccessRate;
        public int GambleCurrentSuccessRate
        {
            get => gambleCurrentSuccessRate; set {
                data.GambleCurrentSuccessRate = gambleCurrentSuccessRate = value;
                Save();
            }
        }

        //기본 비지니스맨 선택됨
        private int selectedCharacter;
        public int SelectedCharacter
        {
            get => selectedCharacter; set {
                data.SelectedCharacter = selectedCharacter = value;
                Save();
            }
        }

        //기본 총알 선택됨
        private int selectedAmmo;
        public int SelectedAmmo
        {
            get => selectedAmmo; set
            {
                data.SelectedAmmo = selectedAmmo = value;
                Save();
            }
        }

        //기본 권총 선택됨
        private int selectedGun;
        public int SelectedGun
        {
            get => selectedGun; set
            {
                data.SelectedGun = selectedGun = value;
                Save();
            }
        }

        

        //겜블시 소모되는 젬
        static public int GambleGem = 10;
        //겜블 성공시 추가되는 성공률
        static public int GamblePerValue = 5;
        //겜블 최대 성공률
        static public int GambleMaxRate = 80;
        //겜블 기본 성공률
        static public int GambleDefaultRate = 50;
        //카드 순서에 의해 발생한 캐릭터 카드 시작 인덱스
        //*card 추가되면 모두 변경 되어야 한다
        static public int GunCardStartIndex = 9;
        //카드 순서에 의해 발생한 캐릭터 카드 시작 인덱스
        //*card 추가되면 모두 변경 되어야 한다
        static public int AmmoCardStartIndex = 13;
        //카드 순서에 의해 발생한 캐릭터 카드 시작 인덱스
        //*card 추가되면 모두 변경 되어야 한다
        static public int CharacterCardStartIndex = 16;
        //모든 총의 기본 치명타율 5%
        static public int DefaultCriticalChance = 5;
        //알림 통지 기준이 되는 씬이름
        static public string PresentSceneName = "1.ZombieShooter-Robby";

        //도박 성공률은 여기서 계산해서 data넣어 저장 시킨다
        //*계산은 아래 계산기 이용해서 결과 결고는 data.GambleCurrentSuccessRate 에 넣기
        //NextValueCalculator.GetGambleSuccessRate(0)

        public void Awake()
        {
            //싱글톤 패턴
            Instance = this;
            //데이터 로드
            Load();
            //삭제 안함
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Json데이터 저장
        /// </summary>
        void Save()
        {
            //Debug.Log("[Save] 글로벌 컨트롤러");
            string strJson = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("GlobalData", strJson);
        }

        /// <summary>
        /// Json데이터 로드
        /// </summary>
        void Load()
        {
            //Debug.Log("[Load] 글로벌 컨트롤러");
            if (!PlayerPrefs.HasKey("GlobalData")) { Save(); }

            data = JsonUtility.FromJson<GlobalDataController.Data>(PlayerPrefs.GetString("GlobalData"));

            //로드후에 반드시 싱크 마춰야 함
            Synchronization();
        }

        /// <summary>
        /// 저장 되어야 하는 데이터
        /// UICardModel.CardSetting 의 순서와 같다
        /// CardSetting.CardType역시 같은 순서이다.
        /// </summary>
        [System.Serializable]
        public class Data
        {
            //현재 도박 성공률
            public int GambleCurrentSuccessRate = 50;
            //기본 권총 선택됨
            //*card 추가되면 모두 변경 되어야 한다
            public int SelectedGun = 9;
            //기본 총알 선택됨
            //*card 추가되면 모두 변경 되어야 한다
            public int SelectedAmmo = 13;
            //기본 비지니스맨 선택됨
            //*card 추가되면 모두 변경 되어야 한다
            public int SelectedCharacter = 16;

            public Data() { }
        }
        public Data data = new Data();

        /// <summary>
        /// 데이터와 싱크 마춤
        /// </summary>
        void Synchronization()
        {
            //이떼 불필요한 Save발생을 피하기 위해 로컬 데이터 사용
            this.gambleCurrentSuccessRate = data.GambleCurrentSuccessRate;
            this.selectedCharacter = data.SelectedCharacter;
            this.selectedAmmo = data.SelectedAmmo;
            this.selectedGun = data.SelectedGun;
        }
    }
}
