using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter.Test
{
    /// <summary>
    /// 강제로 플레이어 총기의 parent를 상위로 올림.
    /// 일종의 꼼수 코드
    /// </summary>
    public class TestCode01 : MonoBehaviour
    {
        static public TestCode01 Instance;

        public Transform target;
        public Transform myParent;

        private void Awake()
        {
            Instance = this;
        }

        public void Swap()
        {
            target.parent = myParent;

            //정확한 가운데를 강제로 마추기 위한 보정 값.
            target.localRotation = Quaternion.Euler(-90, -116, 25);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Swap();
            }
        }
    }
}
