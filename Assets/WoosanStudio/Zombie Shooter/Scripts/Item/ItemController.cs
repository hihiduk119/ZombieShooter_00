using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 아이템의 컨트롤
    /// </summary>
    public class ItemController : MonoBehaviour
    {
        [Header("모델 [(Auto->ItemFactory.Make())]")]
        public GameObject Model;

        [Header("메인 이펙트 [(Auto->ItemFactory.Make())]")]
        public GameObject MainEffect;

        [Header("서브 이펙트 [(Auto->ItemFactory.Make())]")]
        public GameObject SubEffect;

        [Header("거리 체크기 [(Auto->ItemFactory.Make())]")]
        public DistanceCheck DistanceCheck;

        [Header("해당 타겟으로 이동 [(Auto->ItemFactory.Make())]")]
        public MoveToUITarget MoveToUITarget;

        /// <summary>
        /// 비활성화 시킴
        /// </summary>
        public void Deactivate()
        {
            Model.SetActive(false);
            MainEffect?.SetActive(false);
            SubEffect?.SetActive(false);
        }

        /// <summary>
        /// 활성화 이때 위치도 조정
        /// </summary>
        /// <param name="position">활성 위치 좌표</param>
        public void Activate(Vector3 position)
        {
            transform.position = position;

            Model.SetActive(true);
            MainEffect?.SetActive(true);
            SubEffect?.SetActive(true);
        }

        /// <summary>
        /// 아이템 획득 연출
        /// </summary>
        public void GetItem()
        {
            //메인 이펙트 비활성
            MainEffect?.SetActive(false);
            //서브 이펙트 비활성
            SubEffect?.SetActive(false);
            //이동 시작
            MoveToUITarget.Move();
        }

        /// <summary>
        /// 아이템 획득 완료
        /// </summary>
        public void GetItemComplete()
        {
            //모델 비활성화
            Model.SetActive(false);

            //오브젝트 풀 쓸지 말지 고민중..
            Destroy(this.gameObject);
        }

        #region [-TestCode]
        void Update()
        {
            //아이템 획득 테스트
            if (Input.GetKeyDown(KeyCode.H))
            {
                GetItem();
            }
        }
        #endregion
    }
}
