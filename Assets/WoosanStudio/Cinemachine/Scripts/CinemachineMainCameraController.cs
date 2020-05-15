using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
namespace WoosanStudio.ZombieShooter
{
    public class CinemachineMainCameraController : MonoBehaviour
    {
        private CinemachineBrain brain;

        private void Awake()
        {
            brain = GetComponent<CinemachineBrain>();
        }
    }
}
