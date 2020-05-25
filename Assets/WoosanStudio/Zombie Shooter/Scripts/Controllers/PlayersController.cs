using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// PlayerFactory에서 생성된 각 플레이어 관리
    /// </summary>
    public class PlayersController : MonoBehaviour
    {
        [Header("[모든 플레이어]")]
        public List<Player> Players = new List<Player>();

        /// <summary>
        /// 모든 플레이어 사격 시작
        /// </summary>
        public void StartShoot()
        {
            Players.ForEach(value => value.Gun.IProjectileLauncher.Fire());
        }

        /// <summary>
        /// 모든 플레이어 사격 중지
        /// </summary>
        public void StopShoot()
        {
            Players.ForEach(value => value.Gun.IProjectileLauncher.Stop());
        }

        /// <summary>
        /// 모든 플레이어 활성 및 비활성화 시킴 
        /// </summary>
        /// <param name="value"></param>
        public void SetActiveAll(bool value)
        {
            if (value)  //활성화
            {
                Players.ForEach(player => {
                    //플레이어 자체 활성화
                    player.gameObject.SetActive(true);
                    //플레이어와 연결된 체력바 visiualization
                    player.GetComponent<HealthBar>().HealthbarPrefab.gameObject.SetActive(true); ;
                });
                

            } else      //비활성화
            {
                //공격 정지 시킴
                StopShoot();
                Players.ForEach(player => {
                    //플레이어 자체 비활성화
                    player.gameObject.SetActive(false);
                    //플레이어와 연결된 체력바 nonvisualization
                    player.GetComponent<HealthBar>().HealthbarPrefab.gameObject.SetActive(false); ;
                    
                });
            }   
        }
    }
}
