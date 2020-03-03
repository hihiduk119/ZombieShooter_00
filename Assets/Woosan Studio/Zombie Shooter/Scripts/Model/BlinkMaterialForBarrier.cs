using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 공용 메터리얼을 사용하여 두녀석을 스왑 시켜서 깜빡임 연출
    /// </summary>
    public class BlinkMaterialForBarrier : MonoBehaviour
    {
        Renderer _renderer;

        public Material defaultMaterial;
        public Material damagedMaterial;

        Coroutine swapMaterial;

        private void Start()
        {
            _renderer = transform.GetComponentInChildren<Renderer>();

        }

        public void Blink()
        {
            Debug.Log("Blink");
            Initialize();

            swapMaterial = StartCoroutine(SwapMaterial());
        }

        IEnumerator SwapMaterial()
        {
            _renderer.sharedMaterial = damagedMaterial;
            yield return new WaitForSeconds(0.15f);
            _renderer.sharedMaterial = defaultMaterial;
        }

        public void Initialize()
        {
            if (swapMaterial != null) { StopCoroutine(swapMaterial); }

            _renderer.sharedMaterial = defaultMaterial;
        }
    }
}
