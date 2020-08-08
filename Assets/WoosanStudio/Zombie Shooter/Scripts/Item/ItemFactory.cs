using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 아이템을 생성
    /// </summary>
    public class ItemFactory : MonoBehaviour
    {
        [Header("아이템 세팅값 리스트")]
        public List<ItemSetting> ItemSettings = new List<ItemSetting>();

        /// <summary>
        /// 아이템 생성
        /// </summary>
        /// <param name="index">만드려는 아이템 인덱스</param>
        public GameObject Make(int index)
        {
            //아이템 생성
            GameObject item = new GameObject(ItemSettings[index].Model.name);

            item.transform.parent = this.transform;

            //거리 체크 스크립트 Add
            DistanceCheck distanceCheck = item.AddComponent<DistanceCheck>();
            //반응거리 넣기
            distanceCheck.MixDistance = 2f;

            //해당 모델 생성
            GameObject model = Instantiate(ItemSettings[index].Model, item.transform);
            //해당 이펙트 생성
            GameObject effect = Instantiate(ItemSettings[index].Effect, item.transform);

            return item;
        }
    }
}
