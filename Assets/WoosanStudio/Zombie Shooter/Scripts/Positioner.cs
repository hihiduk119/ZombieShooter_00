using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 해당 포지션으로 이ㄷ
    /// </summary>
    public class Positioner : MonoBehaviour
    {
        [Header ("[이동하려는 포지션]")]
        public Transform Target;

        /// <summary>
        /// 포지션 재조정
        /// 카메라 위치가 조정되었을때 호출
        /// </summary>
        public void Move()
        {
            transform.position = Target.position;
        }

        #region [-TestCode]
        void Update()
        {
            //플레이어를 카메라 상대 위치로 재 포지셔닝 함.
            if (Input.GetKeyDown(KeyCode.L))
            {
                Move();
            }
        }
        #endregion

    }
}
