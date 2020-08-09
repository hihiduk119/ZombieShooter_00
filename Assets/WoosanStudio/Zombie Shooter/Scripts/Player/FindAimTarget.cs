using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    public class FindAimTarget : MonoBehaviour , ILookPoint
    {
        #region [-ILookPoint 구현부]
        private Vector3 _point = Vector3.zero;
        public Vector3 Point { get => _point; set => _point = value; }

        private UpdatePositionEvent _updatePositionEvent = new UpdatePositionEvent();
        public UpdatePositionEvent UpdatePositionEvent { get => _updatePositionEvent; set => throw new NotImplementedException(); }
        #endregion

        //매 프레임 마다 가장 가까운 타겟을 가져옴.
        [Header("[자동으로 가장 가까운 타겟을 가져옴. (Auto-FixedUpdate())]")]
        public Transform target;
        GameObject player;
        Vector3 pos;

        /// <summary>
        /// 물리 프레임으로 타겟 찾음
        /// </summary>
        void FixedUpdate()
        {
            Find();
        }

        /// <summary>
        /// 가장 가까운 타겟을 찾고 해당 나와 타겟간의 거리를 녹색으로 Scene화면에 표시.
        /// </summary>
        void Find()
        {
            target = TargetUtililty.GetNearestTarget(MonsterList.Instance.Items, this.transform);
            //해당 몬스터 에서 강제로 "AimPivot" 이름을 가진 오브젝트 찾아서 넣기
            //*해당 오브젝트가 눈높이를 가지기 때문에 이렇게 사용
            if (target != null)
            {
                target = target.Find("AimPivot");
            }

            #region [-TestCode : Player와 현재 가장 가까운 몬스터 타겟의 거리를 표시한다]
            if (target != null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    pos = player.transform.position;
                    Debug.DrawLine(pos, target.position, Color.blue);
                    //Debug.Log("거리 = " + Vector3.Distance(pos, target.position));
                    Point = target.position;
                    UpdatePositionEvent.Invoke(Point);
                }
            }
            #endregion
        }
    }
}
