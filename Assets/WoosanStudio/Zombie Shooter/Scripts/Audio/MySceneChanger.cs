using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// MasterAudioTest 1 Scene로 자동 이동하기 위해 만듬
    /// </summary>
    public class MySceneChanger : MonoBehaviour, IPointerClickHandler
    {
        public string SceneName = "";

        public void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        }
    }
}
