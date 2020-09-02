using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WoosanStudio.Common;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터들의 위치를 알기위해 따로 보관함.
    /// AI 플레이어의 자동 타겟을 찾기위해 사용됨.
    /// </summary>
    public class MonsterList : MonoBehaviour
    {
        static public MonsterList Instance;

        [Header("[몬스터 리스트]")]
        public List<Transform> Items = new List<Transform>();

        //[Header("[몬스터 리스트가 0일때 비었다는 이벤트를 발생할지 말지 결정]")]   
        //public bool UseEmptyEvent = false;

        [Header("[몬스터 리스트가 비었다 이벤트]")]
        public UnityEvent ListEmptyEvent = new UnityEvent();

        //캐쉬
        Coroutine checkListCoroutine;
        //0.5초 단위로 체크
        WaitForSeconds WFS = new WaitForSeconds(0.5f);
        
        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 몬스터 리스트가 비었을때 이벤트 발생 활성화
        /// </summary>
        public void ActiveEmptyEvent() 
        {
            //UseEmptyEvent = true;
            //0.5초 단위로 체크하는 코루틴 생성
            checkListCoroutine = StartCoroutine(CheckListCoroutine());
        }

        /// <summary>
        /// 몬스터 리스트가 루프 체크 하며 Empty에서 이벤트 발생
        /// </summary>
        /// <returns></returns>
        IEnumerator CheckListCoroutine()
        {
            while(true)
            {
                yield return WFS;

                //몬스터 리스트가 0이라면 
                if(Items.Count == 0 )
                {
                    //몬스터가 없다는 이벤트 발생
                    ListEmptyEvent.Invoke();
                    //한번만 실행되기에 모든 리스너 제거
                    ListEmptyEvent.RemoveAllListeners();

                    yield break;
                }
            }
        }
    }
}
