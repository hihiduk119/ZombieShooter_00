﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI.MVP.InGameCardSelect
{
    /// <summary>
    /// 카드 선택 팝업
    /// *MVP 모델
    /// </summary>
    public class PopupView : MonoBehaviour
    {
        [Header("[카드 이름]")]
        public Text Name;

        [Header("[카드 스택 카운트")]
        public Text StackCount;

        [Header("[카드 설명]")]
        public Text Description;

        public void UpdateView(string name,string stackCount,string description)
        {
            Name.text = name;
            StackCount.text = stackCount;
            Description.text = description;
        }
    }
}
