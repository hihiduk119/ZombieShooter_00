using UnityEngine;
using System.Collections.Generic;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터의 고유 데이터
    /// 몬스터 프리팹
    /// 몬스터 레벨
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/MonsterSettings/Make Setting", fileName = "MonsterData")]
    public class MonsterSettings : ScriptableObject
    {
        /// <summary>
        /// 몬스터 고유 ID
        /// </summary>
        [SerializeField]
        public enum MonsterID
        {
            WeakZombie = 0,
            ThrowZombie,
            RunnerZombie,
        }

        /// <summary>
        /// 몬스터 아이디
        /// </summary>
        [Header("[고유 아이디]")]
        [SerializeField] private MonsterID _monsterID = 0;
        public MonsterID MonsterId { get => _monsterID; set => _monsterID = value; }

        /// <summary>
        /// 몬스터 이름
        /// </summary>
        [Header("[몬스터 이름]")]
        [SerializeField] private string _name = "None";

        /// <summary>
        /// 몬스터 프리팹
        /// </summary>
        //[Tooltip("몬스터 프리팹")]
        [SerializeField] public GameObject _prefab;

        /// <summary>
        /// 몬스터의 레벨
        /// </summary>
        [Header("[몬스터의 레벨]")]
        [SerializeField] private int _level = 0;
        public int Level { get => _level; }

        /// <summary>
        /// 체력
        /// </summary>
        [Header("[기본 체력 => [레벨에 따라 변경됨]]")]
        [SerializeField] private int _health;
        public int Health { get => _health = HealthCalculator.GetHealth((int)_monsterID, _level, _monsterID, true); }
        public HealthCalculator HealthCalculator { get; }

        [Header("[레벨에 따른 체력 공식]")]
        [SerializeField]
        private string healthFormula = "MonsterHealth";
        public string HealthFormula { get => healthFormula; }

        /// <summary>
        /// 공격 데미지
        /// </summary>
        [Header("[기본 공격력 => [레벨에 따라 변경됨]]")]
        [SerializeField] private int _damage;
        public int Damage { get => _damage = DamageCalculator.GetDamage((int)_monsterID, _level, _monsterID, true); }
        public DamageCalculator DamageCalculator { get; }

        [Header("[레벨에 따른 공격력 공식]")]
        [SerializeField]
        private string damageFormula = "MonsterDamage";
        public string DamageFormula { get => damageFormula; }


        //몬스터 레벨에 따라 저항 갯수 증가 필요.
        [Header("[해당 몬스터가 가진 프로퍼티]")]
        [SerializeField]
        private List<CardProperty> _propertys = new List<CardProperty>();
        public List<CardProperty> Propertys { get => _propertys; }

     
        /// <summary>
        /// 공격 딜레이
        /// </summary>
        [Header("[공격과 다음 공격 간 딜레이]")]
        [SerializeField] private float _attackDelay = 2;
        public float AttackDelay { get => _attackDelay; }

        /// <summary>
        /// 이동 속도
        /// </summary>
        [Header("[이동 속도]")]
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed { get => _moveSpeed; }

        /// <summary>
        /// 회전 속도
        /// </summary>
        [Header("[회전 속도]")]
        [SerializeField] private float turnSpeed = 130f;
        public float TurnSpeed { get => turnSpeed; }

        /// <summary>
        /// 정지 거리
        /// </summary>
        [Header("[최소 공격 거리]")]
        [SerializeField] private float stoppingDistance = 3f;
        public float StopingDistance { get => stoppingDistance; }

        /// <summary>
        /// 장거리 공격 몬스터의 발사체 세팅
        /// </summary>
        [SerializeField]
        private ProjectileSettings projectileSettings;
        public ProjectileSettings ProjectileSettings { get => projectileSettings; }

        /// <summary>
        /// 모델을 만듬
        /// </summary>
        /// <param name="name">생성할 이름</param>
        /// <returns></returns>
        public GameObject MakeModel(string name = "", Transform parent = null)
        {
            GameObject clone = null;

            if (_prefab != null)
            {
                clone = Instantiate(_prefab, parent.position, Quaternion.identity) as GameObject;
                if (name.Length > 0)
                    clone.name = name;
                else
                    clone.name = _prefab.name + " (clone)";
            }
            else
            {
                Debug.Log("[Error] Monster Prefab is NULL !!");
            }

            return clone;
        }

        /// <summary>
        /// 그림자 사용 여부
        /// </summary>
        [Header("그림자 사용")]
        [SerializeField] private bool _useShadow;
        public bool UseShadow { get => _useShadow; }

        /// <summary>
        /// 그림자 사용 여부
        /// </summary>
        [Header("그림자 사용")]
        [SerializeField] private GameObject _shadowProjector;
        public GameObject ShadowProejector { get => _shadowProjector;  }
    }
}