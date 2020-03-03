using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class MonsterFSM : IFiniteStateMachine
    {
        ICharacterInput characterInput;
        ICharacterDrivingModule characterDrivingModule;
        ICharacterAnimatorModule characterAnimatorModule;

        public void SetFSM(ICharacterInput characterInput,ICharacterDrivingModule characterDrivingModule, ICharacterAnimatorModule characterAnimatorModule)
        {
            this.characterInput = characterInput;
            this.characterDrivingModule = characterDrivingModule;
            this.characterAnimatorModule = characterAnimatorModule;
        }

        //Update와 같은 역활.
        public void Tick()
        {
            if (characterInput != null) { characterInput.ReadInput(); }
            if (characterDrivingModule != null) { characterDrivingModule.Tick(); }
            if (characterAnimatorModule != null) { characterAnimatorModule.Move(characterDrivingModule.Speed); }
        }
    }
}
