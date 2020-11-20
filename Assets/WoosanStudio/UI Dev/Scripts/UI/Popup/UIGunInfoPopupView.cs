using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// UI Gun 인포 팝업
    /// 
    /// *MVC패턴
    /// </summary>
    public class UIGunInfoPopupView : MonoBehaviour
    {
        [Header("[이미지]")]
        public Image Image;
        [Header("[이름]")]
        public Text Name;
        [Header("[설멍]")]
        public Text Description;
        [Header("[공격력]")]
        public Text Attack;
        [Header("[치명타율]")]
        public Text Critical;
        [Header("[플레이타임]")]
        public Text PlayTime;
        [Header("[사냥한 몬스터 수]")]
        public Text HuntedMonster;

        private StringBuilder stringBuilder = new StringBuilder();

        private void Start()
        {
            //시작시 씬에서 UIPlayerInfoView 찾아서 데이터 가져옴
            UIGunInfoView infoView = GameObject.FindObjectOfType<UIGunInfoView>();
            //가져온 데이터로 정보 업데이트
            UpdateInfo(infoView.cardSetting);
        }

        /// <summary>
        /// 정보 업데이트
        /// </summary>
        public void UpdateInfo(CardSetting cardSetting)
        {
            //일단 초기화
            stringBuilder.Clear();
            //캐릭터 이미지 넣기
            Image.sprite = cardSetting.Sprite;
            //캐릭터 이름 넣기
            Name.text = cardSetting.Name;

            //설명
            List<string> desc = new List<string>();

            //프로퍼티에서 설명들 가져옴
            //1부터 시작
            //0은 캐릭터 변경한다는 내용이 있음
            for (int i = 1; i < cardSetting.Properties.Count; i++)
            {
                //0레벨 적용된 완성된 설명 가져오기
                //* [수정필요]카드 레벨을 적용 할지 말지 나중에 결정
                desc.Add(cardSetting.Properties[i].GetCompletedDescripsion(cardSetting.Level));
            }

            //캐릭터 정보 스트링 하나로 합치기
            for (int i = 0; i < desc.Count; i++)
            {
                //정보 넣기
                stringBuilder.Append(desc[i]);
                //개행
                stringBuilder.AppendLine();
            }
            //합친 캐릭터 정보 넣기
            Description.text = stringBuilder.ToString();
            
            //샷건은 데미지 추가 설명 필요
            if(cardSetting.Type == CardSetting.CardType.Shotgun)
            {
                //총의 value는 기본 데미지 이다.
                Attack.text = cardSetting.Value.ToString() + "x6";
            } else
            {
                //총의 value는 기본 데미지 이다.
                Attack.text = cardSetting.Value.ToString();
            }

            //모든 총의 기본 치명타율 2%
            Critical.text = GlobalDataController.DefaultCriticalChance.ToString();
            //해당 캐릭터 플레이 타임 넣기
            PlayTime.text = cardSetting.PlayTime.ToString();
            //해당 캐릭터 사냥한 몬스터 숫자 넣기
            HuntedMonster.text = cardSetting.HuntedMonster.ToString();
        }
    }
}
