using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.Common
{
    public class SlowMotionTimeManager : MonoBehaviour
    {
        //싱글톤 패넡
        static public SlowMotionTimeManager Instance;

        //기존 코드 0.2f => 0.5f변경 후 문제 발생 여부 확인 해야함. 
        //public float slowdownFactor { get; private set; } = 0.2f;
        //get 의 초기화를 위해 사용.
        public float slowdownFactor { get; private set; } = 0.5f;
        //public float recoverFactor{ get; private set; } = 1.5f;

        //인터페이스로 만든 오디
        public new WoosanStudio.Common.Audio.IAudio audio;

        private float defaultFixedDeltaTime = 1f;

        private bool isSlow = false;

        IEnumerator doSlowMotionCoroutine;

        private void Awake()
        {
            //defaultFixedDeltaTime = Time.fixedDeltaTime;

            //싱글톤 패넡
            Instance = this;

            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        /// <summary>
        /// 슬로우 시작 1초후 원복
        /// </summary>
        public void DoSlow(Vector3 position)
        {
            Debug.Log("===============슬로우 발생");
            if (doSlowMotionCoroutine != null) { StopCoroutine(doSlowMotionCoroutine); }
            StartCoroutine(doSlowMotionCoroutine = DoSlowMotionCoroutine(0.5f));
        }

        /// <summary>
        /// 슬로우 시작 1초후 원복
        /// </summary>
        IEnumerator DoSlowMotionCoroutine(float second)
        {
            DoSlowMotion();
            yield return new WaitForSeconds(second);
            Rollback();
        }

        /// <summary>
        /// 슬로우 모션을 발생 시키는 부분
        /// </summary>
        public void DoSlowMotion() {
            Time.timeScale = slowdownFactor;
            //Time.fixedDeltaTime 는 0.02시간에 한번씩 계산하기 때문에 변환 time scale 변경시 같이 해줘야함
            //*FixedUpdate에 구현된 부분이 여기에 영향을 받음.
            //안그러면 오브젝트가 끊기는 것처럼 날아감 보임
            Time.fixedDeltaTime = 0.02f * slowdownFactor;

            //느린 사운드 적용 부분 [둘중에 하나 써야]
            //인터페이스로 구현
            //audio.Pitch(Time.timeScale);
            //실제 호출
            //AudioManager.instance.SlowMotion(Time.timeScale);
        }

        /// <summary>
        /// 원래로 복귀
        /// </summary>
        public void Rollback()
        {
            Time.timeScale = 1f;
            //Time.fixedDeltaTime 는 0.02시간에 한번씩 계산하기 때문에 변환 time scale 변경시 같이 해줘야함
            //안그러면 오브젝트가 끊기는 것처럼 날아감 보임
            //Time.fixedDeltaTime = defaultFixedDeltaTime;
            Time.fixedDeltaTime = 0.02f;
        }

        #region [-TestCode]
        public void Swap()
        {
            isSlow = !isSlow;
            if(isSlow)
            {
                DoSlowMotion();
            } else
            {
                Rollback();
            }
        }


        /*private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) {
                //씬안의 모든 리지드 바디 가져와서 벨로시티 저장
                DoSlowMotion();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Rollback();
                //Debug.Log("[3] time = " + Time.timeScale + "   Time.fixedDeltaTime = " + Time.fixedDeltaTime);
            }

            if(Input.GetKeyDown(KeyCode.P)) {
                //AudioManager.Instance.MusicLoop(SoundLoop.LetsRock);
            }
        }*/
        #endregion
    }
}
