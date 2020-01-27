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
        /// <param name="parant">생성될 오브젝트의 부모</param>
        /// <param name="type">생성할 무기의 인덱스</param>
        /// <returns></returns>
        public IWeapon MakeWeapon(IInputEvents inputEvents,ICameraShaker cameraShaker,List<IReloadAction> reloadActionList, Transform parant,int type)
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
                    _weapon.AddComponent<Pistol>();
                    Debug.Log("Make pistol.cs and ProjectileLauncher.cs");
                    _iGun = (IGun)_weapon.GetComponent<Pistol>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<Pistol>();
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
            if (_iGun != null)
            {
                //총 세팅값 설정
                _iGun.GunSettings = _gunSettings[type];

                //총의 발사 탄환 생성
                _iGun.ProjectileLauncher.projectileSetting = _iGun.GunSettings.ProjectileSettings;

                
                //인풋 핸들러 연결부분.
                _iGun.SetInputEventHandler(inputEvents);

                //리로드시 액션 연결부분
                //_iGun.ConnectReloadEvent(reloadEventSocket);
                reloadActionList.ForEach(value => _iGun.ReloadEvent.AddListener(value.ReloadAction));

                //_iGun.ReloadEvent.AddListener(reloadAction);
                /*for (int index = 0; index < reloadActionList.Count; index++)
                {
                    _iGun.ReloadEvent.AddListener(reloadActionList[index].ReloadAction);
                }*/

                //해당 런처에서 발사시 화면 흔들림 액션 등록
                if (cameraShaker != null) 
                    _iGun.ProjectileLauncher.TriggerEvent.AddListener(cameraShaker.Shake);

                //탄 초기화
                _iGun.Initialize();
            }

            //Test code

            return _iWeapon;
        }
    }
}
