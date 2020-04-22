using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    [CreateAssetMenu(menuName = "ZombieShooter/ProjectileSettings/Make Setting", fileName = "ProjectileData")]
    [System.Serializable]
    public class ProjectileSettings : ScriptableObject
    {
        //이름
        public new string name;
        //탄
        public Rigidbody bombPrefab;
        //총구 화염
        public GameObject muzzleflare;
        //발사체의 최소, 최대 속도
        public float min, max;
        //지속 사격
        public bool rapidFire;
        //사격간 딜레이
        public float rapidFireCooldown;
        //샷건 사용시 
        public bool shotgunBehavior;
        //샷건 탄 갯수
        public int shotgunPellets;
        //탄피
        public GameObject shellPrefab;
        //머즐 플레어 사용 할지 말지 결정.
        //몬스터가 공격시는 사용 안함.
        public bool hasMuzzleFlare = true;
        //탄피 사용할지 말지 결정.
        public bool hasShells = true;
        //플레이어가 쏜건지 몬스터가 쏜건지 알기 위한 용도.
        public bool playerShoted = true;
    }
}