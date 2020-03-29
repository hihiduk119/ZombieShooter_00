using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 베리어 구현
    /// </summary>
    public class Barrier : MonoBehaviour, IHaveHit
    {
        //cashe
        private Animator animator;

        private IBlink blink;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            blink = transform.GetComponentInChildren<IBlink>();
        }

        //강제 에니메이션 재시작[Gun Trigger 방식]
        public void Hit()
        {
            animator.Play("Empty");
            animator.SetTrigger("Pong");
            //맞았을때 깜빡임.
            blink.Blink();
        }

        #region [-TestCode]
        /*void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Hit();
            }
        }*/
        #endregion
    }
}
