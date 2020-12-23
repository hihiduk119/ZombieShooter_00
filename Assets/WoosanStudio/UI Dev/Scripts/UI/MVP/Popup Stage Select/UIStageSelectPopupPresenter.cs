using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 스테이지 선택 팝업
    ///  *MVP패턴
    /// </summary>
    public class UIStageSelectPopupPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIStageSelectPopupView View;

        [Header("[MVP Model]")]
        public UIStageModel Model;


    }
}
