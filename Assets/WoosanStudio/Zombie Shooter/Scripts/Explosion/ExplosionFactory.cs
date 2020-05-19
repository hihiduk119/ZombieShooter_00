using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 화면 전체에 폭발을 만듬.
    /// </summary>
    public class ExplosionFactory : MonoBehaviour
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

        //실제 폭발 반경을 위해 카메라와의 거리
        //**LevelSwapController2.distance 와 값을 마춰라
        public float distance = 80f;

        [Header("[폭발 프리팹 세팅 리스튼")]
        public List<ExplosionSetting> settings = new List<ExplosionSetting>();

        //폭발 위치의 루트
        public Transform ExplosionRoot;

        //폭발 포지션
        private List<Transform> points;

        //캐쉬
        private Vector3 size = Vector3.zero;

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
                case 90: pos.z += distance;  break;
                case 180: pos.x += distance; break;
                case 270: pos.z -= distance; break;
                case 0: pos.x -= distance; break;
            }

            //폭발루트에 변경된 좌표와 회전값 넣/
            explosionRoot.position = pos;
            //카메라 회전 y축 각도 그대로 적용
            rot.y = Camera.localRotation.eulerAngles.y;
            explosionRoot.localRotation = Quaternion.Euler(rot);
        }

        /// <summary>
        /// 실제 폭파 연출 호출
        /// </summary>
        /// <param name="setting"></param>
        public void Show(ExplosionSetting setting)
        {
            //폭탄위치 계산
            CalculateExplosionPosition(ExplosionRoot, Camera);
            // 폭탄을 터트리는 연출.
            StartCoroutine(SequentialShow(setting));
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
            while(points.Count > index)
            {
                //폭탄의 형태를 랜덤하게 하기위해 사용
                int rand = UnityEngine.Random.Range(1, setting.prefabs.Length);
                GameObject clone = Instantiate(setting.prefabs[rand]);

                clone.transform.position = points[index].position;

                yield return new WaitForSeconds(0.06f);
                index++;
            }
        }

        #region [-TestCode]
        //해당 영역에 폭탈을 떨어뜨림.
        public void TestRun()
        {
            Show(settings[(int)ExplosionType.RedOriginalFire]);
            GlobalDamageController.Instance.DoDamage(1000);
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        TestRun();
        //    }
        //}
        #endregion


        //폭파 영역을 미리 확인하기위해 사용 빌드시 제거 필요
        #region [-TestCode]
        
        void OnDrawGizmosSelected()
        {
            //박스 사이즈로 높이를 결정.
            size.y = 0;

            //일단 카메라 포지션 기준
            MyExplosionRect.Position = Camera.position;
            //보정 값
            MyExplosionRect.Position.y = 0;

            //회전 각에 따라 Distance도 다르게 적용.
            //회전 각에 따라 영역의 가로 새로도 변경
            switch ((int)Camera.localRotation.eulerAngles.y)
            {
                case 90:
                    MyExplosionRect.Position.z += distance;
                    size.z = MyExplosionRect.Area.x;
                    size.x = MyExplosionRect.Area.y;
                    break;
                case 180:
                    MyExplosionRect.Position.x += distance;
                    size.x = MyExplosionRect.Area.x;
                    size.z = MyExplosionRect.Area.y;
                    break;
                case 270:
                    MyExplosionRect.Position.z -= distance;
                    size.z = MyExplosionRect.Area.x;
                    size.x = MyExplosionRect.Area.y;
                    break;
                case 0:
                    MyExplosionRect.Position.x -= distance;
                    size.x = MyExplosionRect.Area.x;
                    size.z = MyExplosionRect.Area.y;
                    break;
            }


            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(MyExplosionRect.Position, size);
            //Gizmos.DrawCube(MyExplosionRect.Position, size);
            //Gizmos.DrawSphere(MyExplosionRect.Position,40);

            //테스트 타겟의 위치 조정
            //TestTarget.position = MyExplosionRect.Position;

        }
        #endregion
    }
}
