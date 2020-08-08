﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter {

    public class MainManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
        
        #region [TestCode] -테스트 폰에 올리기 위한 강제 실행 코드
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);
            //스테이지 최초 시작 실행
            //StageManager.Instance.FirstStage();
        }
        #endregion
    }
}
