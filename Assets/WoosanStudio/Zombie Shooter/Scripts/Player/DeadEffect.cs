using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Player
{
    /// <summary>
    /// 플레이어 죽음시 이펙트
    /// </summary>
    public class DeadEffect : MonoBehaviour
    {
        [Header("[죽음 구름]")]
        public GameObject DeathSkull;

        [Header("[벽돌 흩날림]")]
        public GameObject RockHit;

        /// <summary>
        /// 이펙트 실행
        /// </summary>
        public void Run()
        {
            GameObject clone;

            //죽음 구름 생성
            clone = Instantiate(DeathSkull);
            clone.transform.position = this.transform.position;

            //벽돌 흩날림 생성
            clone = Instantiate(RockHit);
            Vector3 pos = this.transform.position;

            //높이 1증가
            pos.y += 1; 
            clone.transform.position = pos;
        }
    }
}
