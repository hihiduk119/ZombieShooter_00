using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 로비에서 Gun모델 변경을 컨트롤
    /// </summary>
    public class GunModelController : MonoBehaviour
    {
        [Header("[모델 리스트]")]
        public List<GameObject> Models = new List<GameObject>();

        /// 캐릭터 변경
        /// UIPlayerPresenter에서 호출
        /// </summary>
        /// <param name="index"></param>
        public void Change(int index)
        {
            //Debug.Log("idx = " + index);
            //모두 비활성화
            Models.ForEach(modelObject => modelObject.SetActive(false));

            //해당 모델을 활성화.
            //*모델 타입 이넘과 모델 리스트의 순서는 동일해야 한다.
            Models[index].SetActive(true);
        }
    }
}
