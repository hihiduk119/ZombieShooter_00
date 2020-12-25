using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.Text;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 맵 아이템 뷰
    /// *MVP 모델
    /// </summary>
    public class UIMapItemView : MonoBehaviour
    {
        public enum State
        {
            Able,           //사용 가능
            Lock,           //잠김
            ComingSoon,     //곧 출시
        }

        [Header("[맵 이미지]")]
        public Image Image;

        [Header("[맵 이름")]
        public Text Title;

        [Header("[락 아이콘]")]
        public GameObject Lock;


        /// <summary>
        /// 맵 뷰 업데이트
        /// </summary>
        /// <param name="cardSetting"></param>
        public void UpdateInfo(Sprite sprite,string name,State state)
        {
            Image.sprite = sprite;

            //락 비활성화
            Lock.SetActive(false);

            StringBuilder stringBuilder = new StringBuilder();

            switch (state)
            {
                case State.Able:
                    Title.text = name;
                    break;
                case State.ComingSoon:
                    //coming soon 넣기
                    stringBuilder.Append("Coming").AppendLine().Append("Soon");
                    Title.text = stringBuilder.ToString();
                    break;
                case State.Lock:
                    //락 활성화
                    Lock.SetActive(true);
                    break;
                default:
                    break;
            }

        }
    }
}
