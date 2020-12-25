using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 맵 아이템 프레즌트
    /// *MVP 모델
    /// </summary>
    public class UIMapItemPresenter : MonoBehaviour
    {
        [Header("[[Auto->Awake()] MVP View]")]
        public UIMapItemView View;

        private void Awake()
        {
            //나의 트랜스폼에서 찾는다
            View = this.GetComponent<UIMapItemView>();
        }

        /// <summary>
        /// 맵 뷰 업데이트
        /// </summary>
        public void UpdateInfo(Sprite sprite, string name, UIMapItemView.State state)
        {
            View.UpdateInfo(sprite, name, state);
        }
    }
}
