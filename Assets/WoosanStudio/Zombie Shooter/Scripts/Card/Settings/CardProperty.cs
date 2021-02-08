using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 자체의 프로퍼티
    /// 프로퍼티의 초기값 및 레벨당 증가 수치 및 설명 등이 있음
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/CardPropertySettings/Make Setting", fileName = "Property")]
    [System.Serializable]
    public class CardProperty : ScriptableObject, IProperty
    {
        [Header("[프로퍼티 타입]")]
        [SerializeField]
        private PropertyType type;
        public PropertyType Type => type;

        [Header("[기본 프로퍼티 초기 값]")]
        [SerializeField]
        private int value;
        public int Value => value;

        [Header("[타이틀]")]
        [SerializeField]
        private string title;
        public string Title => title;

        [Header("[사용 아이콘]")]
        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;

        [Header("카드 레벨이 반영된 값 => 프로퍼티 계산에 사용]")]
        [SerializeField]
        private int valueByLevel;
        public int ValueByLevel { get => valueByLevel; set => valueByLevel = value; }

        [Header("[계산 공식 => [PropertyType과 같음]]")]
        [SerializeField]
        private string fomula;
        public string Fomula => fomula;

        [Header("[레벨 1업당 증가 수치]")]
        [SerializeField]
        private float increasedValuePerLevelUp;
        public float IncreasedValuePerLevelUp => increasedValuePerLevelUp;

        [Header("[해당 속성 설명]")]
        [SerializeField][TextArea(5, 8)]
        private string descripsion;
        public string Descripsion => descripsion;

        private StringBuilder stringBuilder = new StringBuilder();

        [System.Serializable]
        public enum PropertyType
        {
            AttackSpeed = 0,                //공격 속도
            MagazineCapacity,               //탄창 용량
            CriticalDamage,                 //치명 데미지
            CriticalChance,                 //치명 기회
            MaxHP,                          //최대 체력
            AirStrikeDamage,                //공습 데미지
            AngerRecharge,                  //분노 체움 속도
            Coin,                           //획득 돈
            Exp,                            //획득 경험치

            ChangeCharacter,                //캐릭터 변경
            ChangeWeapon,                   //무기 변경
            ChangeAmmoType,                 //탄약 타입 변경
            NamedZombieDamage,              //네임드 좀비 데미지
            GeneralZombieDamage,            //일반 좀비 데미지

            PistolDamage = 100,             //권총 데미지    (권총 샷건 돌격 스나이퍼 모두 결국 데미지 업 )
            ShotgunDamage,                  //샷건 데미지
            AssaultRifleDamage,             //돌격 소총 데미지
            SniperRifleDamage,              //스나이퍼소총 데미지
            AllWeaponDamage = 199,          //모든 무기 데미지 => 각각 계산으로 바꿔야함

            BulletAmmoDamage = 200,         //총알타입탄약 데미지
            LaserAmmoDamage,                //레이저타입탄약 데미지
            PlasmaAmmoDamage,               //플라즈마타입탄약 데미지
            AllAmmoDamage = 299,            //모든 탄약 데미지 => 각각 계산으로 바꿔야함

            //몬스터가 가지는 프로퍼티
            PistolDamageResistance = 400,   //권총 데미지 저항
            ShotgunDamageResistance,        //샷건 데미지 저항
            AssaultRifleDamageResistance,   //돌격 소총 데미지 저항
            SniperRifleDamageResistance,    //스나이퍼소총 데미지 저항
            AllWeaponDamageResistance = 499,//모든 무기 데미지 저항
            BulletAmmoDamageResistance = 500,//총알타입탄약 데미지 저항
            LaserAmmoDamageResistance,      //레이저타입탄약 데미지 저항
            PlasmaAmmoDamageResistance,     //플라즈마타입탄약 데미지 저항
            AllAmmoDamageResistance = 599,  //모든 탄약 데미지 저항
            
            AirStrikeDamageResistance = 600,      //공습 데미지 저항
            //CriticalDamageResistance,         //치명타 데미지 저항
        }

        /// <summary>
        /// 완성된 설명 가져오기
        /// *CardRewardSelectPopupController -> ChangeCardInformation() 부분 변경가능
        /// </summary>
        /// <param name="cardLevel"></param>
        /// <returns></returns>
        public string GetCompletedDescripsion(int cardLevel)
        {
            //일단 초기화하고 시작
            stringBuilder.Clear();

            string desc = descripsion;

            //"/d"의 첫번째 /를 알아옴
            int index = desc.IndexOf('/');

            if (desc[index + 1].Equals('d'))
            {
                //isFind = true;
                //기본 값 + 카드 1레벨당 상승 값 * 카드 레벨 
                float value = (cardLevel * IncreasedValuePerLevelUp) + Value;

                //'/d'를 삭제
                desc = desc.Remove(index, 2);
                //삭제된 위치에 계산됨 값 넣기 => 소수점 1자리만 표기
                desc = desc.Insert(index, string.Format("{0:0.0}", value.ToString()));
            }

            //계산 완료후 완성된 문장 한줄을 더함.
            stringBuilder.Append(desc);

            return stringBuilder.ToString();
        }


        /// <summary>
        /// 업그레이드 정보용 완성된 설명 가져오기
        /// *CardRewardSelectPopupController -> ChangeCardInformation() 부분 변경가능
        /// </summary>
        /// <param name="cardLevel"></param>
        /// <returns></returns>
        public string GetCompletedDescripsionForUpgradeInfo(int cardLevel)
        {
            //일단 초기화하고 시작
            stringBuilder.Clear();

            string desc = descripsion;

            //"/d"의 첫번째 /를 알아옴
            int index = desc.IndexOf('/');

            //'d'와 같은 수치 값이 존재하는 설명이라면
            bool valueIsCanBeChanged = false;

            float value;
            if (desc[index + 1].Equals('d'))
            {
                valueIsCanBeChanged = true;
                //isFind = true;
                //기본 값 + 카드 1레벨당 상승 값 * 카드 레벨 
                value = (cardLevel * IncreasedValuePerLevelUp) + Value;

                //'/d'를 삭제
                desc = desc.Remove(index, 2);
                //삭제된 위치에 계산됨 값 넣기 => 소수점 1자리만 표기
                desc = desc.Insert(index, string.Format("{0:0.0}", value.ToString()));
            }

            //계산 완료후 완성된 문장 한줄을 더함.
            stringBuilder.Append(desc);

            //'d'와 같은 수치 값이 존재하는 설명이라면
            //증가 수치 추가로 붙이기
            if (valueIsCanBeChanged)
            {
                //다음 레벨 증가 수치 추가
                stringBuilder.Append(" -> ");
                value = ((cardLevel + 1) * IncreasedValuePerLevelUp) + Value;
                stringBuilder.Append(value);
                stringBuilder.Append("%");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 레벨이 반영된 값 가져오기
        /// *테스트 필요
        /// </summary>
        /// <param name="cardLevel"></param>
        /// <returns></returns>
        public int GetValueByUpdatedLevel(int cardLevel)
        {
            return Mathf.RoundToInt(cardLevel * IncreasedValuePerLevelUp) + Value;
        }


        /// <summary>
        /// 업그레이드 정보용 완성된 설명 가져오기
        /// *CardRewardSelectPopupController -> ChangeCardInformation() 부분 변경가능
        /// </summary>
        /// <param name="cardLevel"></param>
        /// <returns></returns>
        public string GetSelectedCardDescripsion(int cardLevel,int stack)
        {
            //계산 시에는 스택 +1로 계산 -> 증가 수치 표시용
            //*이거 문제 발생
            //stack += 1;

            //일단 초기화하고 시작
            stringBuilder.Clear();

            string desc = descripsion;

            //"/d"의 첫번째 /를 알아옴
            int index = desc.IndexOf('/');

            //'d'와 같은 수치 값이 존재하는 설명이라면
            bool valueIsCanBeChanged = false;

            float value;
            if (desc[index + 1].Equals('d'))
            {
                valueIsCanBeChanged = true;

                //Value가 (-)일 경우 감소 프로퍼티임.
                //*이때는 스택 계산 안함
                if (this.Value < 0)
                {
                    value = (cardLevel * IncreasedValuePerLevelUp) + Value;
                }
                else
                {
                    //기본 값 + 카드 1레벨당 상승 값 * 카드 레벨 
                    value = ((cardLevel * IncreasedValuePerLevelUp) + Value) * stack;
                }

                //'/d'를 삭제
                desc = desc.Remove(index, 2);
                //삭제된 위치에 계산됨 값 넣기 => 소수점 1자리만 표기
                desc = desc.Insert(index, string.Format("{0:0.0}", value.ToString()));
            }

            //계산 완료후 완성된 문장 한줄을 더함.
            stringBuilder.Append(desc);

            //'d'와 같은 수치 값이 존재하는 설명이라면
            //증가 수치 추가로 붙이기
            //Value가 (-)일 경우 변경 표시 안함
            if (valueIsCanBeChanged && (0 <= this.Value ))
            {
                //다음 레벨 증가 수치 추가
                stringBuilder.Append(" -> ");

                //Value가 (-)일 경우 감소 프로퍼티임.
                //*이때는 스택 계산 안함
                //if (this.Value < 0)
                //{
                //    value = (cardLevel * IncreasedValuePerLevelUp) + Value;
                //} else
                //{
                    value = ((cardLevel * IncreasedValuePerLevelUp) + Value) * (stack + 1);
                //}
                
                stringBuilder.Append(value);
                stringBuilder.Append("%");
            }

            return stringBuilder.ToString();
        }
    }
}
