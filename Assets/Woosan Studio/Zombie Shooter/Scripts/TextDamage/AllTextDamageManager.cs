using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Guirao.UltimateTextDamage;

namespace WoosanStudio.ZombieShooter
{
    public class AllTextDamageManager : MonoBehaviour
    {
        public static AllTextDamageManager Instance;
        public List<UltimateTextDamageManager> managerList  =new List<UltimateTextDamageManager>();

        private Queue<UltimateTextDamageManager> managerQueue = new Queue<UltimateTextDamageManager>();

        private void Awake()
        {
            Instance = this;
            
            managerList = new List<UltimateTextDamageManager>(FindObjectsOfType<UltimateTextDamageManager>());
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            //모두 비활성화 시키기
            managerList.ForEach(value => value.gameObject.SetActive(false));
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
                Debug.Log(value.gameObject.ToString());

                if (!value.gameObject.activeSelf)
                {
                    managerQueue.Enqueue(value);
                }
            });

            UltimateTextDamageManager manager = managerQueue.Dequeue();

            manager.gameObject.SetActive(true);

            return manager;
        }
    }
}
