using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DarkTonic.MasterAudio;

namespace WoosanStudio.ZombieShooter
{
    [RequireComponent(typeof(Animator))]
    public class Prop : MonoBehaviour, IHaveHit
    {
        //cashe
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        //강제 에니메이션 재시작[Gun Trigger 방식]
        public void Hit()
        {
            animator.Play("Empty");
            animator.SetTrigger("Pong");

            //총맞은 사운드
            //*공습 데미지 사운드때는 동작 안하게 만들어야 함.
            MasterAudio.FireCustomEvent("ObjectsHit", this.transform);
        }

        /// <summary>
        /// 일반 히트와 동일
        /// </summary>
        public void HitByGlobalDamage()
        {
            Hit();
        }

        //void Update()
        //{
        //    if(Input.GetKeyDown(KeyCode.A))
        //    {
        //        Hit();
        //    }
        //}
    }
}
