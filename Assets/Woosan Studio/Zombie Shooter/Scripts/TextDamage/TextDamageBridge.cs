using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Guirao.UltimateTextDamage;

namespace WoosanStudio.ZombieShooter
{
    public class TextDamageBridge : MonoBehaviour
    {
        [HideInInspector] public UltimateTextDamageManager textManager;
        public Transform overrideTransform;

        private Dictionary<int, string> keyValues = new Dictionary<int, string>()
        {{0,"default"},{1,"critical"},{2,"status"} };

        private IEnumerator Start()
        {
            //0.1f초 기다리는 이유는 UltimateTextDamageManager에서 각각의 UI 생성시
            //하위 Text 엘리먼트를 만드는데 한프레임이 소요되기 때문에 이것보다 길게 대기함.
            yield return new WaitForSeconds(0.1f);

            UltimateTextDamageManager manager = AllTextDamageManager.Instance.GetTextDamageManager();
            if (manager != null) { textManager = manager; }

            //test code
            //while(true)
            //{
            //    yield return new WaitForSeconds(2f);
            //    PopText("100", "default");
            //}

            //IHaveHealth를 찾아서 이벤트 등록
            IHaveHealth haveHealth = transform.GetComponent<IHaveHealth>();
            haveHealth.DamagedEvent.AddListener(DamagedEventHandler);
        }

        //이벤트 등록 부분
        public void DamagedEventHandler(int damage)
        {
            PopText(damage.ToString(), keyValues[0]);
        }

        /// <summary>
        /// 
        /// 텍스트 데미지 표출
        /// 
        /// </summary>
        /// <param name="value">표출하려는 값</param>
        /// <param name="key">셋업된 key = [default,critical,status]</param>
        public void PopText(string value,string key)
        {
            textManager.Add(value, overrideTransform != null ? overrideTransform : transform, key);
        }
    }
}