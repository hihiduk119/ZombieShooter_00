using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Guirao.UltimateTextDamage;

namespace WoosanStudio.ZombieShooter {
    public class UltimateTextDamageManagerInitializer : MonoBehaviour
    {
        private UltimateTextDamageManager manager;

        private void Start()
        {
            manager =  transform.GetComponent<UltimateTextDamageManager>();
            //카메라 찾아서 가져오기
            manager.theCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UnityEngine.Camera>();
        }
    }
}
