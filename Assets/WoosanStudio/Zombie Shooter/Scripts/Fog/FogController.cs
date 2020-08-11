using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 게임내 안개의 값을 조정
    /// </summary>
    public class FogController : MonoBehaviour , IStart , IEnd
    {
        [Header("[안개의 밀도 값]")]
        public List<float> DencityList = new List<float>();
        [Header("[트윈 지속시간]")]
        public float Duration = 0.25f;
        
        public UnityEvent StartEvent => startEvent;
        public UnityEvent EndEvent => endEvent;

        [SerializeField]
        [Header("[안개 트윈 시작 이벤트]")]
        private UnityEvent startEvent = new UnityEvent();
        [SerializeField]
        [Header("[안개 트윈 끝 이벤트]")]
        private UnityEvent endEvent = new UnityEvent();

        private Tween FogDencity;

        /// <summary>
        /// 밀도를 변경 합니다.
        /// </summary>
        public void DencityChange(int index)
        {
            //시작이벤트 실행
            startEvent?.Invoke();

            FogDencity.Kill();
            //트윈 시작
            FogDencity = DOTween.To(()=> RenderSettings.fogDensity,
                (x) => RenderSettings.fogDensity = x, DencityList[index], Duration);

            //끝 이벤트 실행
            FogDencity.OnComplete(() => endEvent?.Invoke());
        }

        #region [-TestCode]
        //안개 변경 코드
        //void Update()
        //{
        //    //짙은 안개 설정
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        DencityChange(0);
        //    }

        //    //옅은 안개 설정
        //    if (Input.GetKeyDown(KeyCode.H))
        //    {
        //        DencityChange(1);
        //    }
        //}
        #endregion
    }
}
