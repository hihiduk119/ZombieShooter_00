using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/GunSettings/Make Setting", fileName = "GunData")]
    internal class GunSettings : ScriptableObject
    {
        /// <summary>
        /// 무기 타입 - 추가 될 경우를 대비해 int로 만듬
        /// </summary>
        [SerializeField] private int _type = 0;
        /// <summary>
        /// 기본 데미지
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private int _maxAmmo = 10;
        [SerializeField] private int _level = 1;
        [SerializeField] private float _reloadTime = 2f;

        /// <summary>
        /// 총기 모델 프리팹
        /// </summary>
        [SerializeField] private GameObject _model;

        /// <summary>
        /// projectileActor.cs 에서 가져온 클래
        /// </summary>
        [SerializeField] private WoosanStudio.ZombieShooter.ProjectileSettings _projectileSettings;
        public int Type { get => _type; }

        public int MaxAmmo { get => _maxAmmo; }
        public int Level { get => _level; }
        public float ReloadTime { get => _reloadTime; }
        public GameObject Model
        {
            get
            {
                if (_model == null) { _model = Instantiate(_model) as GameObject; }
                return _model;
            }
        }
    }
}
