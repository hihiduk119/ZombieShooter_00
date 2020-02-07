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
            _animator.SetTrigger("Attack");
        }

        public void Move(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }
    }
}
