using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Test
{
    public class TesterPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        private PlayerFactory playerFactory;
        private MonsterFactory monsterFactory;
        private Player player = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            playerFactory = GameObject.FindObjectOfType<PlayerFactory>();

            monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();

            //weaponFactory = GameObject.FindObjectOfType<WeaponFactory>();

            //시작시 비활성화 시작
            CanvasActive(false);
        }

        /// <summary>
        /// 캔버스를 활성화 또는 비활성화
        /// </summary>
        /// <param name="value"></param>
        void CanvasActive(bool value)
        {
            if(value)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            } else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        /// <summary>
        /// 테스터 패널 닫기
        /// </summary>
        public void ClosePanel()
        {
            //Debug.Log("ClosePanel");
            CanvasActive(false);
        }

        /// <summary>
        /// 테스터 패널 열기
        /// </summary>
        public void OpenPanel()
        {
            //Debug.Log("OpenPanel");
            CanvasActive(true);
        }

        /// <summary>
        /// 자동 몬스터 생성 시작
        /// </summary>
        public void MakeMonster()
        {
            Debug.Log("MakeMonster");
            monsterFactory.Initialize();
        }

        /// <summary>
        /// 자동 몬스터 생성 정지
        /// </summary>
        public void StopMakeMonster()
        {
            Debug.Log("StopMakeMonster");
            monsterFactory.Stop();
        }

        /// <summary>
        /// 삭제 모든 몬스터 
        /// </summary>
        public void DeleteAllMonster()
        {
            Debug.Log("DeleteAllMonster");
            //모든 몬스터에 데미지 1000줌
            GlobalDamageController.Instance.DoDamage(1000);
        }

        /// <summary>
        /// 만든다 플레이어
        /// </summary>
        public void MakePlayer()
        {
            Debug.Log("MakePlayer");
            playerFactory.Initialize();
        }

        /// <summary>
        /// 시작한다 사격
        /// </summary>
        public void StartShoot()
        {
            if(player == null)  player = GameObject.FindObjectOfType<Player>();

            if (player != null)
            {
                player.Gun.IProjectileLauncher.Fire();
            }
            else
            {
                Debug.Log("player 가져오기 실패");
            }
        }


        /// <summary>
        /// 정지한다 사격
        /// </summary>
        public void StopShoot()
        {
            if (player == null) player = GameObject.FindObjectOfType<Player>();

            if (player != null)
            {
                player.Gun.IProjectileLauncher.Stop();
            }
            else
            {
                Debug.Log("player 가져오기 실패");
            }
        }
    }
}
