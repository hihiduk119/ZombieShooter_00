using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 스프라이트 변경
    /// </summary>
    public class SpriteChanger : MonoBehaviour
    {
        [Header("[교체용 스프라이트]")]
        public List<Sprite> SpriteList = new List<Sprite>();

        [Header("[스프라이트 변경할 이미지]")]
        public Image Image;
        //Image의 RectTransform
        private RectTransform rectTransform;
        private int testIndex = 0;

        private void Awake()
        {
            //시작하면 바로 Image의 RectTraansform을 가져옴
            rectTransform = Image.GetComponent<RectTransform>();
        }

        /// <summary>
        /// 스프라이트 해당 인덱스로 변경
        /// </summary>
        /// <param name="index">변경 하려는 인덱스</param>
        public void Change(int index)
        {
            if (Image == null) return;

            Image.sprite = SpriteList[index];

            rectTransform.sizeDelta = Image.sprite.rect.size;
        }

        #region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.T))
        //    {
        //        Change(testIndex);

        //        testIndex--;

        //        if (testIndex < 0)
        //            testIndex = SpriteList.Count - 1;
        //    }

        //    if (Input.GetKeyDown(KeyCode.Y))
        //    {
        //        Change(testIndex);
        //        testIndex++;

        //        if (testIndex >= SpriteList.Count)
        //            testIndex = 0;
        //    }
        //}
        #endregion
    }
}
