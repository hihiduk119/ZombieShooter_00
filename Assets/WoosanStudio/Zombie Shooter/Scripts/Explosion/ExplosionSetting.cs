using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/ExplosionSetting/Make Setting", fileName = "ExplosionSettingData")]
    [System.Serializable]
    public class ExplosionSetting : ScriptableObject
    {
        public new string name;
        public GameObject[] prefabs;
    }
}
