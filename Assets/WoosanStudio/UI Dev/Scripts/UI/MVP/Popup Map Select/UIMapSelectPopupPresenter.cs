using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 스테이지 선택 팝업
    ///  *MVP패턴
    /// </summary>
    public class UIMapSelectPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIMapSelectPopupView View;

        [Header("[MVP Model]")]
        public UIMapModel Model;

        [Header("[Map Content]")]
        public UI.MapContent MapContent;

        [System.Serializable]
        public class UpdateUseAble : UnityEvent<bool> { }
        [Header("[선택 아이템 사용 불가 이벤트]")]
        public UpdateUseAble UpdateUseAbleEvent = new UpdateUseAble();

        //MapContent애서 가져옴
        private List<UIMapItemPresenter> mapItems = new List<UIMapItemPresenter>();

        //경험치 컨트롤
        private ExpPresenter expPresenter;

        //라운드 셀렉터 컨트롤
        private UIRoundSelectPresenter roundSelectPresenter;

        private void Awake()
        {
            //MapContent애서 mapItems 세팅
            for (int i = 0; i < MapContent.transform.childCount; i++)
            {
                mapItems.Add(MapContent.transform.GetChild(i).GetComponent<UIMapItemPresenter>());
            }

            //경험치 컨트롤러 가져오기
            expPresenter = GameObject.FindObjectOfType<ExpPresenter>();

            //맵 변경시 화면 업데이트하게 등록
            View.MapChangeEvent.AddListener(UpdateInfo);

            //스타트버튼 사용 여부에따라 좌우 버튼 트윈
            UpdateUseAbleEvent.AddListener(View.UpdateButton);

            //라운드 셀렉터 컨트롤 가져오기
            roundSelectPresenter = GameObject.FindObjectOfType<UIRoundSelectPresenter>();
        }

        private void Start()
        {
            for (int i = 0; i < Model.mapSettings.Count; i++)
            {
                //Debug.Log(expPresenter.Level() + "   --  " + Model.mapSettings[i].UnlockLevel);
                //현재 레벨 과 맵 언락레벨 비교
                if(expPresenter.Level() >= Model.mapSettings[i].UnlockLevel)
                {
                    //맵 사용 활성화
                    Model.mapSettings[i].CanUse = true;

                    //맵 락 해제 및 화면 업데이트
                    mapItems[i].UpdateInfo(false, Model.mapSettings[i].Icon,
                        Model.mapSettings[i].Name,
                        UIMapItemView.State.Able, Model.mapSettings[i].UnlockLevel.ToString());

                } else
                {
                    //맵 락 해제 및 화면 업데이트
                    mapItems[i].UpdateInfo(true, Model.mapSettings[i].Icon,
                        Model.mapSettings[i].Name,
                        UIMapItemView.State.Lock, Model.mapSettings[i].UnlockLevel.ToString());
                }
            }


            //맵 0으로 초기화
            //*나중에 현재 선택 맵 로드시 변경 되어야 함
            View.MoveScroll(GlobalDataController.Instance.SelectedMap);
            //맵 0으로 화면 업데이트
            UpdateInfo(GlobalDataController.Instance.SelectedMap);
        }

        /// <summary>
        /// 맵 이름 변경
        /// </summary>
        /// <param name="mapIndex"></param>
        private void UpdateInfo(int mapIndex)
        {
            //Debug.Log("index = " + mapIndex);
            View.UpdateInfo(Model.mapSettings[mapIndex].Name);

            //글로벌 데이터에 현재 맵 저장
            GlobalDataController.Instance.SelectedMap = mapIndex;

            //맵 세팅 캐쉬에 담기
            GlobalDataController.MapSetting = Model.mapSettings[mapIndex];

            //스타트 버튼 사용 가능 여부 통지
            UpdateUseAbleEvent.Invoke(Model.mapSettings[mapIndex].CanUse);

            //라운드 셀렉터에 현재 맵 넣기
            roundSelectPresenter.Setting = GlobalDataController.MapSetting;
        }
    }
}
