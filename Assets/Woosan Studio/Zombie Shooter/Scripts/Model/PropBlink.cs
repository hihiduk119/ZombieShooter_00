using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 공용 메터리얼을 사용하여 두녀석을 스왑 시켜서 깜빡임 연출
    ///
    /// **몬스터 추가시 메터리일 2개 사용하기 때문에 일반 메터리얼 및 데미지 입는 메터리얼 추가 필요.
    /// </summary>
    public class PropBlink: MonoBehaviour , IBlink
    {
        Renderer _renderer;

        public Material defaultMaterial;
        public Material damagedMaterial;

        Coroutine swapMaterial;

        public GameObject myGameObject { get => this.gameObject; set => throw new System.NotImplementedException(); }

        private void Start()
        {
            _renderer = transform.GetComponentInChildren<Renderer>();

            //defaultMaterial이 비어 있을때만 세팅
            //if (defaultMaterial == null) { defaultMaterial = _renderer.sharedMaterials[0]; }
            //damagedMaterial이 비어 있을때만 세팅
            //if (damagedMaterial == null) { damagedMaterial = _renderer.sharedMaterials[1]; }
        }

        public void Blink()
        {
            //Initialize();
            
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
            //_renderer.sharedMaterials = new Material[] { defaultMaterial, damagedMaterial };
        }
    }
}
