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
        [Header("[이동하려는 포지션 이름으로 찾기]")]
        public bool UseName = false;

        [Header("[이동하려는 포지션 이름]")]
        public string Name = "FindName";

        [Header ("[이동하려는 포지션]")]
        public Transform Target;

        [Header("[추가 값]")]
        public Vector3 ExtraValue;

        private void Awake()
        {
            //이동 하려는 포지션을 이름으로 찾ㅇ
            if(UseName)
            {
                Target = GameObject.Find(Name).transform;
            }
        }

        /// <summary>
        /// 포지션 재조정
        /// 카메라 위치가 조정되었을때 호출
        /// </summary>
        public void Move()
        {
            Vector3 pos = Target.position;
            pos += ExtraValue;
            transform.position = pos;
        }

        /*
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
        */

    }
}
