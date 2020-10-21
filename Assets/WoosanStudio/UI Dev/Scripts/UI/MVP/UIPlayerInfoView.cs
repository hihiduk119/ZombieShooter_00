using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// *MPV 모델
    /// </summary>
    public class UIPlayerInfoView : MonoBehaviour
    {
        public UnityEngine.UI.Image Image;

        /// <summary>
        /// 캐릭터 이미지 업데이트
        /// </summary>
        /// <param name="sprite"></param>
        public void UpdateImage(Sprite sprite)
        {
            Image.sprite = sprite;
        }
    }
}
