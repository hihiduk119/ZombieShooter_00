using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 선택 및 구매 컨트롤
    /// 
    /// *MVC패턴
    /// </summary>
    public class UIPlayerPresenter : MonoBehaviour
    {
        [Header("[MVC 모델]")]
        public UIPlayerModel Model;

        [System.Serializable]
        public class UpdateCharacter : UnityEvent<int> { }

        [System.Serializable]
        public class UpdatePurchaseView : UnityEvent<PurchaseViewData> { }

        [System.Serializable]
        public class PurchaseViewData
        {
            public bool UseAble = false;
            public int RequireGem = 100;
            public int RequireLevel = 10;

            public PurchaseViewData(bool value = true) { UseAble = value; }
            public PurchaseViewData(int gem,int level,bool value = false) { RequireGem = gem; RequireLevel = level; UseAble = value; }

            public void Print()
            {
                Debug.Log("사용 여부 = " + UseAble + " 젬 요구 = " + RequireGem + "  요구레벨 = " + RequireLevel);
            }
        }


        [Header("[캐릭터 업데이트 이벤트]")]
        public UpdateCharacter ChangeCharacterEvent = new UpdateCharacter();

        [Header("[캐릭터 업데이트 이벤트]")]
        public UpdatePurchaseView PurchaseActivationEvent = new UpdatePurchaseView();

        private void Start()
        {
            //저장된 모델 불러오기
            Model.Load();
            //실제 모델 변경
            CharacterChange(0);
        }

        /// <summary>
        /// 모델을 변경함
        /// </summary>
        /// <param name="type"></param>
        public void CharacterChange(int value)
        {
            int currentIndex = 0;

            //현재 모델의 인덱스 가져오기
            currentIndex = Model.data.SelectedCharacter;

            currentIndex += value;

            //Debug.Log("currentIndex = " + currentIndex);

            int maxIndex = System.Enum.GetValues(typeof(UIPlayerSelectModel.ModelType)).Length;

            if (currentIndex < 0) { currentIndex = 0; }
            if (maxIndex <= currentIndex) { currentIndex = maxIndex - 1; }

            //캐릭터 변경 통지
            ChangeCharacterEvent.Invoke(currentIndex);

            //구매 뷰 사용 여부 통지
            if(Model.data.CardDatas[currentIndex].UseAble)
            {
                PurchaseActivationEvent.Invoke(new PurchaseViewData());
            }
            else
            {
                PurchaseActivationEvent.Invoke(new PurchaseViewData(Model.cardSettings[currentIndex].GemPrice, Model.cardSettings[currentIndex].UnlockLevel));
            }


            //변경 된 인덱스 모델 데이터에 넣음
            Model.data.SelectedCharacter = currentIndex;

            //저장
            Model.Save();
        }
    }
}
