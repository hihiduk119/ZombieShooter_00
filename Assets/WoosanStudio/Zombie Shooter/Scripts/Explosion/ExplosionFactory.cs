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

        [Serializable]
        public struct ExplosionRect
        {
            public Vector2 Area;
            public Vector3 Position;
        }

        //폭파 반경
        public ExplosionRect MyExplosionRect;

        //실제 폭발 반경을 위해 카메라와의 거리
        public float distance = 75f;

        [Header("[폭발 프리팹 세팅 리스튼")]
        public List<ExplosionSetting> settings = new List<ExplosionSetting>();

        public Transform Root;

        //폭발 포지션
        private List<Transform> points;

        //캐쉬
        private Vector3 size = Vector3.zero;



        private void Awake()
        {
            //싱글톤 생
            Instance = this;

            points = new List<Transform>(Root.GetComponentsInChildren<Transform>());

            //points.ForEach(value => Debug.Log(value.name));
        }

        public void Show(ExplosionSetting setting)
        {
            StartCoroutine(SequentialShow(setting));
        }

        IEnumerator SequentialShow(ExplosionSetting setting)
        {
            int index = 0;
            while(points.Count > index)
            {
                int rand = UnityEngine.Random.Range(1, setting.prefabs.Length);
                GameObject clone = Instantiate(setting.prefabs[rand]);
                //GameObject clone2 = Instantiate(settings[6].prefabs[rand]);

                clone.transform.position = points[index].position;
                //clone2.transform.position = points[index].position;

                yield return new WaitForSeconds(0.05f);
                index++;
            }
        }

        #region [-TestCode]
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
        }
        #endregion
    }
}
