using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 에어 스트라이크를 연출 전체룰 컨트롤 함.
    /// </summary>
    public class AirStrikeController : MonoBehaviour
    {
        public Transform StartPoint;
        public Transform EndPoint;
        public Transform Target;

        public void SetPoint(Transform startPoint, Transform endPoint)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }

        /// <summary>
        /// 실제 연출 수행
        /// </summary>
        public void Run()
        {
            Target.position = StartPoint.position;

            Target.DOMove(EndPoint.position, 1.5f).SetEase(Ease.Linear).OnComplete(() => {
                Debug.Log("OnComplete");
                ExplosionFactory.Instance.TestRun();
            });
        }


        #region [-TestCode]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Run();
            }
        }
        #endregion
    }
}
