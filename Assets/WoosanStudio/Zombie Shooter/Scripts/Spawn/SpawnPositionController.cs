using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Common;
namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 스폰 위치를 컨트롤 함.
    /// </summary>
    public class SpawnPositionController : MonoBehaviour
    {
        [Header("스테이지 모음 리스트")]
        public List<Transform> SpawnPointList = new List<Transform>();

        [Header("스폰 위치 인덱스 = ")]
        public int Index = 0;

        [Header("스테이지 모음 루트 (Auto->Awake())")]
        public Transforms Parent;


        private void Awake()
        {
            Parent = GetComponent<Transforms>();

            //몬스터 생성할 스테이지의 생성 위치를 가져오기 위해
            Transforms.FindChildInFirstLayer(ref SpawnPointList, Parent.transform);
        }

        /// <summary>
        /// 해당 인덱스에 맞는 스폰 포인트 가져오기
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SpawnPoints GetSpawnPoints(int index)
        {
            return SpawnPointList[index].GetComponent<SpawnPoints>();
        }
    }
}
