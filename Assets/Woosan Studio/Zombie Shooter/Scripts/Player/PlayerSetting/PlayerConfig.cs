using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/PlayerConfig/Make Config", fileName = "PlayerConfigData")]
    [System.Serializable]
    public class PlayerConfig : ScriptableObject
    {
        public new string name;
        public GameObject prefab;
        public bool useAI = false;
        public bool useLaserPointer = false;
        public bool useMuzzleFlare = false;
        public bool useRoll = false;
        public GameObject muzzleFlare;
        public bool useShadow = false;
        public GameObject ShadowProjector;
        public bool useJoystick = true;
    }
}
