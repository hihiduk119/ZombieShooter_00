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

            _animator.SetFloat("Speed", speed);
        }
    }
}
