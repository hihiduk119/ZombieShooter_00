using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Guirao.UltimateTextDamage;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 데미지 UI 와 연결해주는 블릿지 클래스
    /// </summary>
    public class TextDamageBridge : MonoBehaviour , IConnect
    {
        [HideInInspector] public UltimateTextDamageManager textManager;
        public Transform overrideTransform;

        private Dictionary<int, string> keyValues = new Dictionary<int, string>()
        {{0,"default"},{1,"critical"},{2,"status"} };

        //데미지 이벤트 연결용
        IHaveHealth haveHealth;

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
            haveHealth = transform.GetComponent<IHaveHealth>();
            //리스너 연결
            Connect();
        }

        /// <summary>
        /// 데미지 이벤트 발생 연결
        /// </summary>
        public void Connect()
        {
            haveHealth.DamagedEvent.AddListener(DamagedEventHandler);
        }

        /// <summary>
        /// 데이지 이벤트 연결 해제
        /// </summary>
        public void Disconnect()
        {
            //이벤트 연결 해제
            haveHealth.DamagedEvent.RemoveListener(DamagedEventHandler);
            //해당 UI 디스에이블
            textManager.gameObject.SetActive(false);
        }

        /// <summary>
        /// 이벤트 등록 부분
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="hit"></param>
        public void DamagedEventHandler(int damage,Vector3 hit,string keyValue)
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