using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터들의 위치를 알기위해 따로 보관함.
    /// </summary>
    public class MonsterManager : MonoBehaviour
    {
        static public MonsterManager Instance;

        public List<Transform> Monsters = new List<Transform>();
        //가장 가까운 타겟의 포지션을 이벤트로 발
        public UpdatePositionEvent UpdateNearestMonsterPositionEvent = new UpdatePositionEvent();

        public Transform Center;

        public Transform target;     

        private void Update()
        {
            target = TargetUtililty.GetNearestTarget(Monsters, Center);

            if(target != null)
            {
                UpdateNearestMonsterPositionEvent.Invoke(target.position);
            }
        }
    }
}
