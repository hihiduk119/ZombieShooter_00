using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Camera
{
    /// <summary>
    /// 카메라 hit 에니메이션
    /// </summary>
    public class CameraHit : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = this.GetComponent<Animator>();
        }

        /// <summary>
        /// 수평으로 흔들기
        /// </summary>
        public void VerticalShake()
        {
            animator.SetTrigger("Vertical");
        }

        /// <summary>
        /// 수직으로 흔들기
        /// </summary>
        public void HorizontalShake()
        {
            animator.SetTrigger("Horizontal");
        }

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                VerticalShake();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HorizontalShake();
            }
        }
        #endregion

    }
}
