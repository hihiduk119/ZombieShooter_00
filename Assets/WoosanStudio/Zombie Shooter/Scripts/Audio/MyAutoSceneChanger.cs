using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// MasterAudioTest 0 Scene로 자동 이동하기 위해 만듬
    /// </summary>
    public class MyAutoSceneChanger : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("MasterAudioTest 1", LoadSceneMode.Single);
        }
    }
}
