namespace WoosanStudio.ZombieShooter
{
    public interface IFiniteStateMachine
    {
        //필요한 모듈 셋업
        void SetFSM(ICharacterInput characterInput, ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule);
        //Update와 같은 역활.
        void Tick();
    }
}