using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RootMotion.FinalIK;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 타겟을 조준 및 해제 시킴.
    /// </summary>
    public class LookAtAimedTarget : MonoBehaviour
    {
        [Header("[실제 AimIK에서 조준할 타겟이름]")]
        public string TargetName = "Aim IK Target";
        [Header("[캐리겉 컨트롤러로 조준을 시킴 -> 애니메이션 연관 (Auto->Awake())]")]
        public PlayerMoveActor PlayerMoveActor;
        [Header("[조준시 팔다리몸 IK -> Final IK (Auto->Awake())]")]
        public AimIK AimIK;
        [Header("[조준시 헤드 IK -> Final IK (Auto->Awake())]")]
        public LookAtIK LookAtIK;
        [Header("[Final IK 사용시 딱딱한 움직인 부드럽게 바꿔줌 (Auto->Awake())]")]
        public PlayerAimSwaper PlayerAimSwaper;
        


        private void Awake()
        {
            //Auto
            PlayerMoveActor = GetComponent<PlayerMoveActor>();
            AimIK = GetComponent<AimIK>();
            LookAtIK = GetComponent<LookAtIK>();
            PlayerAimSwaper = GetComponent<PlayerAimSwaper>();

            //이름으로 타겟 설정 초기화
            Initializ(TargetName);
        }

        /// <summary>
        /// AimIK 및 LookAtIK에서 사용될 타겟을 찾음
        /// </summary>
        /// <param name="aimTarget">찾으려는 타겟 이름</param>
        public void Initializ(string aimTarget)
        {
            Transform target = GameObject.Find(TargetName).transform;
            AimIK.solver.target = target;
            LookAtIK.solver.target = target;
            PlayerMoveActor.fireTarget = target;
        }


        /// <summary>
        /// 조준 시작
        /// </summary>
        public void Aim()
        {
            //플레이어 에니메이션 관련 컨트롤
            PlayerMoveActor.aimed = true;
            //Aim IK 컨트롤
            AimIK.enabled = true;
            //LookAt IK 컨트롤
            LookAtIK.enabled = true;
            //Final IK 를 부드럽게 만듬
            //PlayerAimSwaper.enabled = true;

            //플레이어 에임 스와퍼와 자연스러운 연결이 필요.
            //테스트 용으로 작업 필요함.
        }

        /// <summary>
        /// 조준 해제
        /// </summary>
        public void Release()
        {
            //플레이어 에니메이션 관련 컨트롤
            PlayerMoveActor.aimed = false;
            //Aim IK 컨트롤
            AimIK.enabled = false;
            //LookAt IK 컨트롤
            LookAtIK.enabled = false;
            //Final IK 를 부드럽게 만듬
            //PlayerAimSwaper.enabled = false;
        }

        #region [-TestCode]
        void Update()
        {
            //플레이어 몬스터 조준
            if (Input.GetKeyDown(KeyCode.O))
            {
                Aim();
            }

            //플레이어 몬스터 조준 해제
            if (Input.GetKeyDown(KeyCode.P))
            {
                Release();
            }
        }
        #endregion
    }
}
