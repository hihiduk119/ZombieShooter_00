using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DarkTonic.MasterAudio;

namespace WoosanStudio.ZombieShooter.Player
{
    /// <summary>
    /// 플레이어 이펙트 담당
    /// </summary>
    public class MoveEffect : MonoBehaviour
    {
        ///캐슁용 변수들
        //버티컬
        //float v;
        //호라이즌
        //float h;

        [Header("[생성 이펙트]")]
        public GameObject Prefab;

        [Header("[이펙트 발생 최소 값]")]
        public float MinPower = 0.2f;

        float movePower = 0;
        Vector2 moveVector;
        GameObject clone;

        private State state = State.Stop;
        private WaitForSeconds WFS = new WaitForSeconds(0.2f);

        public enum State
        {
            Stop = 0,
            Move,
        }

        private void Awake()
        {
            StartCoroutine(FSM());
        }

        IEnumerator FSM ()
        {
            while(true)
            {
                yield return StartCoroutine(state.ToString());
            }
        }

        IEnumerator Stop()
        {
            WFS = new WaitForSeconds(0.05f);

            while(true)
            {
                yield return WFS;//0.2f 초간 대기
                if (GetMovePower() > MinPower)
                {
                    ////움직임 발생시 상태 변경
                    state = State.Move;
                    //탈출
                    yield break;
                }
            }
        }

        IEnumerator Move()
        {
            WFS = new WaitForSeconds(0.25f); ;//0.2f 초간 대기
            while (true)
            {
                yield return WFS;//0.2f 초간 대기
                if (GetMovePower() <= MinPower)
                {
                    ////움직임 발생시 상태 변경
                    state = State.Stop;
                    //탈출
                    yield break;
                } else
                {
                    clone = Instantiate(Prefab) as GameObject;
                    clone.transform.position = this.transform.position;

                    MasterAudio.FireCustomEvent("CustomEvent_PlayerFootsteps", this.transform);
                }
            }
        }

        float GetMovePower()
        {
            float movePower = 0;

            //조이스틱 방향 가져오기
            moveVector.x = UltimateJoystick.GetHorizontalAxis("Move");
            moveVector.y = UltimateJoystick.GetVerticalAxis("Move");

            //벡터의 크기로 움직임 파워 계산
            movePower = moveVector.SqrMagnitude();

            return movePower;
        }


        /*
        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                MasterAudio.FireCustomEvent("CustomEvent_PlayerFootsteps", this.transform);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                MasterAudio.FireCustomEvent("CustomEvent_FireAssaultRifleWithBullet", this.transform);
            }
        }
        #endregion
        */

        /*private void FixedUpdate()
        {
            //조이스틱 방향 가져오기
            moveVector.x = UltimateJoystick.GetHorizontalAxis("Move");
            moveVector.y = UltimateJoystick.GetVerticalAxis("Move");

            //벡터의 크기로 움직임 파워 계산
            movePower = moveVector.SqrMagnitude();
            Debug.Log("움직임 파워 = " + movePower);

            //최소 값보다 크면 이펙트 생성
            //사운드도 ??
            if(movePower > MinPower)
            {
                clone = Instantiate(Prefab) as GameObject;
                clone.transform.position = this.transform.position;
            }
        }*/
    }
}
