﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 탄약 정보 표시
    /// *탄약 정보 창을 여는 역활도 함.
    /// *MVP 모델
    /// </summary>
    public class UIAmmoInfoView : MonoBehaviour
    {
        public UnityEngine.UI.Image Image;

        public UnityEngine.UI.Text Name;

        //인포 뷰어 열때 사용하기 위해 저장
        //public UIPlayerPresenter.InfoViewData infoViewData;
        public CardSetting cardSetting;

        /// <summary>
        /// 캐릭터 이미지 업데이트
        /// </summary>
        /// <param name="sprite"></param>
        public void UpdatePlayerInfo(CardSetting cardSetting)
        {
            //받은 데이터 일단 저장
            this.cardSetting = cardSetting;

            Image.sprite = cardSetting.Sprite;

            Name.text = cardSetting.Name;
        }
    }
}