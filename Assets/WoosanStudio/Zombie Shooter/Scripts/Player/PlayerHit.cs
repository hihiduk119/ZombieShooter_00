using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DarkTonic.MasterAudio;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 맞음 클래스
    /// </summary>
    public class PlayerHit : MonoBehaviour, IHaveHit
    {
        private Camera.CameraHit cameraHit;
        private UI.DamageEffect[] damageEffects;

        void Awake()
        {
            cameraHit = GameObject.FindObjectOfType<Camera.CameraHit>();
            damageEffects = GameObject.FindObjectsOfType<UI.DamageEffect>();
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
            foreach (var damageEffect in damageEffects) { damageEffect.Show(); }
            //사운드 플레이
            //*남녀 구분 필요
            MasterAudio.FireCustomEvent("MaleHurt", this.transform);
        }
    }
}
