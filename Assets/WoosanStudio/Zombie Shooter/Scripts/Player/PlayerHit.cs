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

        //현재 캐릭터
        private CardSetting character;

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
            //Debug.Log("[" + transform.name + "] Hit !!");

            //카메라 쉐이킹
            cameraHit.RandomShake();

            //스크린 데미지 이펙트
            foreach (var damageEffect in damageEffects) { damageEffect.Show(); }
            //사운드 플레이

            //현재 선택 캐릭터 가져오기
            this.character= GlobalDataController.SelectedCharacterCard;

            //남녀 목소리 구분
            //*좀비 추가시 목소리 구분
            switch (this.character.Type)
            {
                case CardSetting.CardType.Woman://여성
                    MasterAudio.FireCustomEvent("FemaleHurt", this.transform);
                    break;
                case CardSetting.CardType.Prostitute://여성
                    MasterAudio.FireCustomEvent("FemaleHurt", this.transform);
                    break;
                default://그외 남성
                    MasterAudio.FireCustomEvent("MaleHurt", this.transform);
                    break;
            }
        }

        /// <summary>
        /// 현재 플레이어는 글로벌 히트에 반응하지 않음
        /// </summary>
        public void HitByGlobalDamage()
        {

        }
    }
}
