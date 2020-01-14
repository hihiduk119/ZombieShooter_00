using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class WeaponFactory : MonoBehaviour
    {
        //Gun 세팅값 
        public List<GunSettings> _gunSettings;

        //캐슁 데이타
        GameObject _weapon;
        IProjectileLauncher _projectileLauncher;
        IWeapon _iWeapon;

        /// <summary>
        /// 무기를 생성
        /// </summary>
        /// <param name="parant">생성될 오브젝트의 부모</param>
        /// <param name="type">생성할 무기의 인덱스</param>
        /// <param name="isUser">사용자가 유저인지 AI 인지 구분용</param>
        /// <returns></returns>
        public IWeapon MakeWeapon(Transform parant,int type,bool isUser)
        {
            //어떤 무기는 모델을 가지고 있으면 IHaveModel인터페이스를 상속 받기에 해당 인터페이스 호출.
            IHaveModel haveModel = _gunSettings[type];

            //모델 인스턴스 생성 및 가져오기
            _weapon = haveModel.MakeModel();
            //인스턴스 부모 설정
            _weapon.transform.parent = parant;

            //인덱스 역
            switch (type)
            {
                case 0:
                    //_projectileLauncher = _weapon.AddComponent<Pistol>();
                    _iWeapon = _weapon.AddComponent<Pistol>();
                    _projectileLauncher = _iWeapon.GetProjectileLauncher();
                    break;
                case 1:
                    //_projectileLauncher = _weapon.AddComponent<AssaultRifle>();
                    //_iWeapon = _weapon.GetComponent<AssaultRifle>();
                    break;
                case 2:
                    //_projectileLauncher = _weapon.AddComponent<Shotgun>();
                    //_iWeapon = _weapon.GetComponent<Shotgun>();
                    break;
                case 3:
                    //_projectileLauncher = _weapon.AddComponent<LaserRifle>();
                    //_iWeapon = _weapon.GetComponent<LaserRifle>();
                    break;
            }

            //발사체 무기는 IProjectileLauncher 를 상속 받기 때문에 인터페이스 호출
            if (_projectileLauncher != null)
            {
                //발사 런처 생성
                _projectileLauncher.ProjectileLauncher = _weapon.AddComponent<ProjectileLauncher>();
                //총 세팅 생성
                _projectileLauncher.GunSettings = _gunSettings[type];
                //총의 발사 탄환 생성
                _projectileLauncher.ProjectileLauncher.projectileSetting = _projectileLauncher.GunSettings.ProjectileSettings;

                //해당 런처에서 최초 생성시 재장전 시켜서 탄 초기화
                _projectileLauncher.ReloadAmmo();

                //사용자가 유저일 경우
                if(isUser)
                {
                    //해당 런처에서 어떤 인풋을 사용할지 설정 => 유저 인지 몬스터인기 구분하여 처리 되어야 함.
                    //IInputActions inputActions = FindObjectOfType<KeyInput>();
                    _projectileLauncher.ProjectileLauncher.SetInputActionHandler(FindObjectOfType<KeyInput>());
                }
                
                //해당 런처에서 발사시    화면 흔들림 액션 등록
                _projectileLauncher.ProjectileLauncher.FireActionHandler += FindObjectOfType<CameraShaker>().shakeAction;
                //_projectileLauncher.ProjectileLauncher.FireEventHandler.AddListener(FindObjectOfType<CameraShaker>().shakeAction);
            }

            return _iWeapon;
        }
    }
}
