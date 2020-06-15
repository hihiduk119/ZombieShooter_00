using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.Test
{
    public class SetName : MonoBehaviour
    {   
        private void Start()
        {
            GetComponent<Text>().text = transform.parent.name;
        }
    }
}
