using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    public class SceneAutoTransition : MonoBehaviour
    {
        [Header("[씬을 넘기기전 대기 시간")]
        public float DelayTime = 2f;

        private SceneTransition sceneTransition;
        private void Awake()
        {
            sceneTransition = GetComponent<SceneTransition>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(DelayTime);

            sceneTransition.PerformTransition();
        }
    }
}
