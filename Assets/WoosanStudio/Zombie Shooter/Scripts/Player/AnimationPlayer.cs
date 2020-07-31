using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public class AnimationPlayer : MonoBehaviour
    {
        private Animator animator;

        [Header("[실행할 에니메이션 트리거 이름]")]
        public List<string> AnimationList = new List<string>();

        [Header("[자동으로 랜덤하게 실행]")]
        public bool bAutoPlay = true;

        private Coroutine autoPlayCorutine;
        private WaitForSeconds WFS = new WaitForSeconds(5f);
        private bool bIdle = false;

        private int TestIndex = 0;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            //자동실행 체크시 자동으로 에니메이션 시작
            if (bAutoPlay) { AutoStart(); }
        }

        /// <summary>
        /// 자동 실행 시작
        /// </summary>
        public void AutoStart()
        {
            if (autoPlayCorutine != null) StopCoroutine(autoPlayCorutine);
            autoPlayCorutine = StartCoroutine(AutoPlayCorutine());
        }

        IEnumerator AutoPlayCorutine()
        {
            while(true)
            {
                if(bIdle) //기본 Idle시작
                {
                    Reset();
                } else //랜덤 에니메이션 시작
                {
                    Play(AnimationList[Random.Range(0, AnimationList.Count)]);
                }

                yield return WFS;

                //Idle상태와 비Idle상태를 번갈아 가며 실행.
                bIdle = !bIdle;
            }
        }


        /// <summary>
        /// 에니메이션 초기화
        /// </summary>
        private void Reset()
        {
            animator.Play("Idle", -1, 0f);
        }

        /// <summary>
        /// 해당 에니메이션 실행
        /// </summary>
        /// <param name="name"></param>
        public void Play(string name)
        {
            // 에니메이션 초기화
            Reset();
            //해당 에니메이션 트리거 실행
            animator.SetTrigger(name);

            Debug.Log("플레이 => " + name);
        }

        #region [-TestCode]
        /// <summary>
        /// 해당 에니메이션 실행
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Play(AnimationList[TestIndex]);

                TestIndex++;
                if(TestIndex >= AnimationList.Count)
                {
                    TestIndex = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Play(AnimationList[TestIndex]);

                TestIndex--;
                if(TestIndex < 0)
                {
                    TestIndex = AnimationList.Count - 1;
                }
            }
        }
        #endregion
    }
}
