using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    public class DoRoll : MonoBehaviour
    {
        private Animator _animator;

        public void SetAnimator(Animator animator) { _animator = animator; }

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down,
        }

        float distance = 5f;
        float duration = 0.8f/2;
        float delay = 0.5f/2;
        Ease ease = Ease.OutExpo;



        public void Roll(Direction direction)
        {
            if(_animator != null) _animator.SetTrigger("Roll");

            switch (direction)
            {
                case Direction.Left:
                    transform.DOLocalMoveZ(-distance, duration ).SetRelative(true).SetEase(ease).SetDelay(delay);
                    break;
                case Direction.Right:
                    transform.DOLocalMoveZ(distance, duration ).SetRelative(true).SetEase(ease).SetDelay(delay);
                    break;
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                Roll(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Roll(Direction.Right);
            }
        }
    }
}
