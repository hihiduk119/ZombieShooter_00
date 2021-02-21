using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 라운드 선택 프리젠터
    /// </summary>
    public class UIRoundSelectPresenter : MonoBehaviour
    {
        [Header("[MVP View]")]
        public UIRoundSelectView View;

        //라운드 카운트
        private int roundCount = 0;

        //라운드 강하게 올릴때 사용하는 값
        private int strongValue = 10;

        [Header("[[Auto] 해당되는 맵 가져오기]")]
        public Map.Setting Setting;

        //시작 버튼 컨트롤러
        private UIStartButtonPresenter startButtonPresenter;

        //올리고 내릴때 최대 라운드는 플레이어가 플레이한 라운드
        //사용되는 에너지 값 변경
        //모든 맵마다 최대 언락된 라운드 저장 해야함.
        private void Awake()
        {
            this.View.RoundUpEvent.AddListener(RoundUp);
            this.View.RoundDownEvent.AddListener(RoundDown);
            this.View.RoundStrongUpEvent.AddListener(RoundStrongUp);
            this.View.RoundStrongDownEvent.AddListener(RoundStrongDown);

            //시작 버튼 컨트롤러
            startButtonPresenter = GameObject.FindObjectOfType<UIStartButtonPresenter>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            //글로벌 데이터와 싱크
            roundCount = GlobalDataController.SelectRound;

            //버튼 활성 & 비활성
            //*여기에 있는게 맞는가??
            DoActvateButton();
        }

        /// <summary>
        /// 1씩 라운드 올리기
        /// </summary>
        private void RoundUp()
        {
            //임시 라운드 
            roundCount++;

            // 라운드 업다운 실행
            DoUpAndDown();

            //버튼 활성 & 비활성
            DoActvateButton();
        }

        /// <summary>
        /// 1씩 라운드 내리기
        /// </summary>
        private void RoundDown()
        {
            roundCount--;

            // 라운드 업다운 실행
            DoUpAndDown();

            //버튼 활성 & 비활성
            DoActvateButton();
        }

        /// <summary>
        /// 10씩 라운드 올리기
        /// </summary>
        private void RoundStrongUp()
        {
            roundCount += strongValue;

            // 라운드 업다운 실행
            DoUpAndDown();

            //버튼 활성 & 비활성
            DoActvateButton();
        }

        /// <summary>
        /// 10씩 라운드 내리기
        /// </summary>
        private void RoundStrongDown()
        {
            roundCount -= strongValue;

            // 라운드 업다운 실행
            DoUpAndDown();

            //버튼 활성 & 비활성
            DoActvateButton();
        }

        /// <summary>
        /// 버튼 사용 가능 여부 체크
        /// * 여기서 Setting.data.ReachedRound값을 보고 버튼 활성 & 비활성
        /// </summary>
        void DoActvateButton()
        {
            //라운드 카운트가 0과 같다면 왼쪽 버튼 비활성
            if (roundCount == 0) { View.SetLeftButtons(false); }
            //라운드 카운트가 0과 같다면 왼쪽 버튼 활성
            else { View.SetLeftButtons(true); }

            //라운드 카운트가 맵 최대 도달 카운트와 같다면 오른쪽 버튼 비활성
            if (roundCount ==  Setting.ReachedRound) { View.SetRightButtons(false); }
            ////라운드 카운트가 맵 최대 도달 카운트와 같다면 오른쪽 버튼 활성
            else { View.SetRightButtons(true); }
        }

        /// <summary>
        /// 라운드 업다운 실행
        /// </summary>
        void DoUpAndDown()
        {
            //라운드 증가 가능 여부 확인
            if (CanUpAndDown(roundCount, Setting.ReachedRound))
            {
                //가능시 실제 글로벌데이터 적용
                GlobalDataController.SelectRound = roundCount;
            }
            else
            {
                //불가능시 글로벌데이터로 롤백
                roundCount = GlobalDataController.SelectRound;
            }

            //라운드 화면 적용
            View.SetRound((roundCount + 1).ToString());

            //에너지 적용
            startButtonPresenter.UpdateEnergy(roundCount);

            //에너지 트윈 연출
            View.EffectRound();
        }

        /// <summary>
        /// 라운드 업다운 가능 여부
        /// </summary>
        /// <param name="maxRound"></param>
        /// <returns></returns>
        bool CanUpAndDown(int value, int maxRound)
        {
            //최대 라운드 값보다 큰지 확인
            if(maxRound < value)
            {
                return false;
            }

            //0보다 큰지 확인
            if(0 > value)
            {
                return false;
            }
            return true;
        }
    }
}
