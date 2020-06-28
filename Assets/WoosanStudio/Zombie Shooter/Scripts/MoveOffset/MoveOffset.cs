using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 텍스쳐의 옵셋을 강제로 밈
    /// </summary>
    public class MoveOffset : MonoBehaviour
    {
        private Material mMaterial;
        //0.05초 간격
        private WaitForSeconds mWFS = new WaitForSeconds(0.05f);

        IEnumerator Start()
        {
            //공유 메터리얼을 가져와 사용하기 때문에 대표 한명만 변경 하면 됨.
            mMaterial = GetComponent<MeshRenderer>().sharedMaterial;

            Vector2 offset;

            while (true)
            {
                offset = mMaterial.mainTextureOffset;
                offset.x += 0.002f;
                if (offset.x >= 1) offset.x -= 1;

                mMaterial.SetTextureOffset("_MainTex", offset);

                

                yield return mWFS;
            }
        }
    }
}
