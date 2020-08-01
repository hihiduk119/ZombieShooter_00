using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 카트를 선택했을때 토글컨트롤
    /// 해당 토글은 선택시 활성화 연출용 토글
    /// </summary>
    public class CardSelectionController : MonoBehaviour
    {
        [Header("[카드 활성 연춣용 토글]")]
        public List<Toggle> Toggles = new List<Toggle>();
        [Header("[카드 선택 연출]")]
        public List<SelectItem> SelectItems = new List<SelectItem>();

        private void Start()
        {
            //최초 시작시 가운데 선택하게 만듬
            Select(1);
        }

        /// <summary>
        /// 해당 카드 선택
        /// </summary>
        /// <param name="index">선택한 카드 번호</param>
        public void Select(int index)
        {
            //모든 아이템 선택 해제
            SelectItems.ForEach(value => value.Release());
            //토글 선택
            Toggles[index].isOn = true;
            //아Select템 선택
            SelectItems[index].Select();
        }

        #region [-TestCode]
        /// <summary>
        /// 토클을 강제로 활성화 시키기 위해 사용
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Select(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Select(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Select(2);
            }
        }
        #endregion

    }
}
