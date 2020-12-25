using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 스테이지 선택 팝업
    ///  *MVP패턴
    /// </summary>
    public class UIStageSelectPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIStageSelectPopupView View;

        [Header("[MVP Model]")]
        public UIStageModel Model;

        [Header("[Map Content]")]
        public UI.MapContent MapContent;
        //[Header("[Map Item Root]")]
        //public Transform Content;

        private void Start()
        {
            //MapContent에서 아이템 활성,비활성 끝날때 까지 기다림
            //Start()에서 실행되는 이유

            //활성화된 맵
            //int activeMapCount = 0;

            //for (int i = 0; i < Content.childCount; i++)
            //{
            //    if(Content.GetChild(i).gameObject.activeSelf)
            //    {
            //        activeMapCount++;
            //    }
            //}

            //Debug.Log("activeMapCount = " + activeMapCount);
        }

        private void Awake()
        {
            //맵 갯수 만큼 아이콘 세팅
            //MapContent.SetMap(Model.mapSettings.Count);
        }
    }
}
