using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// UI 플레이어 인포 팝업
    /// 
    /// *MVC패턴
    /// </summary>
    public class UIPlayerInfoPopupView : MonoBehaviour
    {
        [Header("[캐릭터 이미지]")]
        public Image Image;
        [Header("[캐릭터 이름]")]
        public Text Name;
        [Header("[캐릭터 설멍]")]
        public Text Description;
        [Header("[캐릭터 플레이타임]")]
        public Text PlayTime;
        [Header("[캐릭터가 사냥한 몬스터 수]")]
        public Text HuntedMonster;

        private StringBuilder stringBuilder = new StringBuilder();

        private void Start()
        {
            //시작시 씬에서 UIPlayerInfoView 찾아서 데이터 가져옴
            UIPlayerInfoView infoView = GameObject.FindObjectOfType<UIPlayerInfoView>();
            //가져온 데이터로 정보 업데이트
            UpdateInfo(infoView.infoViewData);
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(UIPlayerPresenter.InfoViewData data)
        {
            //일단 초기화
            stringBuilder.Clear();
            //캐릭터 이미지 넣기
            Image.sprite = data.Image;
            //캐릭터 이름 넣기
            Name.text = data.Name;
            //캐릭터 정보 스트링 하나로 합치기
            for (int i = 0; i < data.Descripsions.Count; i++)
            {
                //정보 넣기
                stringBuilder.Append(data.Descripsions[i]);
                //개행
                stringBuilder.AppendLine();
            }
            //합친 캐릭터 정보 넣기
            Description.text = stringBuilder.ToString();
            //해당 캐릭터 플레이 타임 넣기
            PlayTime.text = data.PlayTime.ToString();
            //해당 캐릭터 사냥한 몬스터 숫자 넣기
            HuntedMonster.text = data.HuntedMonster.ToString();
        }
    }
}
