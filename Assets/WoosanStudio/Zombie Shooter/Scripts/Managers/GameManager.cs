using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 게임 전체를 통합 관리 한다.
    ///
    /// 1.캐릭터는 몇명 생성 할지
    /// 2.몇번째 스테이지로 씬을 보낼지
    /// 
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private StageChangeController stageChangeController;

        //플레이어 생성
        private PlayerFactory playerFactory;
        //몬스터 생성
        private MonsterFactory monsterFactory;
        //생성된 플레이어 활성 비활성 제어
        private PlayersController playersController;

        private void Awake()
        {
            //자동으로 가져오기
            playerFactory = GameObject.FindObjectOfType<PlayerFactory>();
            monsterFactory = GameObject.FindObjectOfType<MonsterFactory>();
            stageChangeController = GameManager.FindObjectOfType<StageChangeController>();
            playersController = GameObject.FindObjectOfType<PlayersController>();
        }

        /// <summary>
        /// 플레이이어와 몬스터 들을 세팅 한다.
        /// </summary>
        public void Load()
        {
            LoadAllProps();
        }

        /// <summary>
        /// 해당 레벨에서 로드할수 있는 모든 것을 로드 한다.
        /// </summary>
        private void LoadAllProps()
        {
            //monsterFactory.Initialize();
            playerFactory.Initialize();
        }

        /// <summary>
        /// 해당 레벨로 초기화 시킴
        /// </summary>
        /// <param name="level"></param>
        public void Initialize(int level)
        {
            //해당 스테이지로 화면을 이동시킴
            stageChangeController.Change(level);
        }

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Initialize(0);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Initialize(1);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Load();
            }
        }
        #endregion

    }
}
