using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class DoRoll : MonoBehaviour
    {
        private Animator _animator;

        public void SetAnimator(Animator animator) { _animator = animator; }

        float distance = 5f;
        float duration = 0.8f/2;
        float delay = 0.5f/2;
        Ease ease = Ease.OutExpo;

        //Test Code Start
        TouchController touchController;

        private void Start()
        {
            touchController = FindObjectOfType<TouchController>();

            touchController.AddEvent(TouchController.TouchPosition.Left, data => { LeftCallback((PointerEventData)data); });
            touchController.AddEvent(TouchController.TouchPosition.Right, data => { RightCallback((PointerEventData)data); });
        }

        public void OnPointerDownDelegate(PointerEventData data)
        {
            Debug.Log("OnPointerDownDelegate called");
        }

        void LeftCallback(PointerEventData data) 
        {
            Roll(Direction.Left);
        }

        void RightCallback(PointerEventData data)
        {
            Roll(Direction.Right);
        }

        //Test Code End

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
