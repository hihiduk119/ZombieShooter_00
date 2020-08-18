using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
//using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 타겟에 에임을 설정하는 부분
    /// </summary>
    public class AimMaker : MonoBehaviour
    {
        //싱글톤 패턴
        static public AimMaker Instance;

        //기본 되는 표적 포지션
        [Header("[기본 로컬 포지션]")]
        public Vector3 DefaultLocalPosition = new Vector3(0f, 0.07f, 0f);
        [Header("[기본 로컬 로테이션]")]
        public Vector3 DefaultLocalRotation = new Vector3(90f, 0, 0);
        //3가지 종류 표적 사이즈 [ZombieKinds와 인덱스가 같다]
        [Header("[기본 로컬 스케일]")]
        public List<Vector3> DefaultLocalScales = new List<Vector3>() { new Vector3(1.2f, 1.2f, 1.2f), new Vector3(1.5f, 1.5f, 1.5f), new Vector3(2f, 2f, 2f) };
        [Header("[마크 모델]")]
        public Transform Model;
        [Header("[조준 타겟]")]
        public Transform Target = null;

        private void Awake()
        {
            Instance = this;
            //시작 하자마자 디스에이블 시키기
            Reset();
        }

        /// <summary>
        /// 초기화 코드
        /// </summary>
        public void Reset()
        {
            //해당 타겟 모델 디스에이븕
            Model.gameObject.SetActive(false);
            //타겟 널로 초기
            Target = null;
            //타겟 부모 나한테로 옴기기
            Model.transform.parent = transform;
            //모든 트윈 종료
            Model.transform.DOKill();
        }


        /// <summary>
        /// 타겟을 설정한다.
        /// </summary>
        /// <param name="target"></param>
        public void SetValue(Transform target,int scaleIndex = 0)
        {
            Target = target;

            //해당 타겟 모델 디스에이븕
            Model.gameObject.SetActive(true);

            //이미 타겟 되어있다면 종료
            if (Model.transform.parent.Equals(target)) return;

            Model.transform.parent = target;
            Model.transform.localPosition = DefaultLocalPosition;
            Model.transform.localRotation = Quaternion.Euler(DefaultLocalRotation);
            //추후 몬스터의 크기에 따라서 스케일 이 바뀌어야 한다
            Model.transform.localScale = DefaultLocalScales[scaleIndex];

            //스케일 트윈 세팅
            Model.transform.DOScale(DefaultLocalScales[scaleIndex] * 1.5f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

            //로테이션 트윈 세팅
            Vector3 rot = DefaultLocalRotation;
            rot.y = 30f;
            Model.transform.DOLocalRotate(rot, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
    }
}
