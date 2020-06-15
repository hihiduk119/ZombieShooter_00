using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter.Test
{
    /// <summary>
    /// 테스트용 키 인풋
    /// 1. 씬전환
    /// </summary>


    public class TestKeyInput : MonoBehaviour
    {
        //씬 전환용 클래스
        public SceneTransition SceneTransition;
             
        private void Update()
        {
            //씬 전환 호출
            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneTransition.PerformTransition();
            }
        }
    }
}
