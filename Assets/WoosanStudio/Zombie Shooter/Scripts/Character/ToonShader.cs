﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모델툰 쉐이더의 아웃라인 값을 해당 씬에서만 변경 하기위해 사용
    /// </summary>
    public class ToonShader : MonoBehaviour
    {
        [Header("[아웃라인 value 리스트]")]
        public List<float> OutlineWidths = new List<float>();

        [Header("[공유하는 메터리얼]")]
        public Material material;

        /// <summary>
        /// 모든씬에서 살아있게 만들기
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(this);

            //실행즉시 바로 아웃라인 두껍게 만들기
            OnLevelWasLoaded(1);
        }

        /// <summary>
        /// 해당 씬 로드시 호출
        /// </summary>
        /// <param name="level"></param>
        private void OnLevelWasLoaded(int level)
        {
            if(level == 1)
            {   //두꺼운 아웃라인으로 모델 변경
                SetOutlineWidth(1);
            } else
            {   //얕은 아웃라인으로 모델 변경
                SetOutlineWidth(0);
            }
        }

        /// <summary>
        /// 아웃라인 설정 변경
        /// </summary>
        /// <param name="index"></param>
        public void SetOutlineWidth(int index)
        {
            material.SetFloat("_Outline", OutlineWidths[index]);
        }
    }
}