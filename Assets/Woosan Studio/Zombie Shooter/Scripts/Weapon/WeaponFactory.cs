using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class WeaponFactory : MonoBehaviour
    {
        public List<GunSettings> _gunSettings;

        public void MakeWeapon(Transform parant,int Type)
        {
            _gunSettings[Type].Prefab.transform.parent = parant;
        }
    }
}