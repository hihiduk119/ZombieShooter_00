using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 데이터
    /// *MVC패턴
    /// </summary>
    public class UIPlayerSelectView : MonoBehaviour
    {
        [Header("[MVP Presenter]")]
        public UIPlayerPresenter Presenter;

        [Header("[모델 리스트]")]
        public List<GameObject> ModelObjects = new List<GameObject>();

        /// <summary>
        /// 왼쪽 이동 클릭
        /// </summary>
        public void LeftClick()
        {
            Presenter.CharacterChange(-1);
        }

        /// <summary>
        /// 오른쪽 이동 클릭
        /// </summary>
        public void RightClick()
        {
            Presenter.CharacterChange(1);
        }

        /// <summary>
        /// 캐릭터 변경
        /// </summary>
        /// <param name="index"></param>
        public void ChangedCharacter(int index)
        {
            //Debug.Log("idx = " + index);
            //모두 비활성화
            ModelObjects.ForEach(modelObject => modelObject.SetActive(false));

            //해당 모델을 활성화.
            //*모델 타입 이넘과 모델 리스트의 순서는 동일해야 한다.
            ModelObjects[index].SetActive(true);
        }
    }
}
