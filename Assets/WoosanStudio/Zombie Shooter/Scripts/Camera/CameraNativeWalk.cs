using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카메라의 느리게 좌우로 움직이는 에니메이션 컨트롤
    /// </summary>
    public class CameraNativeWalk : MonoBehaviour
    {
        private Animator mAnimator;

        private void Awake()
        {
            mAnimator = GetComponent<Animator>();
        }

        /// <summary>
        /// 좌우 천천히 흔듬 에니메이션 멈춤
        /// </summary>
        public void Stop()
        {
            mAnimator.enabled = false;
            transform.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// 좌우 천천히 흔듬 에니메이션 시작
        /// </summary>
        public void Run()
        {
            mAnimator.enabled = true;
        }
    }
}
