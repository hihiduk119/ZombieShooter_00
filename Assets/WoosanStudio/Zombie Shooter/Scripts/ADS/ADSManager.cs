using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
namespace WoosanStudio.ZombieShooter.ADS
{
    /// <summary>
    /// 모든 광고 메니져
    /// *단일 씬에 존재 해야함
    /// *Title씬에 존제 필요
    /// </summary>
    public class ADSManager : MonoBehaviour
    {
        //싱글톤 패턴
        static public ADSManager Instance;

        private void Awake()
        {
            if (null == Instance)
            {
                //싱글톤 패턴
                Instance = this;

                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// 광고 제거 구매 요청
        /// </summary>
        public void RemoveADS ()
        {

        }

        /// <summary>
        /// 광고 요청
        /// </summary>
        public void RequestADS()
        {

        }

        /// <summary>
        /// 광고 요청 콜백
        /// </summary>
        public bool CallbackADS()
        {
            return true;
        }
    }
}
