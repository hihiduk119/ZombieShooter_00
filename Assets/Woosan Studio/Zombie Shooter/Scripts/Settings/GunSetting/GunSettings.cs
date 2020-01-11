using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/GunSettings/Make Setting", fileName = "GunData")]
    public class GunSettings : ScriptableObject , IHaveModel
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
        /// 총의 기본 세팅값 (따로 인터페이스로 때어낼지 고민중)
        /// </summary>
        [SerializeField] private int _maxAmmo = 10;
        [SerializeField] private int _currentAmmo = 0;
        [SerializeField] private int _level = 1;
        [SerializeField] private float _reloadTime = 2f;

        /// <summary>
        /// 탄을 모두 소비하면 호출할 액션.
        /// </summary>
        [SerializeField] public UnityAction EmptyAmmoActionHandler { get; set; }

        /// <summary>
        /// 총기 모델 프리팹
        /// </summary>
        [SerializeField] private GameObject _prefab;

        /// <summary>
        /// 총기 모델 프리팹 인스턴스
        /// </summary>
        [SerializeField][HideInInspector] private GameObject _prefabInstance;

        /// <summary>
        /// projectileActor.cs 에서 가져온 클래
        /// </summary>
        [SerializeField] private ProjectileSettings _projectileSettings;
        public int Index { get => _index; }
        public string Name { get => _name; }
        public int MaxAmmo { get => _maxAmmo; }
        public int CurrentAmmo { get => _currentAmmo; }
        public int Level { get => _level; }
        public float ReloadTime { get => _reloadTime; }
        public GameObject Prefab { get { return MakeModel(); } }
        public GameObject PrefabInstance { get => _prefabInstance; }
        public ProjectileSettings ProjectileSettings { get => _projectileSettings; }


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

        /// <summary>
        /// 재장전
        /// </summary>
        public void ReloadAmmo()
        {
            _currentAmmo = _maxAmmo;
        }

        /// <summary>
        /// 탄 사용
        /// </summary>
        public void UseAmmo()
        {
            _currentAmmo--;

            if (_currentAmmo == 0) { EmptyAmmoActionHandler.Invoke(); }
        }
    }
}
