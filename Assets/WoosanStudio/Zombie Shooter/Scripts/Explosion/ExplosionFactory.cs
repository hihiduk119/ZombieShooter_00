﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 화면 전체에 폭발을 만듬.
    /// </summary>
    public class ExplosionFactory : MonoBehaviour , ISequential
    {
        //싱글톤 패턴 인스턴스
        static public ExplosionFactory Instance;
        //폭발 객체의 종류
        public enum ExplosionType
        {
            BlueDemonFire,
            FunkyMixJokerFire,
            GreenWildFire,
            PurpleDarkFire,
            RedOriginalFire,
            RedOriginalFireNoSmoke,
            ClassicHeavyBomb,
        }

        public Transform Camera;
         
        #region [-TestCode] 폭탄영영의 중심점 확인용
        //public Transform TestTarget;
        #endregion

        [Serializable]
        public struct ExplosionRect
        {
            public Vector2 Area;
            public Vector3 Position;
        }

        //폭파 반경
        public ExplosionRect MyExplosionRect;

        //**LevelSwapController2.distance 와 값을 마춰라
        [Header("[실제 폭발 반경을 위해 카메라와의 거리]")]
        public float distance = 80f;

        [Header("[포벌의 반경의 높이]")]
        public float Height = 0f;

        [Header("[폭발 프리팹 세팅 리스튼")]
        public List<ExplosionSetting> settings = new List<ExplosionSetting>();

        //폭발 위치의 루트
        public Transform ExplosionRoot;

        //폭발 포지션
        private List<Transform> points;

        //캐쉬
        private Vector3 size = Vector3.zero;

        #region [ISequential Implement]
        [SerializeField]
        private UnityEvent mStartEvent = new UnityEvent();
        [SerializeField]
        private UnityEvent mMidpointEvent = new UnityEvent();
        [SerializeField]
        private UnityEvent mEndEvent = new UnityEvent();
        //폭격 시작 이벤
        public UnityEvent StartEvent => mStartEvent;
        //폭격 중간 이벤트
        public UnityEvent MidpointEvent => mMidpointEvent;
        //폭격 끝 이벤트
        public UnityEvent EndEvent => mEndEvent;
        #endregion

        //보이기 위해 영역 그리기용
        Transform mExplosionRoot;

        private void Awake()
        {
            //싱글톤 생
            Instance = this;

            //폭파 위치를 가져옴
            points = new List<Transform>(ExplosionRoot.GetComponentsInChildren<Transform>());

            //폭파 위치에서 첫번째는 Parent위치이기 때문에 삭제
            points.RemoveAt(0);

            //points.ForEach(value => Debug.Log(value.name));
        }

        /// <summary>
        /// 폭탄 위치 재계산
        /// </summary>
        void CalculateExplosionPosition(Transform explosionRoot,Transform camera,float height = 0)
        {
            mExplosionRoot = explosionRoot;

            //박스 사이즈로 높이를 결정.
            size.y = 0;

            //일단 카메라 포지션 기준
            explosionRoot.position = camera.position;
            //보정 값
            Vector3 pos = explosionRoot.position;
            //높이가 있다면 높이 적용 => 고가 도로 맵에
            pos.y = height;

            //카메라 각도에 따라 폭발 루트를 회전시키기 위해 미리 받아둠.
            Vector3 rot = explosionRoot.localRotation.eulerAngles;

            //회전 각에 따라 Distance도 다르게 적용.
            //회전 각에 따라 영역의 가로 새로도 변경
            switch ((int)Camera.localRotation.eulerAngles.y)
            {
                //case 90: pos.z += distance;  break;
                //case 180: pos.x += distance; break;
                //case 270: pos.z -= distance; break;
                //case 0: pos.x -= distance; break;

                case 0:
                    pos.z += distance;
                    break;
                case 90:
                    pos.x += distance;
                    break;
                case 180:
                    pos.z -= distance;
                    break;
                case 270:
                    pos.x -= distance;
                    break;
            }

            //폭발루트에 변경된 좌표와 회전값 넣/
            explosionRoot.position = pos;

            //카메라 회전 y축 각도 그대로 적용
            rot.y = Camera.localRotation.eulerAngles.y;
            //폭발 영역 자체가 90도 틀어져있기에 90추가 더
            rot.y += 90;
            explosionRoot.localRotation = Quaternion.Euler(rot);
        }

        /// <summary>
        /// 실제 폭파 연출 호출
        /// </summary>
        public void Show()
        {
            //폭탄위치 계산
            CalculateExplosionPosition(ExplosionRoot, Camera, Height);
            // 폭탄을 터트리는 연출.
            StartCoroutine(SequentialShow(settings[(int)ExplosionType.RedOriginalFire]));
        }

        /// <summary>
        /// 폭탄을 터트리는 연출.
        /// 0.05초의 시간차로 폭파.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        IEnumerator SequentialShow(ExplosionSetting setting)
        {
            int index = 0;
            //시작에 이벤트 발생
            StartEvent.Invoke();

            while(points.Count > index)
            {
                //폭탄의 형태를 랜덤하게 하기위해 사용
                int rand = UnityEngine.Random.Range(1, setting.prefabs.Length);
                GameObject clone = Instantiate(setting.prefabs[rand]);

                clone.transform.position = points[index].position;

                //폭격 중간쯤에 중간 이벤트 발생
                if(index == (int)(points.Count/2))
                {
                    MidpointEvent.Invoke();
                }

                yield return new WaitForSeconds(0.06f);
                index++;
            }

            //끝에 이벤트 발생
            EndEvent.Invoke();
        }



        #region [-TestCode]
        //해당 영역에 폭탈을 떨어뜨림.
        //public void TestRun()
        //{
        //    //폭탄이 터지는 연출
        //    Show();
        //    //전체 오브젝트에 데미지를 줌
        //    GlobalDamageController.Instance.DoDamage();
        //}

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        TestRun();
        //    }
        //}
        #endregion
    }
}
