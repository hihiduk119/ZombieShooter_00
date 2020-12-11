﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 맞음 클래스
    /// </summary>
    public class PlayerHit : MonoBehaviour, IHaveHit
    {
        private Camera.CameraHit cameraHit;
        private UI.DamageEffect damageEffect;

        void Awake()
        {
            cameraHit = GameObject.FindObjectOfType<Camera.CameraHit>();
            damageEffect = GameObject.FindObjectOfType<UI.DamageEffect>();
        }

        /// <summary>
        /// 플레이어 맞았을때
        /// </summary>
        public void Hit()
        {
            Debug.Log("[" + transform.name + "] Hit !!");

            //카메라 쉐이킹
            cameraHit.RandomShake();
            //스크린 데미지 이펙트
            damageEffect.Show();
        }
    }
}