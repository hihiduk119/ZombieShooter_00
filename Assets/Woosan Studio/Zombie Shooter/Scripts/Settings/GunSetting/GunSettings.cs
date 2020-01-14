using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/GunSettings/Make Setting", fileName = "GunData")]
    public class GunSettings : ScriptableObject , IHaveModel , IGunStat , IWeaponStat
    {
        /// <summary>
        /// 무기 타입 - 추가 될 경우를 대비해 int로 만듬
        /// </summary>
        [SerializeField] private int _index = 0;

        /// <summary>
        /// 무기 이름
        /// </summary>
        [SerializeField] private string _name = "None";

        /// <summary>
        /// projectileActor.cs 에서 가져온 클래스
        /// </summary>
        [SerializeField] private ProjectileSettings _projectileSettings;
        public int Index { get => _index; }
        public string Name { get => _name; }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IHaveModel Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        /// <summary>
        /// 총기 모델 프리팹
        /// </summary>
        [SerializeField] private GameObject _prefab;
        /// <summary>
        /// 총기 모델 프리팹 인스턴스
        /// </summary>
        [SerializeField] [HideInInspector] private GameObject _prefabInstance;

        public GameObject Prefab { get { return MakeModel(); } }
        public GameObject PrefabInstance { get => _prefabInstance; }
        public ProjectileSettings ProjectileSettings { get => _projectileSettings; }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IWeaponStat Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        [SerializeField] private int _level = 1;
        public int Level { get => _level; }

        public int Type { get => _index; }

        [SerializeField] private int _damage;
        public int Damage { get => _damage = DamageCalculator.GetDamage(this); }
        public DamageCalculator DamageCalculator { get; }


        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IGunStat Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        [SerializeField] private int _maxAmmo = 10;
        public int MaxAmmo { get => _maxAmmo; }

        [SerializeField] private int _currentAmmo = 0;
        [HideInInspector] public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
        
        [SerializeField] private float _reloadTime = 2f;
        public float ReloadTime { get => _reloadTime = ReloadTimeCalculator.GetReloadTime(this); }
        public ReloadTimeCalculator ReloadTimeCalculator { get; }

        [SerializeField] private float _fireSpeed;
        public float FireSpeed { get => _fireSpeed = FireSpeedCalculator.GetFireSpeed(this);}
        public FireSpeedCalculator FireSpeedCalculator { get; }

        


        /// <summary>
        /// _prefab 을 사용하여 _prefabInstance에 인스턴스 만듬.
        /// </summary>
        /// <returns>_prefabInstance 리턴 </returns>
        public GameObject MakeModel()
        {
            if (_prefab != null)
            {
                _prefabInstance = Instantiate(_prefab) as GameObject;
                _prefabInstance.name = _name;
            }
            else
            {
                Debug.Log("[Error] Gun Prefab is NULL !!");
            }

            return _prefabInstance;
        }
    }
}
