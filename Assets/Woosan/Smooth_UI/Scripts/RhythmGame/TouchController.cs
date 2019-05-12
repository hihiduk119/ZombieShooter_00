using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Touch controller.
/// 2018.09.07
/// 좌우 방향으로 움직이는 터치 패드
/// 롤링스카이에서 사용된 터치 컨트롤러에 최대한 유사한 느낌 재현
/// 
/// float value = GetInputDirection() 값을 가져와서 
/// FixedUpdate()에서 Target.Translate(value); 이렇게 사용
/// </summary>

namespace WoosanStudio.RhythmGame
{

    public class TouchController : MonoBehaviour
    {
        public static TouchController instance;
        [HideInInspector] public bool bMouseDown;//마우스 다운 구분자
        [HideInInspector] public Vector3 mouseDownPos;//마우스 다운 position
        Vector3 downPos;//마우스 다운 position [스크린 좌표로 변환된]
        public Transform tfMouseDown;//터치의 시작점
        public Transform tfMouseDrag;//터치의 끝점

        //float gab = Screen.width / 5;//시작 포인트와 끝포인트의 최대 벌어질수 있는 거리 .
        float gab = 400f;//시작 포인트와 끝포인트의 최대 벌어질수 있는 거리 .
        float value;
        [Header("손가락 움직임에 의한 공의 이동 파워")]
        public float moveStrong = 2.5f;//손가락 움직임에 의해 움직이는 공의 이동 파워
        [Header("손가락 움직임에 의한 순간 가속 파워")]
        public float touchpadRecoverStrong = 7.5f;//손가락 움직임에 시작 포인트와 끝포인트의 거리를 좁히는 힘의 파워[강할수록 순간 가속과 감속이 빠르게 감소]

		private void Awake()
		{
            instance = this;
		}

		private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("마우스 클릭 했다");
                this.bMouseDown = true;
                this.mouseDownPos = Input.mousePosition;

                //UI 좌표계로 변환
                Vector3 pos = Input.mousePosition;
                pos.x -= Screen.width / 2;
                pos.y -= Screen.height / 2;
                this.downPos = this.tfMouseDown.localPosition = pos;
            }

            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("마우스 땟다");

                this.bMouseDown = false;
                this.mouseDownPos = new Vector3(-1, -1, -1);
                this.value = 0;
                this.tfMouseDrag.localPosition = this.tfMouseDown.localPosition;
            }

            if (this.bMouseDown)
            {
                Vector3 pos = Input.mousePosition;
                //UI 
                pos.y -= Screen.height / 2;
                //gab 차이 많큼 값만 리턴
                pos.y = Mathf.Clamp(pos.y, this.tfMouseDown.localPosition.y - gab, this.tfMouseDown.localPosition.y + gab);
                //실제 왼쪽 오른쪽 인지 값구하기
                this.value = pos.y - this.tfMouseDown.localPosition.y;
                //0-1로 만들기 [일종의 노말라이즈]
                this.value = this.value / gab;

                //y 값은 시작 위치와 같이 고정 시킴.
                pos.x = this.downPos.x;
                this.tfMouseDrag.localPosition = pos;
                //시작 포지션은 마지막 포지션을 따라 다님.
                this.tfMouseDown.localPosition = Vector3.Lerp(this.tfMouseDown.localPosition, this.tfMouseDrag.localPosition, Time.deltaTime * touchpadRecoverStrong);

                Debug.Log("distance = " + Vector3.Distance(this.tfMouseDown.localPosition, this.tfMouseDrag.localPosition));
            }
        }

        //터치에서 생성된 방향 및 힘을 Vector3로 얻어오는 부분
        public Vector3 GetInputDirection()
        {
            return new Vector3(this.value * moveStrong, 0, 0);
        }

        public float GetAcceleration()
        {
            return this.value;
        }
    }
}
