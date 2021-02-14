using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 총기에 레이저 포인트 표현
    /// </summary>
    public class LaserPoint : MonoBehaviour , ILaserPointer
    {
        //총기의 레이저 포인터 리스트[총기 많아 졌을때 무기 교체시 무기마다 레이저포인터 세팅되게 해야함]
        public List<Transform> laserPointers = new List<Transform>();
        //레이저 포인트
        public LineRenderer lineRenderer;
        //최대 길이
        public float maxLength = 50f;

        [Header("[레이저 포인터 사용 플레그]")]
        [SerializeField]
        private bool isAble = false;
        public bool IsAble { get => isAble; set => isAble = value; }

        //캐싱용
        Vector3 tmpPos;

        /// <summary>
        /// 레이저 활성 & 비활성
        /// </summary>
        /// <param name="value"></param>
        public void SetActivate(bool value)
        {
            //라인 렌더러 활성 & 비활성
            lineRenderer.enabled = value;
            //업데이트 사용 & 비사용 플레그
            isAble = value;
        }

        void Update() {
            if (!IsAble) return;

            //라인 렌더러 시작 위치 따라다니기
            lineRenderer.SetPosition(0, laserPointers[0].position);
            tmpPos = laserPointers[0].TransformPoint(new Vector3(0, 0, maxLength));
            lineRenderer.SetPosition(1, tmpPos);
        }
    }
}
