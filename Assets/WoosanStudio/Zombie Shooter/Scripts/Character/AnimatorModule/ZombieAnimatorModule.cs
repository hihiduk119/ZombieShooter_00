using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Character
{
    public class ZombieAnimatorModule : ICharacterAnimatorModule
    {
        private Animator _animator;

        private AnimatorState state = AnimatorState.Idle;
        public AnimatorState State { get => state; }

        public ZombieAnimatorModule(Animator animator)
        {
            _animator = animator;
        }

        /// <summary>
        /// 대기 에니에이션
        /// </summary>
        public void Idle()
        {
            if (_animator == null) return;
            _animator.SetFloat("Speed", 0);

            _animator.Play("Idle");

            //에니메이션 상태 변경
            state = AnimatorState.Idle;
        }

        /// <summary>
        /// 공격 에니미에션
        /// </summary>
        public void Attack()
        {
            if (_animator == null) return;

            _animator.SetFloat("Speed", 0);

            _animator.Play("Idle");
            _animator.SetTrigger("Attack");

            //에니메이션 상태 변경
            state = AnimatorState.Attack;
        }

        /// <summary>
        /// 움직임 에니메이션
        /// </summary>
        /// <param name="speed"></param>
        public void Move(float speed)
        {
            if (_animator == null) return;

            //Debug.Log("Move??");


            //에니메이션 세팅상 1이 최고 스피
            _animator.SetFloat("Speed", 1);

            //에니메이션 상태 변경
            state = AnimatorState.Move;
        }
    }
}
