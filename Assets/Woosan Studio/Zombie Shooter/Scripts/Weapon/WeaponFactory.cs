using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 해당 오브젝트를 만들어 주고 연결할 Action이 있으면 연결까지 다 해줌.
    /// </summary>
    public class WeaponFactory : MonoBehaviour
    {
        //Gun 세팅값 
        public List<GunSettings> _gunSettings;

        //캐슁 데이타
        GameObject _weapon;

        IWeapon _iWeapon;
        IGun _iGun;

        /// <summary>
        /// 무기를 생성
        /// </summary>
        /// <param name="inputEvents">사용할 인풋 인터페이스 세팅</param>
        /// <param name="cameraShaker">카메라 쉐이킹 인터페이스</param>
        /// <param name="reloadActionList"></param>
        /// <param name="iGun"></param>
        /// <param name="joint"></param>
        /// <param name="type">생성할 무기의 인덱스</param>
        /// <param name="useLaserPoint">생성할 무기의 인덱스</param>
        /// <returns></returns>
        public IWeapon MakeWeapon(IInputEvents inputEvents,ICameraShaker cameraShaker,List<IReloadAction> reloadActionList,ref IGun iGun , Transform joint,int type , bool useLaserPoint)
        {
            //어떤 무기는 모델을 가지고 있으면 IHaveModel인터페이스를 상속 받기에 해당 인터페이스 호출.
            IHaveModel haveModel = _gunSettings[type];

            //모델 인스턴스 생성 및 가져오기
            _weapon = haveModel.MakeModel();
            //인스턴스 부모 설정
            _weapon.transform.parent = joint;

            //초기 위치 설정
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;

            //Debug.Log("_weapon parent = " + _weapon.transform.parent.ToString());

            //인덱스 역
            switch (type)
            {
                case 0:
                    _weapon.AddComponent<Pistol>();
                    iGun = _iGun = (IGun)_weapon.GetComponent<Pistol>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<Pistol>();
                    break;
                case 1:
                    _weapon.AddComponent<AssaultRifle>();
                    iGun = _iGun = (IGun)_weapon.GetComponent<AssaultRifle>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<AssaultRifle>();
                    break;
                case 2:
                    _weapon.AddComponent<Shotgun>();
                    iGun = _iGun = (IGun)_weapon.GetComponent<Shotgun>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<Shotgun>();
                    break;
                case 3:
                    _weapon.AddComponent<LaserRifle>();
                    iGun = _iGun = (IGun)_weapon.GetComponent<LaserRifle>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<LaserRifle>();
                    break;
            }

            //레이저 포인터 사용 여부 활성, 비활성화
            joint.GetComponentInChildren<LaserPointerActor>().isVisible = useLaserPoint;

            //발사체 무기는 IProjectileLauncher 를 상속 받기 때문에 인터페이스 호출
            if (_iGun != null)
            {
                //총 세팅값 설정
                _iGun.GunSettings = _gunSettings[type];

                //총의 발사 탄환 생성
                _iGun.ProjectileLauncher.projectileSetting = _iGun.GunSettings.ProjectileSettings;

                //인풋 핸들러 연결부분.
                _iGun.SetInputEventHandler(inputEvents);

                //리로드시 액션 연결부분
                reloadActionList.ForEach(value => _iGun.ReloadEvent.AddListener(value.ReloadAction));

                //해당 런처에서 발사시 화면 흔들림 액션 등록
                if (cameraShaker != null) { _iGun.ProjectileLauncher.TriggerEvent.AddListener(cameraShaker.Shake); }                    

                //탄 초기화
                _iGun.Initialize();
            }
            return _iWeapon;
        }
    }
}
