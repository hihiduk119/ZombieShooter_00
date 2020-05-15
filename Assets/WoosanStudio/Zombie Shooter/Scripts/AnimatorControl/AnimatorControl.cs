using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에니메이션을 컨트롤
    /// 현재는 Player만 컨트롤.
    /// </summary>
    public class AnimatorControl 
    {
        private Animator animator;

        /// <summary>
        /// 생성시 에니메이션 새팅 필요.
        /// </summary>
        /// <param name="animator">에니메이션 세</param>
        public AnimatorControl(Animator animator)
        {
            this.animator = animator;
        }

        /// <summary>
        /// 움직임 애니메이션 값을 넣고 실행.
        /// </summary>
        /// <param name="value">현재 애니메이션 값을 반환</param>
        /// <returns></returns>
        public float SetMoveValue(float value)
        {
            this.animator.SetFloat("Move", value);
            return this.animator.GetFloat("Move");
        }


        /// <summary>
        /// 에니메이션 움직임 값 가져오기
        /// </summary>
        /// <returns>움직임 값</returns>
        public float GetMoveValue()
        {
            return this.animator.GetFloat("Move");
        }
    }
}
