using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 
    /// *MPV 모델
    /// </summary>
    public class ExpPresenter : MonoBehaviour
    {
        public ExpModel Model;

        public float expSliderValue;

        private void Start()
        {
            //모델 초기화
            Model.Initialize();
        }

        /// <summary>
        /// 경험치 누적
        /// </summary>
        /// <param name="value"></param>
        public void AddExp(int value)
        {
            //현재 Exp에 추가
            Model.data.CurrentExp += value;

            //레벨이 업됬는지 확인
            if (Model.MaxExpList[Model.data.Level-1] <= Model.data.CurrentExp)
            {
                int extraExp = Model.data.CurrentExp - Model.MaxExpList[Model.data.Level - 1];
                //레벨없 시킴
                Model.data.Level++;
                //남은 경험치 현재 경험치에 넣음
                Model.data.CurrentExp = extraExp;
            }

            //데이터 저장
            Model.Save();
        }
    }
}
