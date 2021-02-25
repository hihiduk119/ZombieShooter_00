using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class ZombieAnimatorModule : ICharacterAnimatorModule
    {
        private Animator _animator;

        public ZombieAnimatorModule(Animator animator)
        {
            _animator = animator;
        }

        public void Attack()
        {
            if (_animator == null) return;

            _animator.SetFloat("Speed", 0);

            _animator.Play("Idle");
            _animator.SetTrigger("Attack");
        }

        public void Move(float speed)
        {
            if (_animator == null) return;

            //Debug.Log("Move??");


            //에니메이션 세팅상 1이 최고 스피
            _animator.SetFloat("Speed", 1);
        }

        public void Idle()
        {
            if (_animator == null) return;
            _animator.SetFloat("Speed", 0);

            _animator.Play("Idle");
        }
    }
}
