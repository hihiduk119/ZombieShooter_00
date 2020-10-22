using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 선택
    /// *MVC패턴
    /// </summary>
    public class UIPlayerSelectPresenter : MonoBehaviour
    {
        [Header("[모델 리스트]")]
        public List<GameObject> ModelObjects = new List<GameObject>();

        [Header("[MVC 모델]")]
        public UIPlayerSelectModel Model;

        int currentIndex = 0;

        private void Start()
        {
            //저장된 모델 불러오기
            Model.Load();
            //실제 모델 변경
            Change(0);
        }

        /// <summary>
        /// 모델을 변경함
        /// </summary>
        /// <param name="type"></param>
        public void Change(int value)
        {
            //현재 모델의 인덱스 가져오기
            currentIndex = (int)Model.data.Type;

            currentIndex += value;

            //Debug.Log("currentIndex = " + currentIndex);

            int maxIndex = System.Enum.GetValues(typeof(UIPlayerSelectModel.ModelType)).Length;

            if (currentIndex < 0) { currentIndex = 0; }
            if(maxIndex <= currentIndex) { currentIndex = maxIndex-1; }

            ModelObjects.ForEach(modelObject => modelObject.SetActive(false));

            //해당 모델을 활성화.
            //*모델 타입 이넘과 모델 리스트의 순서는 동일해야 한다.
            ModelObjects[currentIndex].SetActive(true);

            //변경 된 인덱스 모델 데이터에 넣음
            Model.data.Type = (UIPlayerSelectModel.ModelType)currentIndex;

            //[Test]
            Select();
        }

        public void Select()
        {
            //저장 호출
            Model.Save();
        }
    }
}
