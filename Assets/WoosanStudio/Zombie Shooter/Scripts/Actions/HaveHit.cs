using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    public class HaveHit : MonoBehaviour, IHaveHit
    {
        public void Hit()
        {
            Debug.Log("[" + transform.name + "] Hit !!");
        }
    }
}
