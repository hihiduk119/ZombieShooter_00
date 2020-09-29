using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

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

        //private Dictionary<int, string> keyValues = new Dictionary<int, string>()
        //{{0,"default"},{1,"critical"},{2,"status"} };

        private StringBuilder stringBuilder = new StringBuilder();

        //데미지 이벤트 연결용
        //*데미지 이벤트가 해당 인터페이스에서 전달됨
        private IHaveHealth haveHealth;

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
            //*ExplodingProgetile가 충돌한 객체에서 IHaveHealth를 찾아서
            //*이벤트를 전달하기 때문에 IHaveHealth를 찾아서 가지고 있어야함
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
            //데미지 텍스트 표출
            //*크리티컬의 경우 keyValues를 다른걸로 변경 해야함
            //{{0,"default"},{1,"critical"},{2,"status"} };
            stringBuilder.Clear();
            //크리티컬이라면 "크리티컬" 단어 추가해서 넣기
            if (keyValue.Equals("critical"))
            {
                stringBuilder.Append("CRIT");
                stringBuilder.AppendLine();
            }
            stringBuilder.Append(damage.ToString());

            PopText(stringBuilder.ToString(), keyValue);

            stringBuilder.Clear();
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