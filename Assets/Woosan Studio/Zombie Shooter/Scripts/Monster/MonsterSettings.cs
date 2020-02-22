using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/MonsterSettings/Make Setting", fileName = "MonsterData")]
    public class MonsterSettings : ScriptableObject
    {
        /// <summary>
        /// 공격타입을 정의
        /// </summary>
        [SerializeField] 
        public enum AttackType
        {
            Melee = 0,
            Throwing,
        }

        /// <summary>
        /// 공격 타입
        /// </summary>
        [Tooltip("공격 타입")]
        [SerializeField] private AttackType _attackType = 0;

        /// <summary>
        /// 고유 아이디
        /// </summary>
        [Tooltip("고유 아이디")]
        [SerializeField] private int _id = 0;

        /// <summary>
        /// 몬스터 이름
        /// </summary>
        [Tooltip("몬스터 이름")]
        [SerializeField] private string _name = "None";

        /// <summary>
        /// 몬스터 프리팹
        /// </summary>
        [Tooltip("몬스터 프리팹")]
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
        public int Health { get => _health = HealthCalculator.GetHealth(_id, _level, _attackType, true); }
        public HealthCalculator HealthCalculator { get; }


        /// <summary>
        /// 공격 데미지
        /// </summary>
        [Tooltip("공격 데미지")]
        [SerializeField] private int _damage;
        public int Damage { get => _damage = DamageCalculator.GetDamage(_id,_level, _attackType, true); }
        public DamageCalculator DamageCalculator { get; }

        /// <summary>
        /// 공격 속도
        /// </summary>
        [Tooltip("공격 속도")]
        [SerializeField] private float _attackSpeed;
        public float AttackSpeed { get => _attackSpeed; }

        /// <summary>
        /// 이동 속도
        /// </summary>
        [Tooltip("이동 속도")]
        [SerializeField] private float _moveSpeed;
        public float MoveSpeed { get => _moveSpeed; }

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
                clone = Instantiate(_prefab, parent.position,Quaternion.identity) as GameObject;
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
    }
}