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
        [Header("[고유 아이디]")]
        [SerializeField] private int id;
        public int ID { get => id; }

        [Header("[모델 프리팹]")]
        [SerializeField] private GameObject model;
        public GameObject Model { get => model; }

        [Header("[이펙트 프리팹]")]
        [SerializeField] private GameObject effect;
        public GameObject Effect { get => effect; }
    }
}
