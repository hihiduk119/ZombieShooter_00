using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 레이저 포인터를 만듬
    /// </summary>
    public class LaserPointerFactory : MonoBehaviour
    {
        [Header ("[레이저 포인터 프리팹]")]
        public GameObject Prefab;

        [Header("[레이저 포인터 를 생성 시킬 부모 -> 생성 플레이어에 마춰 바꿔주야함")]
        public Transform Anchor;

        [Header("[초기화 로컬 position]")]
        public Vector3 InitPosition;

        [Header("[초기화 로컬 rotation]")]
        public Vector3 InitRotation;

        /// <summary>
        /// 해당 레이저 포인터 생성
        /// </summary>
        public GameObject Make()
        {
            //Debug.Log("Anchor name = " + Anchor.name);

            GameObject clone = Instantiate(Prefab) as GameObject;
            clone.transform.parent = Anchor;
            clone.transform.localPosition = InitPosition;
            clone.transform.localRotation = Quaternion.Euler(InitRotation);

            return clone;
        }

        #region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.M))
        //    {
        //        Make();
        //    }
        //}
        #endregion

    }
}
