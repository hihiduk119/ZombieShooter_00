using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 가이드 활성 또는 비활성
    /// </summary>
    public class UICanStartGuiderView : MonoBehaviour
    {
        [Header("[에너지 가이드]")]
        public GameObject Energy;

        [Header("[캐릭터 가이드]")]
        public GameObject Character;

        [Header("[건 가이드]")]
        public GameObject Gun;

        [Header("[탄약 가이드]")]
        public GameObject Ammo;

        [Header("[맵 가이드]")]
        public GameObject Map;

        [Header("[스타트 가이드]")]
        public GameObject Start;

        //비튼 연산 플래그
        [System.Flags]
        public enum Type
        {
            None        = 0,
            Energy      = 1 << 0,
            Character   = 1 << 1,
            Gun         = 1 << 2,
            Ammo        = 1 << 3,
            Map         = 1 << 4,
            Start       = 1 << 5,
            All         = int.MaxValue,
        }

        /// <summary>
        /// 에너지 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromEnergy(bool value)
        {
            SetActive(Type.Energy, !value);
        }

        /// <summary>
        /// 캐릭터 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromCharacter(bool value)
        {
            SetActive(Type.Character, !value);
        }

        /// <summary>
        /// 탄약 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromAmmo(bool value)
        {
            SetActive(Type.Ammo, !value);
        }

        /// <summary>
        /// 총 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromGun(bool value)
        {
            SetActive(Type.Gun, !value);
        }

        /// <summary>
        /// 맵 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromMap(bool value)
        {
            SetActive(Type.Map, !value);
        }

        /// <summary>
        /// 스타트버튼 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromStart(bool value)
        {
            SetActive(Type.Start, !value);
        }

        /// <summary>
        /// 해당 가이드 활성 or 비활성
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void SetActive(Type type,bool value)
        {
            //해당 플래그에 해당되는 가이드 활성 or 비활성
            if ((type & Type.Energy) != 0) { Energy.SetActive(value); }

            if ((type & Type.Character) != 0) { Character.SetActive(value); }

            if ((type & Type.Gun) != 0) { Gun.SetActive(value); }

            if ((type & Type.Ammo) != 0) { Ammo.SetActive(value); }

            if ((type & Type.Map) != 0) { Map.SetActive(value); }

            if ((type & Type.Start) != 0) { Start.SetActive(value); }
        }
    }
}
