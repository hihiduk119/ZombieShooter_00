using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 해당 오브젝트를 만들어 주고 연결할 Action이 있으면 연결까지 다 해줌.
    /// 다해주기 위해서는 모두 가지고 있어야 함
    /// </summary>
    public class WeaponFactory : MonoBehaviour
    {
        //Gun 세팅값 
        public List<GunSettings> _gunSettings;

        //총알 세팅값
        //public ProjectileSettings[][] _projectileSettings;
        public List<ProjectileData> projectileDatas;

        [System.Serializable]
        public class ProjectileData
        {
            public List<ProjectileSettings> _projectileSettings;
        }
        

        //**Player는 이미 IGun과 IWeapon을 가지고 있다는걸 명심하자.
        //**WeaponFactory에서 가져오는 행위는 독립성을 해칠수 있다.
        //프로젝타일 런처를 가져오려면 IGun이 필요함
        public List<IGun> Guns = new List<IGun>();
        //무기의 시작과 정지를 할수 있는 IWeaponAction을 가지고 있음
        public List<IWeapon> Weapons = new List<IWeapon>();

        //무기의 공격 시작과 끝을 이벤트 인터페이스
        //public IInputEvents InputEvents { get => inputEvents; set => inputEvents = value; }
        //public IStart StartEvent { get => startEvent; set => startEvent = value; }
        //public IEnd EndEvent { get => endEvent; set => endEvent = value; }

        //카메라 쉐이킹용 이벤트 인터페이스 => IInputEvent와 통합 가능 할듯 리팩토링 해야함.
        //public ICameraShaker CameraShaker { get => cameraShaker; set => cameraShaker = value; }
        //무기의 재장전 이밴트 액션 리스트
        //public List<IReloadAction> ReloadActionList { get => reloadActionList; set => reloadActionList = value; }

        //IStart startEvent;
        //IEnd endEvent;
        //ICameraShaker cameraShaker;
        //List<IReloadAction> reloadActionList;

        //캐슁 데이타
        GameObject _weapon;
        IWeapon _iWeapon;
        IGun _iGun;

        /// <summary>
        /// 무기를 생성
        /// </summary>
        /// <param name="start">총의 발사 시작 및 발사 끝을 알리는 이벤트</param>
        /// <param name="end">총의 발사 시작 및 발사 끝을 알리는 이벤트</param>
        /// <param name="cameraShaker">카메라 쉐이킹 인터페이스</param>
        /// <param name="reloadActionList">재장전시 발생 되는 이벤트 액션 리스트</param>
        /// <param name="iGun">해당 컴퍼넌트 생성</param>
        /// <param name="joint">생성 시킬 오브젝트의 부</param>
        /// <param name="type">생성할 무기의 인덱스</param>
        /// <param name="useLaserPoint">생성할 무기의 인덱스</param>
        /// <returns></returns>
        public IWeapon MakeWeapon(IStart start,IEnd end,ICameraShaker cameraShaker,UnityAction ammoEmpty, ref IGun iGun , Transform joint,int gunType ,int ammoType,bool useLaserPointer, GameObject muzzleFlashProjector)
        {
            //어떤 무기는 모델을 가지고 있으면 IHaveModel인터페이스를 상속 받기에 해당 인터페이스 호출.
            IHaveModel haveModel = _gunSettings[gunType];

            //모델 인스턴스 생성 및 가져오기
            _weapon = haveModel.MakeModel();
            //인스턴스 부모 설정
            _weapon.transform.parent = joint;

            //초기 위치 설정
            _weapon.transform.localPosition = _gunSettings[gunType].InitPosition;
            _weapon.transform.localRotation = Quaternion.identity;

            //Debug.Log("_weapon parent = " + _weapon.transform.parent.ToString());

            //인덱스 역
            switch (gunType)
            {
                case 0:
                    _weapon.AddComponent<Pistol>();
                    iGun = _iGun = (IGun)_weapon.GetComponent<Pistol>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<Pistol>();
                    break;
                case 1:
                    _weapon.AddComponent<Shotgun>();
                    iGun = _iGun = (IGun)_weapon.GetComponent<Shotgun>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<Shotgun>();
                    break;
                case 2:
                    _weapon.AddComponent<AssaultRifle>();
                    iGun = _iGun = (IGun)_weapon.GetComponent<AssaultRifle>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<AssaultRifle>();
                    break;
                case 3:
                    _weapon.AddComponent<LaserRifle>();
                    iGun = _iGun = (IGun)_weapon.GetComponent<LaserRifle>();
                    _iWeapon = (IWeapon)_weapon.GetComponent<LaserRifle>();
                    break;
            }

            //생성된 IGun은 모두 Guns에 저장됨.
            Guns.Add(iGun);
            //생성된 IWeapon 모두 Weapons 저장됨.
            Weapons.Add(_iWeapon);

            //머즐 플레쉬의 블링크를 트리거 이벤트와 연결
            if (muzzleFlashProjector != null)
            {
                MuzzleFlash muzzleFlash = muzzleFlashProjector.GetComponent<MuzzleFlash>();

                Debug.Log("hellow = " + muzzleFlashProjector.name);
               

                //발사시 플레어 블링크 등록
                _iGun.ProjectileLauncher.TriggerEvent.AddListener(((IMuzzleFlare)muzzleFlash).Blink);
            }

            //발사체 무기는 IProjectileLauncher 를 상속 받기 때문에 인터페이스 호출
            if (_iGun != null)
            {
                //총 세팅값 설정
                //*AssaultRifle,Pistol,등 실제 클래스에서 사용은 함.
                //*직관성 부족으로 리팩토링 필요함.
                _iGun.GunSettings = _gunSettings[gunType];

                //실제 (ProjectileLauncher에) 발사기관에 총 세팅
                _iGun.ProjectileLauncher.gunSetting = _gunSettings[gunType];

                //실제 (ProjectileLauncher에) 발사기관에 탄환 세팅
                //_iGun.ProjectileLauncher.projectileSetting = _projectileSettings[ammoType][gunType];
                _iGun.ProjectileLauncher.projectileSetting = projectileDatas[ammoType]._projectileSettings[gunType];

                //에벤트 없을을 통지함.
                if (start == null) Debug.Log("start event null");
                if (end == null) Debug.Log("end event null");

                //무기 공격시작, 공격 끝 핸들러 연결부분.
                if (start != null && end != null) _iGun.SetInputEventHandler(start, end);

                //탄약 다씀 이벤트 연결
                //*연결해 놓으면 리로드 시작시 알아서 실행
                if (ammoEmpty != null) _iGun.EmptyEvent.AddListener(ammoEmpty);

                //해당 런처에서 발사시 화면 흔들림 액션 등록
                if (cameraShaker != null) { _iGun.ProjectileLauncher.TriggerEvent.AddListener(cameraShaker.Shake); }

                //총 발사시 반동에니메이션 등록
                //Weapon.GunAnimation gunRecoil = GameObject.FindObjectOfType<Weapon.GunAnimation>();
                //if (gunRecoil != null) { _iGun.ProjectileLauncher.TriggerEvent.AddListener(gunRecoil.Action); }


                //탄 초기화
                _iGun.Initialize();
            }
            return _iWeapon;
        }
    }
}
