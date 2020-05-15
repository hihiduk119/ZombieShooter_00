using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    public class AutoAim : MonoBehaviour , ILookPoint
    {
        #region [-ILookPoint 구현부]
        private Vector3 _point = Vector3.zero;
        public Vector3 Point { get => _point; set => _point = value; }

        private UpdatePositionEvent _updatePositionEvent = new UpdatePositionEvent();
        public UpdatePositionEvent UpdatePositionEvent { get => _updatePositionEvent; set => throw new NotImplementedException(); }
        #endregion

        Transform target;
        GameObject player;
        Vector3 pos;

        void FixedUpdate()
        {
            Find();
        }

        void Find()
        {
            target = TargetUtililty.GetNearestTarget(MonsterList.Instance.Items, this.transform);

            #region [-TestCode : Player와 현재 가장 가까운 몬스터 타겟의 거리를 표시한다]
            if (target != null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    pos = player.transform.position;
                    Debug.DrawLine(pos, target.position, Color.green);

                    Point = target.position;
                    UpdatePositionEvent.Invoke(Point);
                }
            }
            #endregion
        }
    }
}
