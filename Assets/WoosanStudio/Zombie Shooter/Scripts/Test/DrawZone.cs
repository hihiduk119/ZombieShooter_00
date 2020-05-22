using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace WoosanStudio.ZombieShooter
{
    public class DrawZone : MonoBehaviour
    {
        public Transform Camera;

        [Serializable]
        public struct DisplayRect
        {
            public Vector2 Area;
            public Vector3 Position;
        }

        //캐쉬
        private Vector3 size = Vector3.zero;

        //폭파 반경
        public DisplayRect MyDisplayRect;

        //실제 폭발 반경을 위해 카메라와의 거리
        //**LevelSwapController2.distance 와 값을 마춰라
        public float distance = 80f;

        //폭파 영역을 미리 확인하기위해 사용 빌드시 제거 필요
        //해당 사각형은 눈으로 보는 확인용으로 실제 특정한 곳에 사용돼지는 않는다
        #region [-TestCode]

        void OnDrawGizmosSelected()
        {
            //박스 사이즈로 높이를 결정.
            size.y = 0;

            //일단 카메라 포지션 기준
            MyDisplayRect.Position = Camera.position;
            //보정 값
            MyDisplayRect.Position.y = 0;

            //회전 각에 따라 Distance도 다르게 적용.
            //회전 각에 따라 영역의 가로 새로도 변경
            switch ((int)Camera.localRotation.eulerAngles.y)
            {
                case 90:
                    MyDisplayRect.Position.z += distance;
                    size.z = MyDisplayRect.Area.x;
                    size.x = MyDisplayRect.Area.y;
                    break;
                case 180:
                    MyDisplayRect.Position.x += distance;
                    size.x = MyDisplayRect.Area.x;
                    size.z = MyDisplayRect.Area.y;
                    break;
                case 270:
                    MyDisplayRect.Position.z -= distance;
                    size.z = MyDisplayRect.Area.x;
                    size.x = MyDisplayRect.Area.y;
                    break;
                case 0:
                    MyDisplayRect.Position.x -= distance;
                    size.x = MyDisplayRect.Area.x;
                    size.z = MyDisplayRect.Area.y;
                    break;
            }


            Gizmos.color = Color.red;
            //Gizmos.DrawWireCube(MyDisplayRect.Position, size);
            Gizmos.DrawCube(MyDisplayRect.Position, size);
            //테스트 타겟의 위치 조정
            //TestTarget.position = MyExplosionRect.Position;

        }
        #endregion
    }
}
