using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using Ricimi;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 버튼 활성 비활성 컨트롤
    /// </summary>
    public class UIStartButtonPresenter : MonoBehaviour
    {
        [Header("[MVP 뷰]")]
        public UIStartButtonView View;

        [Header("[Energy 프레젠트]")]
        public EnergyPresenter energyPresenter;

        [Header("[씬 전환]")]
        public SceneTransition sceneTransition;

        [System.Serializable]
        public class UpdateUseAble : UnityEvent<bool> { }
        [Header("[사용 불가 이벤트]")]
        public UpdateUseAble UpdateUseAbleEvent = new UpdateUseAble();

        [Header("[스테이지 시작됨을 알림]")]
        public UnityEvent StageStartEvent = new UnityEvent();

        [Header("[No Energy 팝업 오프너]")]
        public Ricimi.PopupOpener PopupOpener;


        //캐릭터 사용 가능
        bool useAbleCharacter = false;
        //탄야 사용 가능
        bool useAbleAmmo = false;
        //총 사용 가능
        bool useAbleGun = false;
        //맵 사용 가능
        bool useAbleMap = false;
        //버튼 애니메이션용 코루틴
        Coroutine buttonAnimaitonCoroutine;

        private int energy = 0;

        /// <summary>
        /// 캐릭터 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromCharacter(bool value)
        {
            useAbleCharacter = value;
            //실제 뷰를 업데이트
            UpdateValue();
        }

        /// <summary>
        /// 탄약 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromAmmo(bool value)
        {
            useAbleAmmo = value;
            //실제 뷰를 업데이트
            UpdateValue();
        }

        /// <summary>
        /// 총 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromGun(bool value)
        {
            useAbleGun = value;
            //실제 뷰를 업데이트
            UpdateValue();
        }

        /// <summary>
        /// 맵 사용 가능 여부 업데이트
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValueFromMap(bool value)
        {
            useAbleMap = value;
            //실제 뷰를 업데이트
            UpdateValue();
        }

        /// <summary>
        /// 실제 뷰 업데이트
        /// </summary>
        private void UpdateValue()
        {
            //캐릭터 , 탄약, 총 3개 모두 true 일때만 최종 true전달
            if(useAbleCharacter && useAbleAmmo && useAbleGun && useAbleMap)
            {
                View.UpdateValue(true);

                //사용 가능 이벤트
                UpdateUseAbleEvent.Invoke(true);
            } else//아니면 false
            {
                View.UpdateValue(false);

                //사용 불가 이벤트
                UpdateUseAbleEvent.Invoke(false);
            }
        }

        /// <summary>
        /// 에너지 업데이트
        /// </summary>
        /// <param name="roundCount">라운드 카운트</param>
        public void UpdateEnergy(int roundCount)
        {
            //사용 에너지 계산식
            //기본 에너지 10 + 라운드 1 * 라운드 1당 추가 되는 에너지
            this.energy = GlobalDataController.DefaultConsumeEnergy + roundCount * GlobalDataController.ConsumeEnergyByRound;

            //사용할 에너지 업데이트
            View.UpdateEnergy(this.energy.ToString());

            //에너지 트윈 연출
            View.EffectEnergy();
        }

        /// <summary>
        /// 시작 버튼 눌림
        /// </summary>
        public void StartClick()
        {
            //웨이브(라운드)를 계산한 소모되는 에너지
            int consumeEnergy = GlobalDataController.GetConsumeEnergy(GlobalDataController.DefaultConsumeEnergy, GlobalDataController.ConsumeEnergyByRound, GlobalDataController.SelectRound);

            //현재 에너지 가 충분하면 사용
            if (energyPresenter.Model.GetData().CurrentEnergy >= consumeEnergy)
            {
                energyPresenter.UpdateEnergy(-consumeEnergy);
                /// 해당 씬으로 이동
                GoToScene();

                //에너지 부족 시작 아님
                GlobalDataController.NoEnergyStart = false;

                Debug.Log("남은 에너지 = " + energyPresenter.Model.GetData().CurrentEnergy + " 사용한 에너지 = " + consumeEnergy);
            } else
            {
                //에너지 부족 메시지 출력
                //NotifyPopupController.Instance.OpenResult(UINotifyPopupModel.Type.NotEnoughEnergy);
                //에너지 부족 팝업 출력
                PopupOpener.OpenPopup();

                //가이드 잠시 표시후 닫음
                if (buttonAnimaitonCoroutine != null) { StopCoroutine(buttonAnimaitonCoroutine); } 
                buttonAnimaitonCoroutine = StartCoroutine(ButtonAnimaitonCoroutine());
            }
        }

        /// <summary>
        /// 가이드 잠시 표시후 닫음
        /// </summary>
        /// <returns></returns>
        IEnumerator ButtonAnimaitonCoroutine()
        {
            UICanStartGuiderView canStartGuiderView = GameObject.FindObjectOfType<UICanStartGuiderView>();
            canStartGuiderView.Energy.SetActive(true);
            yield return new WaitForSeconds(3f);
            canStartGuiderView.Energy.SetActive(false);
        }

        /// <summary>
        /// 해당 씬으로 이동
        /// *씬 이동전 필요한 데이터 취합 및 이동 통지.
        /// *선택된 카드, 무기 , 탄약
        /// </summary>
        public void GoToScene()
        {
            //씬이름
            sceneTransition.scene = "Town";
            //씬이동
            sceneTransition.PerformTransition();

            //스테이지 시작 이벤트
            StageStartEvent.Invoke();
        }
    }
}
