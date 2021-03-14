using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

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
        public GameObject Make(ItemSetting.FieldItem type, Transform spawnTransform, int value ,UnityAction<Transform> destoryAction , UnityAction<Transform> destoryAction2)
        {
            //아이템 인덱으로 변경
            //*enum 순서는 List순서와 같아야 한다.
            int index = (int)type;

            //아이템 생성
            GameObject item = new GameObject(ItemSettings[index].Model.name);
            //아이템 컨트롤러 추가
            ItemController itemController = item.AddComponent<ItemController>();
            //스폰 트랜스폼 넣어줌
            itemController.spawmTransform = spawnTransform;

            //이벤트 핸들러 연결 => 삭제시 생성 위치 알려주는 액션
            itemController.ItemDestoryEvent.AddListener(destoryAction);

            //이벤트 핸들러 연결 => 삭제시 나 자신을 알려주는 액션
            itemController.ItemDestoryEvent2.AddListener(destoryAction2);

            item.transform.parent = this.transform;
            item.transform.localPosition = Vector3.zero;

            //해당 모델 생성
            GameObject model = Instantiate(ItemSettings[index].Model, item.transform);

            //아이템에 필드 아이템.cs 추가
            Field.Item fieldItem = item.AddComponent<Field.Item>();

            //아이템 값 세팅
            fieldItem.Value = value;

            //Model이 가지고있는 HUD FieldItem에 셋업
            SickscoreGames.HUDNavigationSystem.HUDNavigationElement hud = model.GetComponentInChildren<SickscoreGames.HUDNavigationSystem.HUDNavigationElement>();
            fieldItem.HUDNaviElement = hud;

            //아이템 컨트롤러에 모델 세팅
            itemController.Model = model;

            //메인 이펙트 생성
            if (ItemSettings[index].MainEffect != null)
            {
                GameObject mainEffect = Instantiate(ItemSettings[index].MainEffect, item.transform);
                //아이템 컨트롤러에 메인 이펙트 세팅
                itemController.MainEffect = mainEffect;
            }

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
