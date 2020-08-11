using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 아이템을 생성 및 아이템 컨트롤러 추가
    /// </summary>
    public class ItemFactory : MonoBehaviour
    {
        [Header("아이템 세팅값 리스트")]
        public List<ItemSetting> ItemSettings = new List<ItemSetting>();

        /// <summary>
        /// 아이템 모델 및 컨트롤러 생성
        /// </summary>
        /// <param name="index">만드려는 아이템 인덱스</param>
        public GameObject Make(int index)
        {
            //아이템 생성
            GameObject item = new GameObject(ItemSettings[index].Model.name);
            //아이템 컨트롤러 추가
            ItemController itemController = item.AddComponent<ItemController>();
            
            item.transform.parent = this.transform;
            item.transform.localPosition = Vector3.zero;

            //해당 모델 생성
            GameObject model = Instantiate(ItemSettings[index].Model, item.transform);
            //메인 이펙트 생성
            GameObject mainEffect = Instantiate(ItemSettings[index].MainEffect, item.transform);

            //아이템 컨트롤러에 모델 세팅
            itemController.Model = model;
            //아이템 컨트롤러에 메인 이펙트 세팅
            itemController.MainEffect = mainEffect;
            

            //서브 이펙트 생성
            if (ItemSettings[index].SubEffect != null)
            {
                GameObject subEffect = Instantiate(ItemSettings[index].SubEffect, item.transform);
                //아이템 컨트롤러에 서브 이펙트 세팅
                itemController.SubEffect = subEffect;
            }

            return item;
        }
    }
}
