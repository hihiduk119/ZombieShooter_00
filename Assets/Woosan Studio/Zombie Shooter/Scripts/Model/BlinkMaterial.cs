using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 공용 메터리얼을 사용하여 두녀석을 스왑 시켜서 깜빡임 연출
    /// </summary>
    public class BlinkMaterial : MonoBehaviour
    {
        Renderer _renderer;

        public Material defaultMaterial;
        public Material damagedMaterial;

        Coroutine swapMaterial;

        private void Start()
        {
            _renderer = transform.GetComponentInChildren<Renderer>();

            //defaultMaterial이 비어 있을때만 세팅
            if (defaultMaterial == null) { defaultMaterial = _renderer.sharedMaterials[0]; }
            //damagedMaterial이 비어 있을때만 세팅
            if (damagedMaterial == null) { damagedMaterial = _renderer.sharedMaterials[1]; }

            Debug.Log("render = " + _renderer.name);
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
            _renderer.sharedMaterials = new Material[] { defaultMaterial, damagedMaterial };
        }
    }
}
