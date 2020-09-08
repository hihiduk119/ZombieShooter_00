using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카드 셀렉션 아이템 컨트롤
    /// </summary>
    public class SelectItem : MonoBehaviour
    {
        [Header("[(Auto->Awake())]")]
        public SpriteChanger SpriteChanger;
        [Header("[Background-Fill 이미지 등록)]")]
        public Image Fill;
        [Header("[Background-Outline 이미지 등록)]")]
        public Image Outline;
        [Header("[Icon 이미지 등록)]")]
        public Image Icon;
        [Header("[Background-Fill 설정시 자동 (Auto->Awake())]")]
        public ColorSet FillColorSet;
        [Header("[Background-Outline 설정시 자동(Auto->Awake())]")]
        public ColorSet OutlineColorSet;

        //자동으로 할당
        private void Awake()
        {
            SpriteChanger = GetComponent<SpriteChanger>();
            FillColorSet = Fill.GetComponent<ColorSet>();
            OutlineColorSet = Outline.GetComponent<ColorSet>();
        }

        /// <summary>
        /// Icon이미지를 업데이트 함
        /// </summary>
        /// <param name="sprite">업데이트할 이미지</param>
        public void IconUpdate(Sprite sprite , Color color,float scale)
        {
            Icon.sprite = sprite;
            //색 적용
            Icon.color = color;
            //아이콘 크기 스프라이트 사이즈에 마춰 재적용.
            Icon.rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
            //스케일 적용
            Icon.rectTransform.localScale = new Vector3(scale, scale, scale);
        }

        /// <summary>
        /// 선택 했을때
        /// </summary>
        public void Select()
        {
            //컬러셋 0번으로 변//컬러셋 0번으로 변경
            Fill.color = FillColorSet.ColorList[1];
            Outline.color = OutlineColorSet.ColorList[1];
        }

        /// <summary>
        /// 선택 해제시
        /// </summary>
        public void Release()
        {
            //컬러셋 1번으로 변경
            Fill.color = FillColorSet.ColorList[0];
            Outline.color = OutlineColorSet.ColorList[0];
        }


        #region [-TestCode]
        //void Update()
        //{
        //    //백그라운드 컬러 변경
        //    if (Input.GetKeyDown(KeyCode.Y))
        //    {
        //        Select();
        //    }

        //    //백그라운드 컬러 원복
        //    if (Input.GetKeyDown(KeyCode.U))
        //    {
        //        Release();
        //    }
        //}
        #endregion
    }
}
