using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RootMotion.FinalIK;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// Final Ik 에서 에임 타겟을 부드럽게 다른 타겟으로 조준 하는 스크립트
    /// 조준변경시 AimTarget 만 변경하면 됨
    /// </summary>
    public class PlayerAimSwaper : MonoBehaviour
    {
        private AimIK mAimIK;
        //실제 조준 되고 있는 타겟
        public Transform mAimIKTarget;

        [Header("[조준 시키려는 타겟]")]
        public Transform AimTarget;
        [Header("[조준 변환 속도]")]
        public float Speed = 0.1f;

        [Header("[조준할 타겟을 찾아줌. (Auto->Awake())]")]  
        public FindAimTarget FindAimTarget;

        [Header("[추가 값]")]
        public Vector3 ExtraValue;

        //테스트용
        //public List<Transform> TestTarget = new List<Transform>();
        //public int TestIndex = 0;

        private void Awake()
        {
            mAimIK = GetComponent<AimIK>();
            FindAimTarget = GetComponent<FindAimTarget>();

            //Aim IK 에 타겟이 있는지 확인,
            if (mAimIK.solver.target != null)
            {
                mAimIKTarget = mAimIK.solver.target;
            } else
            {
                Debug.Log("에임 타겟이 존제하지 않습니다.");
            }
        }

        /// <summary>
        /// 조준하기
        /// </summary>
        /// <param name="target">스왑 시킬 타겟</param>
        private void Aiming(Transform target)
        {
            if(mAimIKTarget == null)
            {
                mAimIKTarget = mAimIK.solver.target;
            }
            if (target == null) return;

            mAimIKTarget.position = Vector3.Lerp(mAimIKTarget.position, target.position, Speed);
        }

        /// <summary>
        /// 즉시 조준하기
        /// </summary>
        /// <param name="target">조준할 타겟</param>
        public void ImmediatelyAiming(Transform target)
        {
            if (mAimIKTarget == null)
            {
                mAimIKTarget = mAimIK.solver.target;
            }
            if (target == null) return;

            mAimIKTarget.position = target.position;
        }

        private void FixedUpdate() 
        {
            Aiming(FindAimTarget.target);
        }

        #region [-TestCode]
        void Update()
        {
            //조준하기
            Aiming(AimTarget);

            //테스트용 타겟을 변경해서 타겟 조정
            //if (Input.GetKeyDown(KeyCode.T))
            //{
            //         = TestTarget[TestIndex];
            //    TestIndex++;

            //    if (TestIndex >= TestTarget.Count) { TestIndex = 0; }
            //}
        }
        #endregion

    }
}
