using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/ProjectileSettings/Make Setting", fileName = "ProjectileData")]
    [System.Serializable]
    public class ProjectileSettings : ScriptableObject
    {
        public string name;
        public Rigidbody bombPrefab;
        public GameObject muzzleflare;
        public float min, max;
        public bool rapidFire;
        public float rapidFireCooldown;

        public bool shotgunBehavior;
        public int shotgunPellets;
        public GameObject shellPrefab;
        public bool hasShells;
    }
}