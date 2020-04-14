using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using WoosanStudio.Common;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 화면내 모든 몬스터에 데미지를 주는 클래스
    /// 폭격 연출시 호출
    ///
    /// **현재는 barrier만 제외해서 데미지 주는 형태로 되어 있지만
    /// 몬스터와 프랍데이터 에만 데미지를 주는 형태로 바꿔야 할수도 있다.
    /// </summary>
    public class GlobalDamageController : MonoBehaviour
    {
        //싱글톤 패턴 인스턴스
        static public GlobalDamageController Instance;
        //씬 전체의 IHaveHit
        public List<IHaveHit> haveHits = new List<IHaveHit>();
        //씬 전체의 IHaveHealth
        public List<IHaveHealth> haveHealths = new List<IHaveHealth>();

        //캐쉬용
        GameObject[] barriers;
        List<IHaveHit> compareHaveHits = new List<IHaveHit>();
        List<IHaveHealth> compareHaveHealths = new List<IHaveHealth>();

        private void Awake()
        {
            //싱글톤 인스턴스 생성
            Instance = this;
        }

        /// <summary>
        /// 초기화시
        /// 씬 전체의 IHaveHit를 가져옴
        /// 씬 전체의 IHaveHealth 가져옴
        ///
        /// 데미지를 줄 오브젝트 재계
        /// </summary>
        void Initialize()
        {
            //호출시 마다 모든 리스트 클리ㅇ
            haveHits.Clear();
            haveHealths.Clear();
            compareHaveHits.Clear();
            compareHaveHealths.Clear();

            //barrier 는 제외하기 위해 해당 리스트 가져오기
            this.barriers = GameObject.FindGameObjectsWithTag("Barrier");

            //barrier 에서 IHaveHit를 가져오기
            this.compareHaveHits = new List<IHaveHit>();
            foreach (GameObject barrier in barriers) { this.compareHaveHits.Add(barrier.GetComponent<IHaveHit>());}
            //Debug.Log("barriers HaveHit " + compareHaveHits.Count);

            //barrier 에서 IHaveHealth 가져오기
            this.compareHaveHealths = new List<IHaveHealth>();
            foreach (GameObject barrier in barriers) { this.compareHaveHealths.Add(barrier.GetComponent<IHaveHealth>()); }

            //씬 전체의 IHaveHit를 가져옴
            var list = FindObjectsOfType<MonoBehaviour>().OfType<IHaveHit>();
            foreach (IHaveHit item in list) { this.haveHits.Add(item); }

            //씬 전체의 IHaveHealth 가져옴
            var list2 = FindObjectsOfType<MonoBehaviour>().OfType<IHaveHealth>();
            foreach (IHaveHealth item in list2) { this.haveHealths.Add(item); }

            //씬 전체에서 가져온 IHaveHit에서 Barrier부분 제거
            ListUtililty.RemoveList<IHaveHit>(this.haveHits, this.compareHaveHits);

            //씬 전체에서 가져온 IHaveHealth Barrier부분 제거
            ListUtililty.RemoveList<IHaveHealth>(this.haveHealths, this.compareHaveHealths);


            Debug.Log("haveHit = " + haveHits.Count + "   haveHealths = " + haveHealths.Count);
        }

        /// <summary>
        /// Base리스트에서 삭제할 리스트를 제거 해서 반환
        /// </summary>
        /// <typeparam name="T">any 타입</typeparam>
        /// <param name="baseList">베이스 리스트</param>
        /// <param name="removeList">삭제할 아이템 리스트</param>
        /// <returns></returns>
        public List<T> RemoveList<T>( List<T> baseList , List<T> removeList )
        {
            //Debug.Log("현재 base count = " + baseList.Count + "     remove Count = " + removeList.Count);

            for(int i = 0; i < removeList.Count; i++)
            {
                for (int j = 0; j < baseList.Count; j++)
                {
                    if(removeList[i].Equals(baseList[j]))
                    {
                        baseList.RemoveAt(j);
                    }
                }
            }

            //Debug.Log("남음 base count = " + baseList.Count);

            return baseList;
        }

        /// <summary>
        /// 실제 데미지를 주는 부분
        /// </summary>
        public void DoDamage(int damage)
        {
            Initialize();

            haveHits.ForEach(value => value.Hit());
            haveHealths.ForEach(value => value.DamagedEvent.Invoke(damage, Vector3.zero));

            
        }

        #region [-TestCode]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                DoDamage(1000);
            }
        }
        #endregion
    }
}
