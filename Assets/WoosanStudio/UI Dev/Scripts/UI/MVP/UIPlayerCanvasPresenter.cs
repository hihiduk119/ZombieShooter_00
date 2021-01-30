using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 플레이어를 따라다는 UI 캔버스 프레젠터
    /// 1. 체력바
    /// 2. 탄약바
    /// *MVP 모델
    /// </summary>
    public class UIPlayerCanvasPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIPlayerCanvasView View;

        [Header("[체력바가 오브젝트 따라다니게 만들어줌]")]
        public EnergyBarToolkit.EnergyBarFollowObject HP_BarFollowObject;

        [Header("[탄약바가 오브젝트 따라다니게 만들어줌]")]
        public EnergyBarToolkit.EnergyBarFollowObject AmmoBarFollowObject;

        [Header("[체력바 컨트롤]")]
        public EnergyBar HP_Bar;

        [Header("[탄약바 컨트롤]")]
        public EnergyBar AmmoBar;

        /// <summary>
        /// 따라다닐 플레이어 세팅
        /// </summary>
        /// <param name="player"></param>
        public void FollowPlayer(GameObject player)
        {
            HP_BarFollowObject.followObject = player;
            AmmoBarFollowObject.followObject = player;
        }

        /// <summary>
        /// 활성 비활성
        /// </summary>
        public void SetActivate(bool value)
        {
            View.SetActivate(value);
        }

        /// <summary>
        /// 체력 업데이트
        /// *현재 체력에서 -+한다
        /// </summary>
        /// <param name="value"></param>
        public void UpdateHP(int value)
        {
            HP_Bar.valueCurrent += value;

            //0보다 작으면 0으로초기화
            if(HP_Bar.valueCurrent < 0 ) { HP_Bar.valueCurrent = 0; }
        }

        /// <summary>
        /// 최대 체력 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateMaxHP(int value)
        {
            HP_Bar.SetValueMax(value);
        }

        /// <summary>
        /// 체력 초기화
        /// </summary>
        public void ResetHP()
        {
            HP_Bar.valueCurrent = HP_Bar.valueMax;
        }

        /// <summary>
        /// 탄약 업데이트
        /// *현재 탄약에서 -+한다
        /// </summary>
        /// <param name="value"></param>
        public void UpdateAmmo(int value)
        {
            AmmoBar.valueCurrent += value;

            //0보다 작으면 0으로초기화
            if (AmmoBar.valueCurrent < 0) { AmmoBar.valueCurrent = 0; }
        }

        /// <summary>
        /// 최대 탄약 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateMaxAmmo(int value)
        {
            AmmoBar.SetValueMax(value);
        }

        /// <summary>
        /// 체력 초기화
        /// </summary>
        public void ResetAmmo()
        {
            AmmoBar.valueCurrent = AmmoBar.valueMax;
        }


        //#region [-TestCode]
        //void Update()
        //{
        //    //체력 -10
        //    if (Input.GetKeyDown(KeyCode.A))
        //    {
        //        UpdateHP(-10);
        //    }

        //    //탄약 -1
        //    if (Input.GetKeyDown(KeyCode.S))
        //    {
        //        UpdateAmmo(-1);
        //    }

        //    //체력 200변경
        //    if (Input.GetKeyDown(KeyCode.D))
        //    {
        //        UpdateMaxHP(200);
        //        ResetHP();
        //    }

        //    //탄약 60변경
        //    if (Input.GetKeyDown(KeyCode.F))
        //    {
        //        UpdateMaxAmmo(60);
        //        ResetAmmo();
        //    }
        //}
        //#endregion
    }
}
