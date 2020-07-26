using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 머즐 플레쉬를 껏다 켰다 하는 컨트롤러
    /// </summary>
    public class MuzzleFlash : MonoBehaviour , IMuzzleFlare
    {
        [Header("[최초 생성시 로컬 포지션]")]
        public Vector3 InitLocalPosition = new Vector3(1.9f, 1.5f, 0);
        [Header("[최초 생성시 로컬 로테이션]")]
        public Vector3 InitLocalRotation = new Vector3(90, 90, 0);

        //껏다 켯다 하기 위한 코루티
        private Coroutine blinkCoroutine;
        //private WaitForEndOfFrame mWFEF = new WaitForEndOfFrame();

        //*실제 사격되는 발사체보다 빠르면 Blink가 정지한듯 보일수 있다.주의 필요.
        private WaitForSeconds mWFS = new WaitForSeconds(0.05f);
        //자동 할당 (Auto->Awake())
        private Projector projector;

        private void Awake()
        {
            projector = GetComponent<Projector>();
            //생성시 디스에이블
            projector.enabled = false;
        }

        /// <summary>
        /// 최초 포지션 및 로테이션 초기화
        /// </summary>
        public void Initialize()
        {
            //로컬 포지션 적용.
            transform.localPosition = InitLocalPosition;
            //로컬 로테이션 적용.
            transform.localRotation = Quaternion.Euler(InitLocalRotation);
        }

        /// <summary>
        /// 머즐 플레쉬 활성
        /// </summary>
        public void Active()
        {
            if (blinkCoroutine != null)
            {                
                //코루틴 정지
                StopCoroutine(blinkCoroutine);
            }

            //코루틴 시작
            blinkCoroutine = StartCoroutine(BlinkCoroutine());
        }

        /// <summary>
        /// 머즐 플레쉬 비활성
        /// </summary>
        public void Deactive()
        {
            if (blinkCoroutine != null)
            {
                //코루틴 정지
                StopCoroutine(blinkCoroutine);
                //프로젝터 디스에이블
                projector.enabled = false;
            }
        }

        /// <summary>
        /// 한프레임 프로젝터 깜빡임
        /// </summary>
        /// <returns></returns>
        IEnumerator BlinkCoroutine()
        {
            //Debug.Log("Blink A");
            //프로젝터 인에이블
            projector.enabled = true;
            //한 프레임 대기
            yield return mWFS;
            //프로젝터 디스에이블
            projector.enabled = false;
            //Debug.Log("Blink B");
        }

        //======================== [IMuzzleFlare Implement] ========================
        public void Blink()
        {
            Active();
        }
    }
}
