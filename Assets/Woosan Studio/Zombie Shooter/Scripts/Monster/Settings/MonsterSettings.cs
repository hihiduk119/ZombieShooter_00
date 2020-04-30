using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/MonsterSettings/Make Setting", fileName = "MonsterData")]
    public class MonsterSettings : ScriptableObject
    {
        /// <summary>
        /// 공격타입을 정의
        /// </summary>
        /*[SerializeField] 
        public enum AttackType
        {
            Melee = 0,
            Throwing,
        }*/

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
        /// 공격 타입
        /// </summary>
        /*[Tooltip("공격 타입")]
        [SerializeField] private AttackType _attackType = 0;
        */

        /// <summary>
        /// 몬스터 아이디
        /// </summary>
        [Tooltip("고유 아이디")]
        [SerializeField] private MonsterID _monsterID = 0;
        public MonsterID MonsterId { get => _monsterID; set => _monsterID = value; }

        /// <summary>
        /// 몬스터 이름
        /// </summary>
        [Tooltip("몬스터 이름")]
        [SerializeField] private string _name = "None";

        /// <summary>
        /// 몬스터 프리팹
        /// </summary>
        //[Tooltip("몬스터 프리팹")]
        [SerializeField] private GameObject _prefab;

        /// <summary>
        /// 몬스터의 레벨
        /// </summary>
        [Tooltip("몬스터의 레벨")]
        [SerializeField] private int _level = 1;
        public int Level { get => _level; }

        /// <summary>
        /// 체력
        /// </summary>
        [Tooltip("체력")]
        [SerializeField] private int _health;
        public int Health { get => _health = HealthCalculator.GetHealth((int)_monsterID, _level, _monsterID, true); }
        public HealthCalculator HealthCalculator { get; }


        /// <summary>
        /// 공격 데미지
        /// </summary>
        [Tooltip("공격 데미지")]
        [SerializeField] private int _damage;
        public int Damage { get => _damage = DamageCalculator.GetDamage((int)_monsterID, _level, _monsterID, true); }
        public DamageCalculator DamageCalculator { get; }

        /// <summary>
        /// 공격 딜레이
        /// </summary>
        [Tooltip("공격과 다음 공격 간 딜레이")]
        [SerializeField] private float _attackDelay = 2;
        public float AttackDelay { get => _attackDelay; }

        /// <summary>
        /// 공격실행 후 실제 공격 까지의 딜레이
        /// </summary>
        [Tooltip("공격실행 후 실제 공격 까지의 딜레이")]
        [SerializeField] private float hitDelay = 0.7f;
        public float HitDelay { get => hitDelay; }

        /// <summary>
        /// 이동 속도
        /// </summary>
        [Tooltip("이동 속도")]
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed { get => _moveSpeed; }

        /// <summary>
        /// 회전 속도
        /// </summary>
        [Tooltip("회전 속도")]
        [SerializeField] private float turnSpeed = 130f;
        public float TurnSpeed { get => turnSpeed; }

        /// <summary>
        /// 정지 거리
        /// </summary>
        [Tooltip("정지 거리")]
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
        [Tooltip("그림자 사용")]
        [SerializeField] private bool _useShadow;
        public bool UseShadow { get => _useShadow; }

        /// <summary>
        /// 그림자 사용 여부
        /// </summary>
        [Tooltip("그림자 사용")]
        [SerializeField] private GameObject _shadowProjector;
        public GameObject ShadowProejector { get => _shadowProjector;  }
    }
}