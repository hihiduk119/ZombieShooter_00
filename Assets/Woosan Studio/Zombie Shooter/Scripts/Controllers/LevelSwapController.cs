using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Camera;
using WoosanStudio.Common;
using Cinemachine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 레벨 변경을 컨트롤 함.
    /// </summary>
    public class LevelSwapController : MonoBehaviour
    {
        //작업해야함
        [Header("[해당 레벨의 세팅]")]
        public LevelConfig LevelConfig;

        [Header("[따라다닐 카메라 설정]")]
        public CustomCamFollow CustomCamFollow;
        [Header("[따라다닐 카메라의 타겟모음]")]
        public List<Transform> FollowCameraTargets = new List<Transform>();

        [Header("[방어 구조물]")]
        public List<Transform> Barriers = new List<Transform>();
        
        
        [Header("[몬스터 팩토리]")]
        public MonsterFactory MonsterFactory;
        [Header("[몬스터 스폰]")]
        public List<Transform> MonsterSpawnPoints = new List<Transform>();

        [Header("[플레이어 팩토리]")]
        public PlayerFactory PlayerFactory;
        [Header("[플레이어 스폰]")]
        public List<Transform> PlayerSpawnPoints = new List<Transform>();


        [Header("[폭파연출 팩토리]")]
        public ExplosionFactory ExplosionFactory;
        [Header("[폭파연출 스폰]")]
        public List<Transform> ExplosionSpwanPoints = new List<Transform>();

        #region [-Cinemachine]
        [Header("[메인 카메라의 시네머신 브레인]")]
        public CinemachineBrain CinemachineBrain;
        [Header("[시네머신]")]
        public List<Transform> CMs = new List<Transform>();
        #endregion

        #region [-TestCode]
        [Header("[현재 스테이지]")]
        public int StageLevel = 1;

        IEnumerator Start()
        {
            //SwapStage(StageLevel);
            yield return new WaitForSeconds(0.5f);
            SwapStage(StageLevel-1);

            yield return new WaitForSeconds(0.5f);

            //몬스터 생성 시작
            MonsterFactory.Initialize();
            //플레이어 생성 시작
            PlayerFactory.Initialize();

            
        }
        #endregion

        void SwapStage(int index)
        {
            /*
            //모두 비활성화
            FollowCameraTargets.ForEach(value => value.gameObject.SetActive(false));
            Barriers.ForEach(value => value.gameObject.SetActive(false));
            MonsterSpawnPoints.ForEach(value => value.gameObject.SetActive(false));
            PlayerSpawnPoints.ForEach(value => value.gameObject.SetActive(false));
            ExplosionSpwanPoints.ForEach(value => value.gameObject.SetActive(false));

            CMs.ForEach(value => value.gameObject.SetActive(false));

            //해당 스테이지만 활성화
            FollowCameraTargets[index].gameObject.SetActive(true);
            Barriers[index].gameObject.SetActive(true);
            MonsterSpawnPoints[index].gameObject.SetActive(true);
            PlayerSpawnPoints[index].gameObject.SetActive(true);
            ExplosionSpwanPoints[index].gameObject.SetActive(true);

            CMs[index].gameObject.SetActive(true);

            //따라 다닐 카메라 설정
            CustomCamFollow.aheadTarget = FollowCameraTargets[index].GetComponent<FollowCameraTarget>().aheadTarget.transform;
            //스폰포인트 스크립트 찾아서 몬스터 팩토리에 세
            MonsterFactory.SpawnPoints = MonsterSpawnPoints[index].GetComponentInChildren<SpawnPoints>();
            //플레이어 스폰 포인트 가져와서 세팅
            PlayerFactory.PlayerPoints = PlayerSpawnPoints[index].GetComponent<ITransforms>().Items;
            */

            
            //따라 다닐 카메라 설정
            CustomCamFollow.aheadTarget = SelectiveActivate(ref FollowCameraTargets, index).GetComponent<FollowCameraTarget>().aheadTarget.transform;
            //스폰포인트 스크립트 찾아서 몬스터 팩토리에 세팅
            MonsterFactory.SpawnPoints = SelectiveActivate(ref MonsterSpawnPoints, index).GetComponentInChildren<SpawnPoints>();
            //플레이어 스폰 포인트 가져와서 세팅
            PlayerFactory.PlayerPoints = SelectiveActivate(ref PlayerSpawnPoints, index).GetComponent<ITransforms>().Items;
            //베리어 선택적 활성화
            SelectiveActivate(ref Barriers, index);
            //공습연출 선택적 활성화
            SelectiveActivate(ref ExplosionSpwanPoints, index);

            //[임시]시네머신 선택적 활성화
            //SelectiveActivate(ref CMs, index);
            
        }

        /// <summary>
        /// items의 모두 비활성화 및 activeIndex 만 활성화 시키며 해당 트랜스폼 반환
        /// </summary>
        /// <param name="items">아이템 리스트</param>
        /// <param name="activeIndex">활성화 시킬 인덱스</param>
        /// <returns>activeIndex 트렌스폼 반환</returns>
        Transform SelectiveActivate(ref List<Transform> items,int activeIndex) 
        {
            items.ForEach(value => value.gameObject.SetActive(false));
            items[activeIndex].gameObject.SetActive(true);

            return items[activeIndex];
        }
    }
}
