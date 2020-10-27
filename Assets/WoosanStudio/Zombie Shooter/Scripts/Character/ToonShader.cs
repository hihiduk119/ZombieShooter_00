using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모델툰 쉐이더의 아웃라인 값을 해당 씬에서만 변경 하기위해 사용
    /// </summary>
    public class ToonShader : MonoBehaviour
    {
        [Header("[로비 씬]")]
        public string RobbyScene = "1.ZombieShooter-Robby";

        [Header("[아웃라인 value 리스트]")]
        public List<float> OutlineWidths = new List<float>();

        [Header("[공유하는 메터리얼]")]
        public List<Material> Materials = new List<Material>();

        /// <summary>
        /// 모든씬에서 살아있게 만들기
        /// </summary>
        private void Awake()
        {
            //실행즉시 바로 아웃라인 두껍게 만들기
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        /// <summary>
        /// 씬 로드 이벤트 실행
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            Scene myScene = SceneManager.GetSceneByName(RobbyScene);

            if (scene.buildIndex == myScene.buildIndex)
            {   //두꺼운 아웃라인으로 모델 변경
                SetOutlineWidth(1);
                //Debug.Log("===============> 두껍게 변경");
            }
            else
            {   //얕은 아웃라인으로 모델 변경
                SetOutlineWidth(0);
                //Debug.Log("===============> 얕게 변경");
            }
        }

        /// <summary>
        /// 아웃라인 설정 변경
        /// </summary>
        /// <param name="index"></param>
        public void SetOutlineWidth(int index)
        {
            Materials.ForEach(value => value.SetFloat("_Outline", OutlineWidths[index]));
        }
    }
}
