using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class PositionMover : MonoBehaviour
    {
        [Header ("[이동 시킬 타겟]")]
        public Transform Target;

        [Header("[이동 시킬 좌표 리스트]")]
        public List<Transform> posList = new List<Transform>();

        [Header("[현재 좌표]")]
        public int CurrentPosIndex = -1;

        [Header("[이동 시킬 좌표]")]
        public int NextPosIndex;

        //이동완료 이벤트 호출
        public UnityEvent DoneEvent = new UnityEvent();

        public Ease EaseType = Ease.Linear;

        public float Duration = 2f;

        public void Move(int index)
        {
            Target.DOMove(this.posList[index].position, Duration).SetEase(EaseType).OnComplete(() => DoneEvent.Invoke());
        }

        #region [-TestCode]
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                Move(NextPosIndex);
            }
        }
        #endregion
    }
}
