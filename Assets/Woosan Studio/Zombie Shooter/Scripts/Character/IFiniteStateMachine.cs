using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IFiniteStateMachine //: IMove , IAttack
    {
        //필요한 모듈 셋업
        void SetFSM(Transform target, ICharacterInput characterInput, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule , MonsterSettings monsterSettings);
        //필요한 모듈 셋업
        void SetFSM(Transform target, ICharacterInput characterInput, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule, PlayerConfig playerConfig);
        //Update와 같은 역활.
        void Tick();
    }
}