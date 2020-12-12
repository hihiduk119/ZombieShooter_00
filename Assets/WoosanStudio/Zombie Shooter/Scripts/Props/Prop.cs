﻿using System.Collections;
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
            MasterAudio.FireCustomEvent("ObjectsHit", this.transform);
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
