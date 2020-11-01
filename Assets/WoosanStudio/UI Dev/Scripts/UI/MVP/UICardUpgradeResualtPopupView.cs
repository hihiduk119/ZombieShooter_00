using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 연구 완료 팝업
    /// </summary>
    public class UICardUpgradeResualtPopupView : MonoBehaviour
    {
        [Header("[업그레이드 결과 내용]")]
        public Text Result;

        //================> 성공 이펙트  <================

        [Header("[꽂가루 샤워 이펙트]")]
        public GameObject ConfettiShowerEffect;
        [Header("[아이템 후광 이펙트]")]
        public GameObject ItemSparkleEffect;
        [Header("[꽂가루 발사 이펙트]")]
        public GameObject ConfettiBlastEffect;

        [Header("[꽂가루 샤워 이펙트 포지션]")]
        public Transform ConfettiShowerPosition;
        [Header("[아이템 후광 이펙트 포지션]")]
        public Transform ItemSparklePosition;
        [Header("[꽂가루 발사 이펙트 포지션]")]
        public Transform[] ConfettiBlastPosition;

        //================> 실패   이펙트  <================

        [Header("[영혼 이펙트]")]
        public GameObject SoulEffect;

        [Header("[영혼 이펙트 포지션]")]
        public Transform[] SoulEPosition;

        public string strResult;
        public bool bResult;

        /// <summary>
        /// 활성화시 바로 내부 저정된 데이터 꺼내서 보여줌
        /// </summary>
        private void OnEnable()
        {
            UpdateResult(strResult, bResult);
        }

        /// <summary>
        /// 결과 정보 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateResult(string value,bool resultValue)
        {
            Result.text = value;
            //이펙트 연출
            ShowEffect(resultValue);
        }

        /// <summary>
        /// 이펙트를 쇼함
        /// </summary>
        public void ShowEffect(bool success)
        {
            if(success)//성공 이펙트
            {
                //샤워 이펙트
                Make(ConfettiShowerEffect, ConfettiShowerPosition);

                //후광 이펙트
                //Make(ItemSparkleEffect, ItemSparklePosition);

                //꽃 터짐 이펙트 갯수만큼
                StartCoroutine(MakeCoroutine(ConfettiBlastEffect, ConfettiBlastPosition));
                //for (int i = 0; i < ConfettiBlastPosition.Length; i++) {
                //    Make(ConfettiBlastEffect, ConfettiBlastPosition[i]);
                //}
            } else//실패 이펙트
            {
                //영혼 이펙트 갯수만큼
                StartCoroutine(MakeCoroutine(SoulEffect, SoulEPosition));
                //for (int i = 0; i < SoulEPosition.Length; i++)
                //{
                //    Make(SoulEffect, SoulEPosition[i]);
                //}
            }
        }

        /// <summary>
        /// 이펙트 생성
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        void Make(GameObject prefab, Transform parent)
        {
            GameObject clone = null;
            //샤워 이펙트
            clone = Instantiate(prefab) as GameObject;
            clone.transform.parent = parent;
            clone.transform.localScale = new Vector3(480, 480, 480);
            clone.transform.localPosition = Vector3.zero;
        }

        /// <summary>
        /// 이펙트 생성
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        IEnumerator MakeCoroutine(GameObject prefab, Transform[] parents)
        {
            int i = 0;

            while (i < parents.Length)
            {
                Make(prefab, parents[i]);
                i++;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
