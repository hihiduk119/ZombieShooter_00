using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.Camera
{
    /// <summary>
    /// 기존CamFollow 스크립트를 일부 수정하여 화면 밖을 벗어나지 않으면 서 따라다니는 캠 완성
    /// </summary>
    public class CustomCamFollow : MonoBehaviour
    {
        //기본 Cam Follow 의 target오브젝트에 믹스 시킬 오브젝트
        //public Transform mixTarget;
        [Header("[따라다닐 타겟]")]
        public Transform aheadTarget;

        [Header("[타겟 리스트]")]
        public List<Transform> Targets = new List<Transform>();

        [Header("[X축 을 고정]")]
        public bool fixX = false;

        [Header("[Y축 을 고정]")]
        public bool fixY = false;

        [Header("[Z축 을 고정]")]
        public bool fixZ = false;

        protected Vector3 gab;

        //캐쉬 변수
        Vector3 pos;

        public void Start()
        {
            gab = aheadTarget.position - transform.position;
        }

        /// <summary>
        /// 타겟 변경
        /// </summary>
        /// <param name="targetIndex">변경할 타겟 인덱스</param>
        public void Swap(int targetIndex)
        {
            this.aheadTarget = this.Targets[targetIndex];
        }

        /// <summary>
        /// AheadTarget 따라다님
        /// </summary>
        void Follow()
        {
            pos = aheadTarget.position - gab;
            if (fixX) { pos.x = transform.position.x; }
            if (fixY) { pos.y = transform.position.y; }
            if (fixZ) { pos.z = transform.position.z; }
            //좌 우 만 다른 타겟을 사용하여 화면 밖을 벗어 나지 않도록 만듬
            //pos.z = mixTarget.position.z;
            //pos.x = mixTarget.position.x;

            transform.position = pos;
        }

        /// <summary>
        /// 타겟 업데이트
        /// </summary>
        public void FixedUpdate()
        {
            //AheadTarget 따라다님
            Follow();
        }
    }
}
