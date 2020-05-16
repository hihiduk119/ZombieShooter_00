using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/Cinemachine/FollowOffset/Make", fileName = "FollowOffset")]
    public class FollowOffset : ScriptableObject
    {
        [SerializeField]
        public List<Vector3> Offsets = new List<Vector3>();
    }
}
