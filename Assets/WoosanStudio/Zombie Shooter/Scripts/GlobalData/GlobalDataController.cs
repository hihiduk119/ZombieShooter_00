using System.Collections;
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
        //* 모든 계산된 값을 가져옴.
        private int gambleCurrentSuccessRate;
        public int GambleCurrentSuccessRate
        {
            get {
                //기본 갬블 40 + (콤보 카운트 * 콤보 값 )
                int successRate = GlobalDataController.GambleDefaultRate
                            + (GlobalDataController.Instance.GambleComboCount * GlobalDataController.GamblePerValue);

                //혹시라도 최대 성공 값보다 크다면 최대 값으로 초기화
                if (successRate > GlobalDataController.GambleMaxRate) { successRate = GlobalDataController.GambleMaxRate; }

                return successRate;
            }
            //set {
            //    data.GambleCurrentSuccessRate = gambleCurrentSuccessRate = value;
            //    Save();
            //}
        }

        //현재 도박 연속 성공 카운트
        private int gambleComboCount;
        public int GambleComboCount
        {
            get => gambleComboCount; set
            {
                data.GambleComboCount = gambleComboCount = value;
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

        //연구 가능 최대 슬롯 (구매되어 열린)
        private int useUpgradeAbleSlotCount;
        public int UseUpgradeAbleSlotCount
        {
            get => useUpgradeAbleSlotCount; set
            {
                data.UseUpgradeAbleSlotCount = useUpgradeAbleSlotCount = value;
                Save();
            }
        }

        //선택된 맵
        private int selectedMap;
        public int SelectedMap
        {
            get => selectedMap; set
            {
                data.SelectedMap = selectedMap = value;
                Save();
            }
        }

        //선택된 스테이지
        //*지금 사용 안함
        private int selectedStage;
        public int SelectedStage
        {
            get => selectedStage; set
            {
                data.SelectedStage = selectedStage = value;
                Save();
            }
        }


        //겜블시 소모되는 젬
        static public int GambleGem = 10;
        //겜블 코보 1당 추가되는 성공률
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
        static public float DefaultCriticalChance = 0;
        //모든 총의 기본 치명타율 데미지 110%
        static public int DefaultCriticalDamage = 110;
        //알림 통지 기준이 되는 씬이름
        static public string PresentSceneName = "1.ZombieShooter-Robby";
        //업그레이드 가능한 연구 가능 최대 슬롯
        static public int MaxUpgradeSlotCount = 3;
        //In-Game 에서 라운드 종료후 카드 선택 화면에서 카드 추가 가격
        static public int[] CardAddPrices = {1000,1500,3000};

        [Header("[[씬에서 쓰고 버리는 데이터] 중간 카드 선택시 선택 가능한 모든 카드]")]
        public List<CardSetting> SelectAbleAllCard = new List<CardSetting>();
        [Header("[[씬에서 쓰고 버리는 데이터] 중간 카드 선택시 선택 가능한 캐릭터 카드]")]
        public List<CardSetting> SelectAbleCharacterCard = new List<CardSetting>();
        [Header("[[씬에서 쓰고 버리는 데이터] 중간 카드 선택시 선택 가능한 무기 카드]")]
        public List<CardSetting> SelectAbleWeaponCard = new List<CardSetting>();
        [Header("[[씬에서 쓰고 버리는 데이터] 중간 카드 선택시 선택 가능한 탄약 카드]")]
        public List<CardSetting> SelectAbleAmmoCard = new List<CardSetting>();
        //[Header("[[씬에서 쓰고 버리는 데이터] 중간 카드 선택시 선택된 모든 카드]")]
        //public List<CardSetting> SelectedCard = new List<CardSetting>();

        //로비에서 선택된 캐릭터, 무기 , 탄약
        static public CardSetting SelectedCharacterCard;
        static public CardSetting SelectedWeaponCard;
        static public CardSetting SelectedAmmoCard;
        //선택된 맵 세팅 데이터
        static public Map.Setting MapSetting;

        //게임중 선택한 건세팅의 베이스 [퓨어상태]
        static public GunSettings SelectedBaseGunSetting;
        //게임중에 사용하는 건세팅 [총 교체시 같이 교체됨]
        //*생성 세팅시 카드 데이터를 반영해야함.
        private GunSettings selectedGunSetting;
        public GunSettings SelectedGunSetting { get => selectedGunSetting; set => selectedGunSetting = value; }

        //공습 채움 기본 값
        static public float AirStrikeRechargingValue = 1f;

        //공습 기본 데미지
        static public int AirStrikeDamge = 300;

        //로비에서 선택된 라운드 
        static public int SelectRound = 0;

        //스테이지 시작시 기본으로 소모되는 에너지
        static public int DefaultConsumeEnergy = 10;

        //라운드당 추가 소모되는 에너지
        static public int ConsumeEnergyByRound = 1;

        //에너지 부족 상태로 시작
        static public bool NoEnergyStart = false;

        //에너지 부족 시작시 기본 비율 -> %임
        static public int NoEnergyStartHealthPointRate = 10;

        //플레이어의 기본 체력
        static public int PlayerHealth = 50;

        //플레이어의 부활 횟수
        static public int ResurrectionCount = 0;

        //실제 플레이어 트랜스폼
        static public Transform Player;

        //현재 진행중인 라운드
        //*MonsterSpawnScheduleManager.cs 에서 카운팅 시킴
        static public int CurrentRound = 0;

        //플레이어 레벨
        //*ExpModel.cs Load()애서 세팅
        static public int PlayerLevel = 0;

        /// <summary>
        /// 소모되는 에너지 계산해서 가져오기
        /// </summary>
        /// <returns></returns>
        static public int GetConsumeEnergy(int baseVaue,int waveValue,int waveCount)
        {
            int value = baseVaue + (waveValue * waveCount);

            return value;
        }

        /// <summary>
        /// 카드에 의한 건 데이터 업데이트 반영
        /// *변수 1.공속 2.최대 탄약수
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="ammoCard"></param>
        /// <param name="allCard"></param>
        /// <returns></returns>
        public GunSettings UpdateGunSettingByCards(GunSettings origin,GunSettings target,CardSetting ammoCard,List<CardSetting> allCard)
        {
            //*카드의 프로퍼티를 찾아서 적용해야한다.
            //1. all card에서 공속프로퍼티 찾기.
            //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>> 기본 공속 = " + origin.rapidFireCooldown);
            //2. 공속 카드 오리진 세팅값에 비교해서 타겟 세팅에 적용
            float rapidFireCooldown = DamageCalculator.Instance.GetAttackSpeed(origin.rapidFireCooldown);
            //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>> 변경 공속 = " + rapidFireCooldown);
            //새로운 공속 적용.
            target.rapidFireCooldown = rapidFireCooldown;
            //3. 탄약 카드 값을 반영하여 타겟 카드 세팅에 적용.
            //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>> 기본 최대 탄약 = " + origin.MaxAmmo);
            int maxAmmo = DamageCalculator.Instance.GetMaxAmmo(origin.MaxAmmo);
            //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>> 변경 최대 탄약 = " + maxAmmo);
            //최데 탄약 새로 새팅
            target.MaxAmmo = maxAmmo;

            return target;
        }

        public void Awake()
        {
            //데이터 로드
            Load();

            if (null == Instance)
            {
                //싱글톤 패턴
                Instance = this;

                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
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
            //public int GambleCurrentSuccessRate = 40;
            //현재 도박 연속 성공 카운트
            public int GambleComboCount = 0;
            //기본 권총 선택됨
            //*card 추가되면 모두 변경 되어야 한다
            public int SelectedGun = 9;
            //기본 총알 선택됨
            //*card 추가되면 모두 변경 되어야 한다
            public int SelectedAmmo = 13;
            //기본 비지니스맨 선택됨
            //*card 추가되면 모두 변경 되어야 한다
            public int SelectedCharacter = 16;
            //연구 가능 최대 슬롯 (구매되어 열린)
            public int UseUpgradeAbleSlotCount = 1;
            //현재 연구 가능한 슬롯
            //public int CurrentUpgradeAbleSlotCount = 0;
            //기본 타운 맵 선택됨
            public int SelectedMap = 0;
            //기본 0 스테이지 선택됨
            public int SelectedStage = 0;

            public Data() { }
        }
        public Data data = new Data();

        /// <summary>
        /// 데이터와 싱크 마춤
        /// </summary>
        void Synchronization()
        {
            //이떼 불필요한 Save발생을 피하기 위해 로컬 데이터 사용
            //this.gambleCurrentSuccessRate = data.GambleCurrentSuccessRate;
            this.gambleComboCount = data.GambleComboCount;
            this.selectedCharacter = data.SelectedCharacter;
            this.selectedAmmo = data.SelectedAmmo;
            this.selectedGun = data.SelectedGun;
            this.useUpgradeAbleSlotCount = data.UseUpgradeAbleSlotCount;
            //this.currentUpgradeAbleSlotCount = data.CurrentUpgradeAbleSlotCount;
            this.selectedMap = data.SelectedMap;
            this.selectedStage = data.SelectedStage;
        }

        /// <summary>
        /// 슬롯 사용 가능한지 여부확인
        /// </summary>
        /// <returns></returns>
        static public bool CanSlot()
        {
            //사용가능한 슬롯이 없다.
            if (GlobalDataController.MaxUpgradeSlotCount <= GlobalDataController.Instance.UseUpgradeAbleSlotCount)
            {
                //Debug.Log("슬롯 없다");
                return false;
            }
            else
            {
                //Debug.Log("슬롯 있다 -> MaxUpgradeSlotCount = [" + GlobalDataController.MaxUpgradeSlotCount + "]  UseUpgradeAbleSlotCount = [" + GlobalDataController.Instance.UseUpgradeAbleSlotCount + "]");
                return true;
            }
        }

        /// <summary>
        /// 가지고 있는 카드 확인용
        /// </summary>
        public void HasCardsPrint()
        {
            Debug.Log("=========== Common Cards =========");
            SelectAbleAllCard.ForEach(value => Debug.Log("[" + value.Type.ToString() + "]"));

            Debug.Log("=========== Character Cards =========");
            SelectAbleCharacterCard.ForEach(value => Debug.Log("[" + value.Type.ToString() + "]"));

            Debug.Log("=========== Weapon Cards =========");
            SelectAbleWeaponCard.ForEach(value => Debug.Log("[" + value.Type.ToString() + "]"));

            Debug.Log("=========== Ammo Cards =========");
            SelectAbleAmmoCard.ForEach(value => Debug.Log("[" + value.Type.ToString() + "]"));

            Debug.Log("=========== Map =========");

            Debug.Log("선택된 맵이름 = "+ GlobalDataController.MapSetting.Name);
        }



        /// <summary>
        /// 최대 스택이 아닌 6장의 카드 가져오기
        /// </summary>
        public List<CardSetting> GetSixCardExcludingNotMaxStack()
        {
            //일단 참조 복제
            List<CardSetting> cardSettings = new List<CardSetting>();

            //최대 스택은 제외함
            GlobalDataController.Instance.SelectAbleAllCard.ForEach(value =>
            {
                if (value.StackCount < value.MaxStack)
                {
                    cardSettings.Add(value);
                }
            });
            //Debug.Log("1 갯수 = " + cardSettings.Count);

            //랜덤하게 섞기
            cardSettings.Shuffle<CardSetting>();
            //Debug.Log("2 갯수 = " + cardSettings.Count);

            //카드가 6장 이상일때 6개만 남기고 모두 버림
            if (6 < cardSettings.Count)
            { 
                cardSettings.RemoveRange(6, cardSettings.Count - 6);
            }

            //Debug.Log("3 갯수 = " + cardSettings.Count);

            return cardSettings;
        }

        //void OnGUI()
        //{
        //    if (GUI.Button(new Rect(100, 1300, 200, 100), "맵 데이터 확인"))
        //    {
        //        HasCardsPrint();
        //    }
        //}

        /*#region [-TestCode]
        void Update()
        {
            //6개 카드 가져오기 테스트
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GetSixCardExcludingNotMaxStack();
            }
        }
        #endregion*/
    }

    /// <summary>
    /// 확장 메서드
    /// </summary>
    public static class ListExtention
    {
        /// <summary>
        /// 리스트 랜덤 섞기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        static public void Shuffle<T>(this IList<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
