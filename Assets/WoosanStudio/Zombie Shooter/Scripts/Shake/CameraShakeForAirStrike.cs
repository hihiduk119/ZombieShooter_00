using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Thinksquirrel.CShake;

namespace WoosanStudio.ZombieShooter
{
    public class CameraShakeForAirStrike : MonoBehaviour
    {
        [Header("[반복 횟수")]
        public int MaxCount = 6;

        //반복 카운트
        private int count = 0;
        //쉐이커
        private Thinksquirrel.CShake.CameraShake shake;
        private Coroutine shakeCoroutine;
        //0.5초 단위
        private WaitForSeconds WFS = new WaitForSeconds(0.3f);

        private void Awake()
        {
            shake = GetComponent<Thinksquirrel.CShake.CameraShake>();
        }

        /// <summary>
        /// 쉐이크 시작
        /// </summary>
        public void OnShakeCamera()
        {
            if(shakeCoroutine != null) { StopCoroutine(shakeCoroutine); }
            shakeCoroutine = StartCoroutine(ShakeCoroutine());
        }

        /// <summary>
        /// 카운트 횟수 만큼 딜레이를 걸어서 쉐이크
        /// </summary>
        /// <returns></returns>
        IEnumerator ShakeCoroutine()
        {
            count = 0;
            yield return new WaitForSeconds(1.2f);

            while (count < MaxCount)
            {
                shake.Shake();
                yield return WFS;
                count++;
            }
        }

        #region [-TestCode]
        //void Update()
        //{
        //    //0.5 초 단위로 5번 흔
        //    if (Input.GetKeyDown(KeyCode.N))
        //    {
        //        OnShakeCamera();
        //    }
        //}
        #endregion

    }
}
