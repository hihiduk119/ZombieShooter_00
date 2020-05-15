using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter {
    public class CopyComponets : MonoBehaviour
    {
        public ChildsTransform From;
        public ChildsTransform To;

        public void Copy()
        {
            To.SetAllPosition(From.GetAllPosition());
            To.SetAllRotation(From.GetAllRotation());
        }
    }
}
