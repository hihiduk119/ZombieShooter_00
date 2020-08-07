using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 트리거 에니메이션 실행 스크립트
    /// </summary>
    public class PlayAnimation : MonoBehaviour
    {
        [Header("[플레이하려는 에니메이션 이름]")]
        public string TriggerName = "";

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }


        /// <summary>
        /// TriggerName 트리거 에니메이션 실행
        /// </summary>
        public void Player()
        {
            animator.SetTrigger(TriggerName);
        }
    }
}
