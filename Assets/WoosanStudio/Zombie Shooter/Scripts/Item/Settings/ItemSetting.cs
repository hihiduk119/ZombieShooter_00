using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 아이템의 고유 아이디와 해당 구성 모델 및 이펙트 
    /// </summary>
    [CreateAssetMenu(menuName = "ZombieShooter/ItemSettings/Make Setting", fileName = "ItemData")]
    public class ItemSetting : ScriptableObject
    {
        [Header("[타입]")]
        [SerializeField] private FieldItem type;
        public FieldItem Type { get => type; }

        /// <summary>
        /// 필드에 떨어지는 아이템
        /// </summary>
        public enum FieldItem
        {
            Coin = 0,   //코인
            Exp,        //경험치
        }

        [Header("[모델 프리팹]")]
        [SerializeField] private GameObject model;
        public GameObject Model { get => model; }

        [Header("[메인 이펙트 프리팹]")]
        [SerializeField] private GameObject mainEffect;
        public GameObject MainEffect { get => mainEffect; }

        [Header("[서브 이펙트 프리팹]")]
        [SerializeField] private GameObject subEffect;
        public GameObject SubEffect { get => subEffect; }

        [Header("[아이템 가치]")]
        public int Value;
    }
}
