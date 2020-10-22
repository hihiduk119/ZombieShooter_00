using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어 구매화면 컨트
    /// *MPV 모델
    /// </summary>
    public class UIPlayerPurchasePresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIPlayerPurchaseView View;
        [Header("[MVP Model]")]
        public UIPlayerPurchaseModel Model;

        public string CharaceterName;

        public Sprite CharaceterImage;

        public List<CardProperty> CardProperties;

        public void UpdateCharacter(int index)
        {

        }
    }
}