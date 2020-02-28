using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Guirao.UltimateTextDamage;
using WoosanStudio.Extension;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터의 텍스트 데미지UI를 생성 또는 할당함.
    /// 최초 생성보다 UI 가 부족하면 추가로 동적 생성.
    /// 
    /// **만약 몬스터가 죽었을때는 반드시 텍스트 데미지 UI 를 반환하는 작업이 필요.
    /// 현재 이 부분은 구현이 안되어 있기에 작업 및 테스트 필요.
    /// 
    /// </summary>
    public class AllTextDamageManager : MonoBehaviour
    {
        public static AllTextDamageManager Instance;

        [Header("[갯수가 부족할때 동적으로 만들 프리팹]")]
        public GameObject prefab;
        public List<UltimateTextDamageManager> managerList  =new List<UltimateTextDamageManager>();

        private Queue<UltimateTextDamageManager> managerQueue = new Queue<UltimateTextDamageManager>();

        private int namingCount = 0;

        private void Awake()
        {
            Instance = this;
            
            managerList = new List<UltimateTextDamageManager>(FindObjectsOfType<UltimateTextDamageManager>());
            //최초 몇개의 텍스트 데미지 UI가 있는지 확인
            namingCount = transform.childCount;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            //모두 비활성화 시키기
            managerList.ForEach(value => value.gameObject.SetActive(false));
        }

        //프리팹 사용해서 테스트 데미지 UI 새로 만들기
        GameObject Make(Transform parent,GameObject prefab,string name)
        {
            GameObject clone = Instantiate(prefab) as GameObject;
            namingCount++;
            clone.transform.parent = parent;
            clone.name = name + " " + namingCount.ToString();
            clone.transform.Reset();

            return clone;
        }


        /// <summary>
        /// 텍스트 데미지 메니저 가져오기
        /// 큐를 하나 만들어서 활성화 되어 있으면 사용중이 아님으로 사용 안하는것을 가져옴.
        /// </summary>
        /// <returns></returns>
        public UltimateTextDamageManager GetTextDamageManager()
        {
            managerQueue.Clear();
            managerList.ForEach(value =>
            {
                //Debug.Log(value.gameObject.ToString());
                if (!value.gameObject.activeSelf)
                {
                    managerQueue.Enqueue(value);
                }
            });

            UltimateTextDamageManager manager = null;

            try {
                manager = managerQueue.Dequeue();
            }
            catch
            {
                Debug.Log("UltimateTextDamageManager  가 NULL 이다. 몬스터 데이미 UI 확인해라!! ");
                //manager = null;
                manager = Make(transform, prefab, "TextDamageUI").GetComponent<UltimateTextDamageManager>();
                managerList.Add(manager);
                //큐에는 아직 않넣었는데 코드 전체 확인하고 어떻게 동작하는지 이해 후 결정.
            }

            if (manager != null) { manager.gameObject.SetActive(true); }            

            return manager;
        }
    }
}
