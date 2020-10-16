using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.Test
{
    public class SetName : MonoBehaviour
    {
        [Header ("[이름 설정]")]
        public string Name = "";

        private void Start()
        {
            if (Name.Length > 0)
                GetComponent<Text>().text = Name;
            else 
                GetComponent<Text>().text = transform.parent.name;
        }
    }
}
