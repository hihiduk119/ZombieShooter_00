using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 몬스터 레벨에 의한 컬러 변경을 위해 사용
    /// </summary>
    public class OutlineColor : MonoBehaviour
    {
        [Header("[공유 메터리얼]")]
        public Material ShareMaterial;
        [Header("[기본이 메터리얼 컬러]")]
        public Color DefaultColor = new Color32(17,17,17,255);

        /// <summary>
        /// 기본 컬러로 초기화
        /// </summary>
        public void Initialize()
        {
            ShareMaterial.SetColor("_OutlineColor", DefaultColor);
        }

        /// <summary>
        /// 기본 컬러로 초기화
        /// </summary>
        public void SetColor(Color color)
        {
            ShareMaterial.SetColor("_OutlineColor", color);
        }
    }
}
