using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Common
{
    public class DontDestory : MonoBehaviour
    {
        //싱글톤 패턴
        static public DontDestory Instance;

        private void Awake()
        {
            if (null == Instance)
            {
                //싱글톤 패턴
                Instance = this;

                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}