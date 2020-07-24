using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 머즐 플래쉬를 만들어줌
    /// </summary>
    public class MuzzleFlashFactory : MonoBehaviour
    {
        [Header("[머즐 플래쉬 프리팹]")]
        public GameObject Prefab;

        /// <summary>
        /// 해당 타겟에 머즐 플래쉬 생성
        /// </summary>
        /// <param name="target">머즐 만들기</param>
        /// <returns>생성된 머즐 플래쉬</returns>
        public GameObject Make(Transform target)
        {
            //머즐플래쉬 생성
            GameObject clone = Instantiate(Prefab) as GameObject;
            clone.transform.parent = target;
            MuzzleFlash muzzleFlash = clone.GetComponent<MuzzleFlash>();
            clone.name = "MuzzleFlash";

            //머즐플래쉬 로컬 값 세팅
            clone.transform.localPosition = muzzleFlash.InitLocalPosition;
            clone.transform.localRotation = Quaternion.Euler(muzzleFlash.InitLocalRotation);

            return clone;
        }
    }
}
