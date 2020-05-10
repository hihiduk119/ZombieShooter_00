using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모든 Health를 미리 생성 및 활성 비활성 연결 관리.
    /// </summary>
    public class HealthBarManager : MonoBehaviour
    {
        public static HealthBarManager Instance;

        public List<GameObject> HealthBarList = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 생성된 체력 바를 리스트에 등록
        /// </summary>
        /// <param name="obj"></param>
        public void Add(GameObject obj)
        {
            HealthBarList.Add(obj);                
        }

        /// <summary>
        /// 해당 체력바를 리스트에서 제거 후 삭제
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(GameObject obj)
        {
            HealthBarList.Remove(obj);
            Destroy(obj);
        }
    }
}
