using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 가이드 용 아이템
    /// </summary>
    public class GuideItem : MonoBehaviour
    {
        [Header("[현재 위치한 피벗 위치]")]
        public int PivotNumber = 0;

        [Header("[아이템 이름]")]
        public string Name;

        //아이템의 중복을 막기위한 자동 넘버링
        static public int Index = 0;

        public GuideItem() { Index++; }

        [Header("[타입 구분용]")]
        public Type ItemType = Type.None;

        public enum Type
        {
            Demon = 0,
            None,
        }
    }
}
