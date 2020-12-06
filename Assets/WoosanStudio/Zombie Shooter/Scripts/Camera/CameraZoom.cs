using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카메라의 줌인 줌 아웃 에니메이션 컨트롤
    /// </summary>
    public class CameraZoom : MonoBehaviour
    {
        [Header("[이동 로컬 포지션]")]
        public Vector3 TargetPosition;

        [Header("[줌 시간]")]
        public float Duration = 1f;

        [Header("[줌 Ease]")]
        public Ease Ease = Ease.InOutQuad;

        [Header("[줌 인 아웃 데이터 ]")]
        public List<ZoomSet> ZoomSets = new List<ZoomSet>();

        [Header("[Auto 줌 인 아웃 사용시 인덱스 => 구조적으로 문제 있으니 나중에 변경 해야함]")]
        public int ZoomIndex = 0;

        //초기화 시킬 처음값
        private Vector3 mDefalutPos;

        //줌을위한 전용 세트
        [System.Serializable]
        public struct ZoomSet
        {
            //이동 로컬 포지션
            [SerializeField]
            public string name;
            //이동 로컬 포지션
            [SerializeField]
            public Vector3 TargetPosition;
            //줌 Ease
            [SerializeField]
            public Ease Ease;
            //줌 시간
            [SerializeField]
            public float Duration;
        }

        //지금 사용 가능한지 여부확인.
        private bool isAble = false;
        public bool IsAble => isAble;

        private void Awake()
        {
            //초기값 세팅 [로컬 값임]
            mDefalutPos = transform.localPosition;
        }

        /// <summary>
        /// 리로드 전용 줌 아웃
        /// </summary>
        public void AutoZoomOut()
        {
            ZoomOut(1);
        }

        /// <summary>
        /// 화면이 하늘 위로 오르는 연출
        /// </summary>
        public void ZoomOut(int index)
        {
            //에니메이션 시작시 사용 불가로 만듬
            isAble = false;
            //현재 인덱스 바로 세팅
            ZoomIndex = index;
            //강제로 최초 상태로 초기화
            transform.localPosition = mDefalutPos;

            transform.DOLocalMove(ZoomSets[index].TargetPosition, ZoomSets[index].Duration).SetEase(Ease).OnComplete(() => {
                //에니메이션 끝에 사용 불가 해제
                isAble = true;
                //에니메이션 끝 알림림
                //mEndEvent.Invoke();
            });
        }

        /// <summary>
        /// 올랐던 하늘을 복귀하는 연출
        /// </summary>
        public void ZoomIn()
        {
            //에니메이션 시작시 사용 불가로 만듬
            isAble = false;

            transform.DOLocalMove(mDefalutPos, ZoomSets[ZoomIndex].Duration).SetEase(Ease).OnComplete(() => {
                //에니메이션 끝에 사용 불가 해제
                isAble = true;
            });
        }

        //#region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        //Y축으로 15정도 Up카메라 연출
        //        ZoomOut(0);
        //    }

        //    if (Input.GetKeyDown(KeyCode.W))
        //    {
        //        //Up 카메라 연출 회복
        //        ZoomIn();
        //    }
        //}
        //#endregion

    }
}
