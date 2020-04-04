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
    }
}
