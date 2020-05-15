using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 방향을 결정하는 플래그
    /// 위, 아래, 좌, 우 총 4가지만 존재
    /// </summary>
    [System.Serializable]
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    }
}
