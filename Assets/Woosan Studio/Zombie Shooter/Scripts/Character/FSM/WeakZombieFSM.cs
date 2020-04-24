using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class WeakZombieFSM : IFiniteStateMachine ,  IProjectileLauncher
    {
        ICharacterInput characterInput;
        ICharacterDrivingModule characterDrivingModule;
        ICharacterAnimatorModule characterAnimatorModule;
        ICharacterAttackModule characterAttackModule;

        /*
        #region [무기 생성]
        //무기를 만들어주는 팩토리 패턴 적용.
        public WeaponFactory _weaponFactory;
        //무기 생성을 위해 임시로 만듬
        private IInputEvents inputEvents = null;
        private ICameraShaker cameraShaker = null;
        private List<IReloadAction> _reloadActionList = null;
        private IGun _iGun = null;
        //무기 연결 부분
        private Transform joint;
        private GunSettings.WeaponType weaponType = GunSettings.WeaponType.AssaultRifle;
        private bool useLaserPoint = false;
        #endregion

        void Awake()
        {
            _weaponFactory.MakeWeapon(inputEvents, cameraShaker, _reloadActionList, ref _iGun, joint, (int)weaponType, useLaserPoint);
        }
        */

        #region [-IProjectileLauncher Implement]
        public ProjectileLauncher ProjectileLauncher { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public UnityEvent TriggerEvent { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IProjectileLauncherEvents GetProjectileLauncherEvents()
        {
            throw new System.NotImplementedException();
        }

        public void ReloadAmmo()
        {
            throw new System.NotImplementedException();
        }
        #endregion


        #region [-IProjectileLauncher Implement]
        //플레이어 와 중첩으로 사용되어 임시로 하나 선언
        public void SetFSM(Transform target, ICharacterInput characterInput, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule, ICharacterAttackModule characterAttackModule, PlayerConfig playerConfig) { throw new System.NotImplementedException(); }

        public void SetFSM(Transform target, ICharacterInput characterInput, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule , ICharacterAttackModule characterAttackModule)
        {
            this.characterInput = characterInput;
            this.characterDrivingModule = characterDrivingModule;
            this.characterAnimatorModule = characterAnimatorModule;
            this.characterAttackModule = characterAttackModule;
            //this.monsterSettings = monsterSettings;

            //공격이 시작되었음을 등록
            //[중요]ICharacterAttackModule 에 공격 시작을 알림 등록
            characterDrivingModule.ReachDestinationEvent.AddListener(() => {
                //attackStart = true;
                characterAttackModule.AttackStart = true;
            });
        }

        //Update와 같은 역활.
        //각 모듈의 구동부 호출
        public void Tick()
        {
            //AI 인풋 컨트롤 호출
            if (characterInput != null) { characterInput.ReadInput(); }
            //움직임 모듈 호출
            if (characterDrivingModule != null) { characterDrivingModule.Tick(); }
            //에니메이션 모듈 호출
            if (characterAnimatorModule != null) {
                //드라이빙 모듈에서 움직임 상태일 때만 움직임 에니메이션 호출
                if (characterDrivingModule.State == DrivingState.Move) { characterAnimatorModule.Move(characterDrivingModule.Speed); }
            }
            //공격 모듈 호출
            if (characterAttackModule != null) { characterAttackModule.Attack(characterAnimatorModule); }
        }

        public void UseAmmo()
        {
            throw new System.NotImplementedException();
        }
        #endregion


    }
}
