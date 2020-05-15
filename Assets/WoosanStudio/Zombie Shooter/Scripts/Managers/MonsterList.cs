using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터들의 위치를 알기위해 따로 보관함.
    /// AI 플레이어의 자동 타겟을 찾기위해 사용됨.
    /// </summary>
    public class MonsterList : MonoBehaviour
    {
        static public MonsterList Instance;

        public List<Transform> Items = new List<Transform>();

        private void Awake()
        {
            Instance = this;
        }
    }
}
