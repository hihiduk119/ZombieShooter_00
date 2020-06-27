using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.Common
{
    /// <summary>
    /// 최대 FPS를 설정.
    /// </summary>
    public class MaxFPS : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
