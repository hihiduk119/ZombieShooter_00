using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class RegdollController : MonoBehaviour
    {
        public GameObject Model;

        private void Awake()
        {
            Model.SetActive(false);
        }

        public void SetActive(bool value)
        {
            Model.SetActive(value);
        }
    }
}
