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

        [Header("[[Auto] 현재 맵 잠김 확인용 UIStageSelectPopupPresenter에서 세티됨]")]
        public bool Lock = true;

        private void Awake()
        {
            //나의 트랜스폼에서 찾는다
            View = this.GetComponent<UIMapItemView>();
        }

        /// <summary>
        /// 맵 뷰 업데이트
        /// </summary>
        public void UpdateInfo(bool isLock, Sprite sprite, string name, UIMapItemView.State state, string level)
        {
            Lock = isLock;

            
            View.UpdateInfo(sprite, name, state, level);
        }
    }
}
