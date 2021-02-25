﻿using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IFiniteStateMachine //: IMove , IAttack
    {
        //필요한 모듈 셋업
        void SetFSM(Transform target, ICharacterInput characterInput, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule, ICharacterAttackModule characterAttackModule);
        //필요한 모듈 셋업
        void SetFSM(Transform target, ICharacterInput characterInput, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule, ICharacterAttackModule characterAttackModule, PlayerConfig playerConfig);
        //Update와 같은 역활.
        void Tick();

        //생성된 인터페이스 접근을 위해
        ICharacterInput GetCharacterInput();
        ICharacterDrivingModule GetCharacterDrivingModule();
        ICharacterAnimatorModule GetCharacterAnimatorModule();
        ICharacterAttackModule GetCharacterAttackModule();
    }
}