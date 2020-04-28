using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 총구 화염 프로젝터 컨트롤러
    /// </summary>
    public class MuzzleFlareProjector : MonoBehaviour , IMuzzleFlare
    {
        private Projector projector;
        Coroutine coroutineBlink;
        WaitForEndOfFrame WFEF = new WaitForEndOfFrame();

        private void Awake()
        {
            projector = GetComponent<Projector>();
        }

        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        public void SetLocalRotation(Vector3 rotation)
        {
            transform.localRotation = Quaternion.Euler(rotation);
        }

        public void SetParent(Transform parent)
        {
            transform.parent = parent;
        }

        /// <summary>
        /// 머즐을 1프레임 잠깐 켰다가 끔.
        /// </summary>
        public void Blink()
        {
            if (coroutineBlink != null) { StopCoroutine(coroutineBlink); }
            coroutineBlink = StartCoroutine(CoroutineBlink());
        }

        IEnumerator CoroutineBlink()
        {
            projector.enabled = true;
            yield return WFEF;
            projector.enabled = false;
        }
    }
}
