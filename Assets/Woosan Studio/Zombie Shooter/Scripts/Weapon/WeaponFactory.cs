using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class WeaponFactory : MonoBehaviour
    {
        public List<GunSettings> _gunSettings;
        GameObject _weapon;
        IProjectileLauncher _projectileLauncher;

        public IWeapon MakeWeapon(Transform parant,int type)
        {
            //어떤 무기는 모델을 가지고 있으면 IHaveModel인터페이스를 상속 받기에 해당 인터페이스 호출.
            IHaveModel haveModel = _gunSettings[type];

            //모델 인스턴스 생성 및 가져오기
            _weapon = haveModel.MakeModel();
            //인스턴스 부모 설정
            _weapon.transform.parent = parant;


            switch (type)
            {
                case 0:
                    _projectileLauncher = _weapon.AddComponent<Pistol>();
                    break;
                case 1:
                    _projectileLauncher = _weapon.AddComponent<AssaultRifle>();
                    break;
                case 2:
                    _projectileLauncher = _weapon.AddComponent<Shotgun>();
                    break;
                case 3:
                    _projectileLauncher = _weapon.AddComponent<LaserRifle>();
                    break;
            }

            //발사체 무기는 IProjectileLauncher 를 상속 받기 때문에 인터페이스 호출
            if (_projectileLauncher != null)
            {
                _projectileLauncher.ProjectileLauncher = _weapon.AddComponent<ProjectileLauncher>();
                _projectileLauncher.GunSettings = _gunSettings[type];
                _projectileLauncher.ProjectileLauncher.bombList = _projectileLauncher.GunSettings.ProjectileSettings;
            }

            return (IWeapon)_weapon.GetComponent<Pistol>();
        }
    }
}