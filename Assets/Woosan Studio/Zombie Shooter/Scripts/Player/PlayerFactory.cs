using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Extension;

namespace WoosanStudio.ZombieShooter
{
    public class PlayerFactory : MonoBehaviour
    {
        //플레이어 모델 타입
        public enum PlayerModels : int
        {
            Sheriff = 0,
            RiotCop,
            BusinessMan,
        }

        [Header("[플레이어 오브젝트를 담은 프리팹들]")]
        public List<GameObject> Prefabs = new List<GameObject>();
        [Header("[무기를 만들어주는 팩토리 패턴 적용.]")]
        public WeaponFactory WeaponFactory;
        [Header("[ICameraShaker를 가져오기 위한 용도]")]
        public GameObject Shaker;
        [Header("[플레이어 생성 위치]")]
        public Transform PlayerPoint;
        [Header("[플레이어 터치 위치에 따른 회전]")]
        public MoveScreenPointToRayPosition MoveScreenPointToRayPosition;

        //카메라 쉐이커
        private ICameraShaker cameraShaker;
        //플레이어
        private Player player;

        #region [-캐쉬용]
        GameObject clone = null;
        #endregion

        private void Awake()
        {
            cameraShaker = Shaker.GetComponent<ICameraShaker>();
        }

        #region [-TestCode]
        private void Start()
        {
            Make(PlayerModels.Sheriff);
        }
        #endregion

        /// <summary>
        /// 플레이어 생성
        /// </summary>
        /// <param name="playerModels">해당 타입의 모델 생</param>
        /// <returns></returns>
        public GameObject Make(PlayerModels playerModels)
        {
            clone = Instantiate(Prefabs[(int)playerModels]) as GameObject;
            player = clone.GetComponent<Player>();
            player.Initialize(WeaponFactory, cameraShaker);
            clone.transform.parent = PlayerPoint;
            clone.transform.Reset(Quaternion.Euler(0,270,0));

            //터치에 따라 플레이어 회clone
            MoveScreenPointToRayPosition.UpdatePositionEvent.AddListener(clone.GetComponent<LookAhead>().Look);

            return clone;
        }
    }
}
