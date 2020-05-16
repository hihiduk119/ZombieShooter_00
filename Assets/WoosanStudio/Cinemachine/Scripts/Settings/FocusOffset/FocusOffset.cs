using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 포커스 마추기용 데이터로 위치값 과 회전 값을 가짐
    /// </summary>
    [Serializable]
    public struct Focus
    {
        [SerializeField]
        public Vector3 Position;
        [SerializeField]
        public Vector3 Rotation;
    }

    [CreateAssetMenu(menuName = "ZombieShooter/Cinemachine/FocusOffset/Make", fileName = "FocusOffset")]
    public class FocusOffset : ScriptableObject
    {
        [SerializeField]
        public List<Focus> Offsets = new List<Focus>();
    }
}
